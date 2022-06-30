using AutoMapper;
using FluentValidation;
using Loan_API.Application.Common.Helpers;
using Loan_API.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loan_API.Application.RoleAccountant.Commands
{
    public class UpdateLoanByUserId
    {
        public class Command : IRequest<int>
        {
            public DateTime LoanPeriodTime { get; set; }
            public double Amount { get; set; }

            public int CurrencyId { get; set; }
            public int LoanTypeId { get; set; }
            public int LoanStatusId { get; set; }

            public int LoanId { get; private set; }
            public int UserId { get; private set; }

            public void SetUserId(int userId, int loanId)
            {
                LoanId = loanId;
                UserId = userId;
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
                    _logger.LogInformation("Start UpdateLoanByUserId Method, Role Accountant, command");

                    var loan = await _db.Loans.FindAsync(request.LoanId);
                    if (loan != null)
                    {
                        if (request.LoanPeriodTime != new DateTime())
                        {
                            loan.LoanPeriodTime = request.LoanPeriodTime;
                        }
                        if (request.CurrencyId != 0)
                        {
                            loan.CurrencyId = request.CurrencyId;
                        }
                        if (request.LoanTypeId != 0)
                        {
                            loan.LoanTypeId = request.LoanTypeId;
                        }
                        if (request.Amount != 0)
                        {
                            loan.Amount = request.Amount;
                        }
                        if (request.LoanStatusId != 0)
                        {
                            loan.LoanStatusId = request.LoanStatusId;
                        }

                        _db.Loans.Update(loan);
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
