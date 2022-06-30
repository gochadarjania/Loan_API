using AutoMapper;
using Loan_API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loan_API.Application.RoleAccountant.Commands
{
    public class DeleteLoanOfUser
    {
        public class Command : IRequest<string>
        {
            public int LoanId { get; set; }

        }
        public class QueryHandler : IRequestHandler<Command, string>
        {
            private readonly LoanDbContext _db;
            private readonly ILogger<QueryHandler> _logger;
            public QueryHandler(ILogger<QueryHandler> logger, LoanDbContext db)
            {
                _db = db;
                _logger = logger;
            }

            public async Task<string> Handle(Command query, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Start DeleteLoanOfUser Method, Role Accountant, command");

                    var loan = await _db.Loans.FindAsync(query.LoanId, cancellationToken);
                    if (loan == null)
                    {
                        return null;
                    }
                    _db.Loans.Remove(loan);
                    await _db.SaveChangesAsync(cancellationToken);
                    return "Loan is deleted!";
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
