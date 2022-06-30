using Loan_API.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Loan_API.Application.Login.Commands
{
    public class RegistrationUser
    {
        public class Command : IRequest<string>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int Age { get; set; }
            public double? Salary { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            LoanDbContext _db;
            public Validator(LoanDbContext db)
            {
                _db = db;

                RuleFor(c => c.FirstName).NotEmpty().MaximumLength(25);
                RuleFor(c => c.LastName).NotEmpty().MaximumLength(25);
                RuleFor(c => c.UserName)
                    .NotEmpty()
                    .MaximumLength(25)
                    .EmailAddress()
                    .Must(userName =>
                    {
                        return !_db.Users.Any(x => x.UserName == userName);
                    }).WithMessage("is already taken");
               RuleFor(c => c.Age).NotEmpty().
                    Must(age => { return (age > 12 && age<90); }).WithMessage("Correct your {PropertyName} ");
               RuleFor(c => c).NotEmpty().Must(c =>
                {
                    if (c.Password == c.FirstName || c.Password == c.LastName || c.Password == c.UserName || c.Password.Length <7)
                    {
                        return false;
                    }
                    return true;
                }).WithMessage("Password : Please choose a more secure password. It should be longer than 6 characters, unique to you, and difficult for others to guess.");

            }
        }
        public class CommandHandler : IRequestHandler<Command, string>
        {
            private readonly LoanDbContext _db;
            private readonly IMapper _mapper;
            private readonly ILogger<CommandHandler> _logger;
            public CommandHandler(ILogger<CommandHandler> logger, LoanDbContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Start RegistrationUser Method");

                    var user = _mapper.Map<Domain.User>(request);
                    user.Password = BCryptNet.HashPassword(request.Password);

                    await _db.Users.AddAsync(user, cancellationToken);
                    await _db.SaveChangesAsync(cancellationToken);

                    return request.UserName;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    return ex.Message;
                }               
            }
        }
    }
}
