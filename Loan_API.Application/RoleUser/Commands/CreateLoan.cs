using AutoMapper;
using FluentValidation;
using Loan_API.Application.Common.Helpers;
using Loan_API.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loan_API.Application.RoleUser.Commands
{
    public class CreateLoan
    {
        public class Command : IRequest<int>
        {

            public DateTime LoanPeriodTime { get; set; }
            public double Amount { get; set; }

            public int CurrencyId { get; set; }
            public int LoanTypeId { get; set; }
            public int UserId { get; private set; }

            public void SetUserId(string authorization)
            {
                var _headParseUserId = new HeadParseUserId();

                UserId = _headParseUserId.GetUserIdFromHeadTokenDecoder(authorization);
            }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator(LoanDbContext db)
            {
                RuleFor(l => l.Amount).NotEmpty().Must(a => { return !(a > 500); }).WithMessage("You can specify at least 500.");
                RuleFor(l => l.LoanPeriodTime).NotEmpty().Must(t => { return (t > DateTime.Today); }).WithMessage("Enter the date correctly!");
                RuleFor(l => l.CurrencyId).NotEmpty();
                RuleFor(l => l.LoanTypeId).NotEmpty();
            }
        }
        public class CommandHandler : IRequestHandler<Command, int>
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

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Start CreateLoan Method, Role User, Command");

                    var loan = _mapper.Map<Domain.Loan>(request);
                    var user = await _db.Users.FindAsync(request.UserId);
                    if (user.IsBlocked != true)
                    {
                        await _db.Loans.AddAsync(loan, cancellationToken);
                        await _db.SaveChangesAsync(cancellationToken);
                        return loan.Id;
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
