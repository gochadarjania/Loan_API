using Loan_API.Domain;
using Loan_API_XUnit_Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Loan_API_XUnit_Tests.RoleUser.Commands
{
    public class CommandUserRoleServices
    {
        List<Loan> _loans;
        List<User> _users;
        public CommandUserRoleServices()
        {

            DataDbConcext _dataDbConcext = new DataDbConcext();
            _loans = _dataDbConcext.Loans;
            _users = _dataDbConcext.Users;
        }

        public int CreateLoan(Loan loan)
        {
            var user = _users.Find(x => x.Id == loan.UserId);
            if (user.IsBlocked == false)
            {
                _loans.Add(loan);
                return 1;
            }
            return 0;
        }
        public string DeleteLoanById(int userId, int loanId)
        {
            var user = _users.SingleOrDefault(x => x.Id == userId);
            var loan = _loans.SingleOrDefault(x => x.Id == loanId && x.UserId == userId);
            if (loan == null)
            {
                return "Not Found";
            }
            if (loan.LoanStatusId == 1 && user.IsBlocked == false)
            {
                _loans.Remove(loan);
                return "Loan is deleted!";
            }
            return "You can't delete loan!";
        }
        public string UpdateLoanById(int userId, int loanId, Loan loanNew)
        {
            var user = _users.SingleOrDefault(x => x.Id == userId);
            var loan = _loans.SingleOrDefault(x => x.Id == loanId && x.UserId == userId);
            if (loan == null)
            {
                return "Not Found";
            }
            if (loan.LoanStatusId == 1 && user.IsBlocked == false)
            {
                foreach (var item in _loans)
                {
                    if (item.Id == loanId)
                    {
                        item.Amount = loanNew.Amount;
                        item.LoanPeriodTime = loanNew.LoanPeriodTime;
                        item.CurrencyId = loanNew.CurrencyId;
                        item.LoanTypeId = loanNew.LoanTypeId;
                    }
                }
                return "Loan is updated!";
            }
            return "You can't update loan!";
        }
        public string UpdateUserById(int userId, User userNew)
        {
            var user = _users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return "Not Found";
            }
            if (user.IsBlocked == false)
            {
                foreach (var item in _users)
                {
                    if (item.Id == userId)
                    {
                        item.FirstName = userNew.FirstName;
                        item.LastName = userNew.LastName;
                        item.UserName = userNew.UserName;
                        item.Password = BCryptNet.HashPassword(userNew.Password);
                        item.Age = userNew.Age;
                        item.Salary = userNew.Salary;
                    }
                }
                return "User is updated!";
            }
            return "You can't update User!";
        }
    }
}
