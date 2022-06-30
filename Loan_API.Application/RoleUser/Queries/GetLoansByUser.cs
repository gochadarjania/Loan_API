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
    public class GetLoansByUser
    {
        public class Query : IRequest<List<LoanViewModel>>
        {
            public int UserId { get; private set; }
            public void SetUserId(string authorization)
            {
                var _headParseUserId = new HeadParseUserId();

                UserId = _headParseUserId.GetUserIdFromHeadTokenDecoder(authorization);
            }

        }
        public class QueryHandler : IRequestHandler<Query, List<LoanViewModel>>
        {
            private readonly LoanDbContext _db;
            private readonly IMapper _mapper;
            private readonly ILogger<QueryHandler> _logger;
            public QueryHandler(ILogger<QueryHandler> logger, LoanDbContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<List<LoanViewModel>> Handle(Query query, CancellationToken cancellationToken)
            {
                var loanViews = new List<LoanViewModel>();
                try
                {
                    _logger.LogInformation("Start GetLoansByUser Method, Role User, Query");

                    loanViews = await (from loan in _db.Loans
                                      join loanT in _db.LoanTypes on loan.LoanTypeId equals loanT.Id
                                      join loanS in _db.LoanStatuses on loan.LoanStatusId equals loanS.Id
                                      join loanC in _db.Currencies on loan.CurrencyId equals loanC.Id
                                      where loan.UserId == query.UserId
                                       select new LoanViewModel
                                       {
                                           Id = loan.Id,
                                           Amount = loan.Amount,
                                          LoanType = loanT.LoanTypeName,
                                          Currency = loanC.CurrencyName,
                                          LoanLoanStatus = loanS.LoanStatusName,
                                          LoanPeriodTime = loan.LoanPeriodTime,
                                          UserId = loan.UserId
                                      }).ToListAsync(cancellationToken);

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
