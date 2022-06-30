using AutoMapper;
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
    public class UserStatusChangeById
    {
        public class Command : IRequest<string>
        {
            public int UserId { get; set; }
            public bool Status { get; set; }

        }
        public class QueryHandler : IRequestHandler<Command, string>
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

            public async Task<string> Handle(Command query, CancellationToken cancellationToken)
            {
                var result = "";
                try
                {
                    _logger.LogInformation("Start UserStatusChangeById Method, Role Accountant, command");

                    var user = await _db.Users.FindAsync(query.UserId);
                    if (user == null)
                    {
                        return result;
                    }
                    user.IsBlocked = true;
                    _db.Users.Update(user);
                    await _db.SaveChangesAsync(cancellationToken);
                    return result= user.UserName;
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
