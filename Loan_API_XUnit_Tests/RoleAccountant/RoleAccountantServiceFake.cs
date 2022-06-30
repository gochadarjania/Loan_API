using Loan_API.Domain;
using Loan_API_XUnit_Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API_XUnit_Tests.RoleAccountant
{
    public class RoleAccountantServiceFake
    {
        List<Loan> _loans;
        List<User> _users;
        public RoleAccountantServiceFake()
        {

            DataDbConcext _dataDbConcext = new DataDbConcext();
            _loans = _dataDbConcext.Loans;
            _users = _dataDbConcext.Users;
        }
        public int DeleteLoanOfUser(int loanId)
        {

            var loan = _loans.Find(x => x.Id == loanId);

            if (loan == null)
            {
                return 0;
            }
            _loans.Remove(loan);
            return loan.Id;
        }
        public int UpdateLoanByUserId(int userId, Loan loanNew)
        {

            var loan = _loans.Find(x => x.Id == loanNew.Id && x.UserId == userId);

            if (loan == null)
            {
                return 0;
            }

            for (int i = 0; i < _loans.Count; i++)
            {
                if (_loans[i].Id == loanNew.Id)
                {
                    _loans[i] = loanNew;
                }
            }

            return loanNew.Id;
        }
        public int UserStatusChangeById(int userId, bool status)
        {

            var loan = _users.Find(x => x.Id == userId);

            if (loan == null)
            {
                return 0;
            }

            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Id == userId)
                {
                    _users[i].IsBlocked = status;
                }
            }

            return 1;
        }
        public int GetLoansOfUser(int userId)
        {

            var loan = _users.Find(x => x.Id == userId);

            if (loan == null)
            {
                return 0;
            }
            int count = 0;
            for (int i = 0; i < _loans.Count; i++)
            {
                if (_loans[i].UserId == userId)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
