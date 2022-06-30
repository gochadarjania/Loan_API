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

namespace Loan_API.Application.RoleAccountant.Queries
{
    public class GetLoansOfUser
    {
        public class Query : IRequest<List<Domain.Loan>>
        {
            public int UserId { get; set; }

        }
        public class QueryHandler : IRequestHandler<Query, List<Domain.Loan>>
        {
            private readonly LoanDbContext _db;
            private readonly ILogger<QueryHandler> _logger;
            public QueryHandler(ILogger<QueryHandler> logger, LoanDbContext db)
            {
                _db = db;
                _logger = logger;
            }

            public async Task<List<Domain.Loan>> Handle(Query query, CancellationToken cancellationToken)
            {
                var loanViews = new List<Domain.Loan>();
                try
                {
                    _logger.LogInformation("Start GetLoansOfUser Method, Role Accountant, Query");

                    loanViews = await (from loan in _db.Loans
                                       where loan.UserId == query.UserId
                                       select loan).ToListAsync(cancellationToken);

                    return loanViews;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    return loanViews;
                }
            }
        }
    }
}
