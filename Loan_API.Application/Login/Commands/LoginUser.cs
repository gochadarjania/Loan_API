using Loan_API.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.IdentityModel.Tokens.Jwt;
using Loan_API.Application.Common.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Loan_API.Application.Login.Commands
{
    public class LoginUser : IRequest<List<string>>
    {
        public class Command : IRequest<List<string>>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.UserName)
                    .NotEmpty()
                    .MaximumLength(25)
                    .EmailAddress();

                RuleFor(c => c.Password)
                    .NotEmpty()
                    .MinimumLength(6);
            }
        }
        public class CommandHandler : IRequestHandler<Command, List<string>>
        {
            private readonly LoanDbContext _db;
            private readonly ILogger<CommandHandler> _logger;

            private readonly AppSettings _appSettings;
            public CommandHandler(LoanDbContext db, IOptions<AppSettings> appSettings, ILogger<CommandHandler> logger)
            {
                _db = db;
                _appSettings = appSettings.Value;
                _logger = logger;
            }

            public async Task<List<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userNameAndToken = new List<string>();
                try
                {
                    _logger.LogInformation("Start LoginUser Method");

                    var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);

                    if (user == null || !BCryptNet.Verify(request.Password, user.Password))
                    {
                        var ErrorMessage = "Username or password is incorrect";
                        userNameAndToken.Add(ErrorMessage);
                        return userNameAndToken;
                    }

                    var role = await (from r in _db.Roles
                                      where r.Users.Any(x => x.Id == user.Id)
                                      select r).SingleOrDefaultAsync(cancellationToken);

                    string tokenString = GenerateToken(user, role.RoleName);

                    userNameAndToken.Add(user.UserName);
                    userNameAndToken.Add(tokenString);

                    return userNameAndToken;

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    userNameAndToken.Add(ex.Message);
                    return userNameAndToken;
                }
            }

            private string GenerateToken(Domain.User user, string roleName)
            {
                try
                {
                    _logger.LogInformation("Start GenerateToken Method");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, roleName)
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);
                    return tokenString;

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    return null;
                }
            }
        }
    }
}
