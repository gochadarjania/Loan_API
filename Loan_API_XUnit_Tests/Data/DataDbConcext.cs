using Loan_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Loan_API_XUnit_Tests.Data
{
    internal class DataDbConcext
    {
        public List<Loan> Loans;
        public List<User> Users;
        public DataDbConcext()
        {
            Loans = new List<Loan>()
            {
                new Loan { Id = 1, UserId = 2, Amount = 2000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 1, LoanStatusId = 1, LoanTypeId = 1 },
                new Loan { Id = 2, UserId = 2, Amount = 2000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 1, LoanStatusId = 1, LoanTypeId = 1 },
                new Loan { Id = 3, UserId = 2, Amount = 2000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 1, LoanStatusId = 1, LoanTypeId = 1 },
                new Loan { Id = 4, UserId = 3, Amount = 3000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 2, LoanStatusId = 1, LoanTypeId = 1 },
                new Loan { Id = 5, UserId = 4, Amount = 4000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 3, LoanStatusId = 3, LoanTypeId = 3 }

            };

            Users = new List<User>()
            {
                new User { Id = 1, FirstName = "Thomas", LastName = "Hardy", UserName = "admin@gmail.com", Password = BCryptNet.HashPassword("admin"), Age = 20, Salary = 1000, RoleId = 1},
                new User { Id = 2, FirstName = "Christina", LastName = "Berglund", UserName = "user2@gmail.com", Password = BCryptNet.HashPassword("user2"), Age = 25, Salary = 1000, IsBlocked = true },
                new User { Id = 3, FirstName = "Ana", LastName = "Trujillo", UserName = "user3@gmail.com", Password = BCryptNet.HashPassword("user3"), Age = 30, Salary = 1000, },
                new User { Id = 4, FirstName = "Maria", LastName = "Anders", UserName = "user4@gmail.com", Password = BCryptNet.HashPassword("user4"), Age = 35, Salary = 1000, }
            };



        }
    }
}
