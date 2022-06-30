using AutoMapper;
using Loan_API.Application.Common.Helpers;
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

namespace Loan_API.Application.RoleUser.Commands
{
    public class DeleteLoanById
    {
        public class Command : IRequest<string>
        {
            public int LoanId { get; set; }
            public int UserId { get; private set; }
            public void SetUserId(string authorization)
            {
                var _headParseUserId = new HeadParseUserId();

                UserId = _headParseUserId.GetUserIdFromHeadTokenDecoder(authorization);
            }

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
                    _logger.LogInformation("Start DeleteLoanById Method, Role User, Command");

                    var loan = await _db.Loans.SingleOrDefaultAsync(x => x.Id == query.LoanId && x.UserId == query.UserId, cancellationToken);
                    var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == query.UserId, cancellationToken);

                    if (loan == null) { return "Not Found"; }
                    if (loan.LoanStatusId == 1 && user.IsBlocked == false)
                    {
                        _db.Loans.Remove(loan);
                        await _db.SaveChangesAsync(cancellationToken);
                        return "Loan is deleted!";
                    }
                    return null;
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
