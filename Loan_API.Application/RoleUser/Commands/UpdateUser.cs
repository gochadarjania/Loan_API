using FluentValidation;
using Loan_API.Application.Common.Helpers;
using Loan_API.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loan_API.Application.RoleUser.Commands
{
    public class UpdateUser
    {
        public class Command : IRequest<int>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int Age { get; set; }
            public double Salary { get; set; }
            public int UserId { get; private set; }

            public void SetUserId(string authorization)
            {
                var _headParseUserId = new HeadParseUserId();
                UserId = _headParseUserId.GetUserIdFromHeadTokenDecoder(authorization);
            }

        }
        public class Validator : AbstractValidator<Command>
        {
            LoanDbContext _db;
            public Validator(LoanDbContext db)
            {
                _db = db;

                RuleFor(c => c)
                    .Must(c =>
                    {
                        if (!string.IsNullOrEmpty(c.UserName))
                        {
                            return !_db.Users.Any(x => x.UserName == c.UserName);
                        }
                        return true;
                    }).WithMessage("is already taken");

                RuleFor(c => c.Age)
                    .Must(age => {
                            if (age != 0)
                            {
                             return (age > 12 && age < 90);
                            }
                        return true;
                    })
                    .WithMessage("Correct your {PropertyName} ");
                RuleFor(c => c)
                    .Must(c =>
                    {
                        if (!string.IsNullOrEmpty(c.Password))
                        {
                            if (c.Password.Length < 7)
                            {
                                return false;
                            }
                            return true;
                        }
                        return true;
                     })
                    .WithMessage("Password : Please choose a more secure password. It should be longer than 6 characters, unique to you, and difficult for others to guess.");

            }
        }
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly LoanDbContext _db;
            private readonly ILogger<CommandHandler> _logger;
            public CommandHandler(ILogger<CommandHandler> logger, LoanDbContext db)
            {
                _db = db;
                _logger = logger;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Start UpdateUser Method, Role User, Command");

                    var user = await _db.Users.FindAsync(request.UserId);
                    if (user == null) { return 0; }
                    if (user.IsBlocked != true)
                    {
                        if (!string.IsNullOrEmpty(request.FirstName)){ user.FirstName = request.FirstName; }
                        if (!string.IsNullOrEmpty(request.LastName)) { user.LastName = request.LastName; }
                        if (!string.IsNullOrEmpty(request.UserName)) { user.UserName = request.UserName; }
                        if (!string.IsNullOrEmpty(request.Password)) { user.Password = BCryptNet.HashPassword(request.Password); }
                        if (request.Salary != 0){ user.Salary = request.Salary; }

                        _db.Users.Update(user);
                        await _db.SaveChangesAsync(cancellationToken);
                        return user.Id;
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    return 0;
                }
            }
        }
    }
}
