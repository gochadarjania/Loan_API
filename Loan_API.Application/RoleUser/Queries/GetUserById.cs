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
    public class GetUserById
    {
        public class Query : IRequest<UserViewModel>
        {
            public int UserId { get; private set; }
            public void SetUserId(string authorization)
            {
                var _headParseUserId = new HeadParseUserId();

                UserId = _headParseUserId.GetUserIdFromHeadTokenDecoder(authorization);
            }

        }
        public class QueryHandler : IRequestHandler<Query, UserViewModel>
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

            public async Task<UserViewModel> Handle(Query query, CancellationToken cancellationToken)
            {
                var userViews = new UserViewModel();
                try
                {
                    _logger.LogInformation("Start GetUserById Method, Role User, Query");

                    userViews = await (from u in _db.Users
                                       where u.Id == query.UserId
                                       select new UserViewModel
                                       {
                                           FirstName = u.FirstName,
                                           LastName = u.LastName,
                                           UserName = u.UserName,
                                           Age = u.Age,
                                           Salary = u.Salary
                                       }).SingleOrDefaultAsync(cancellationToken);

                    return userViews;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    return userViews;
                }
            }
        }
    }
}
