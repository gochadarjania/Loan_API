using AutoMapper;
using Loan_API.Application.Common.Helpers;
using Loan_API.Application.RoleUser.Queries.ViewModel;
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

namespace Loan_API.Application.RoleUser.Queries
{
    public class GetLoanById
    {
        public class Query : IRequest<LoanViewModel>
        {
            public int UserId { get; private set; }
            public int LoanId { get; set; }

            public void SetUserId(string authorization)
            {
                var _headParseUserId = new HeadParseUserId();
                UserId = _headParseUserId.GetUserIdFromHeadTokenDecoder(authorization);
            }

        }
        public class QueryHandler : IRequestHandler<Query, LoanViewModel>
        {
            private readonly LoanDbContext _db;
            private readonly ILogger<QueryHandler> _logger;
            public QueryHandler(ILogger<QueryHandler> logger, LoanDbContext db)
            {
                _db = db;
                _logger = logger;
            }

            public async Task<LoanViewModel> Handle(Query query, CancellationToken cancellationToken)
            {
                var loanView = new LoanViewModel();
                try
                {
                    _logger.LogInformation("Start GetLoanById Method, Role User, Query");

                    loanView = await (from loan in _db.Loans
                                      join loanT in _db.LoanTypes on loan.LoanTypeId equals loanT.Id
                                      join loanS in _db.LoanStatuses on loan.LoanStatusId equals loanS.Id
                                      join loanC in _db.Currencies on loan.CurrencyId equals loanC.Id
                                      where loan.Id == query.LoanId && loan.UserId == query.UserId
                                      select new LoanViewModel
                                      {
                                          Id = loan.Id,
                                          Amount = loan.Amount,
                                          LoanType = loanT.LoanTypeName,
                                          Currency = loanC.CurrencyName,
                                          LoanLoanStatus = loanS.LoanStatusName,
                                          LoanPeriodTime = loan.LoanPeriodTime,
                                          UserId = loan.UserId
                                      }).SingleOrDefaultAsync(cancellationToken);

                    return loanView;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    return loanView;
                }
            }
        }
    }
}
