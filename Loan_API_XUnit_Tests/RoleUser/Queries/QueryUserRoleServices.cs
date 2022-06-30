using Loan_API.Domain;
using Loan_API_XUnit_Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API_XUnit_Tests.RoleUser.Queries
{
    public class QueryUserRoleServices
    {
        List<Loan> _loans;
        List<User> _users;
        public QueryUserRoleServices()
        {

            DataDbConcext _dataDbConcext = new DataDbConcext();
            _loans = _dataDbConcext.Loans;
            _users = _dataDbConcext.Users;
        }
        public int GetLoanById(int userId, int loanId)
        {
            var loan = _loans.SingleOrDefault(x => x.Id == loanId && x.UserId == userId);
            if (loan == null)
            {
                return 0;
            }
            return loan.Id;
        } 
        public int GetLoansByUserId(int userId)
        {
            var loans = _loans.Where(x =>  x.UserId == userId).ToList();
            if (loans == null)
            {
                return 0;
            }
            return loans.Count;
        }
        public string GetUserById(int userId)
        {
            var user = _users.SingleOrDefault(x =>  x.Id == userId);
            if (user == null)
            {
                return "Not Found";
            }
            return user.UserName;
        }
    }
}
