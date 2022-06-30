using Loan_API.Domain;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loan_API_XUnit_Tests.RoleUser;

namespace Loan_API_XUnit_Tests.UserLogin
{
    public class UserServicesTest
    {
        private readonly UserServices _service;
        public UserServicesTest()
        {
            _service = new UserServices();
        }
        [Fact]
        public void Check_LoginUser_ResponseUserName()
        {
            // Arrange
            var userName = "admin@gmail.com";
            var password = "admin";

            // Act
            var userNameResponse = _service.LoginUser(userName, password);

            // Assert
            Assert.Equal(userName, userNameResponse);

        }
        [Fact]
        public void Registration_User_ResponseUserName()
        {
            var loans = new List<Loan>()
            {
                new Loan (){Id = 6, UserId = 8, Amount = 4000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 3, LoanStatusId = 3, LoanTypeId = 3}
            };
            // Arrange
            var user = new User { 
                Id = 8, FirstName = "Thomas", LastName = "Hardy", 
                UserName = "Test12344", Password = "admin", Age = 20, Salary = 1000, 
                RoleId = 1, IsBlocked = false, Loans = loans};

            // Act
            var userNameResponse = _service.RegistrationUser(user);

            // Assert
            Assert.Equal(user.UserName, userNameResponse);

        }

        [Fact]
        public void Registration_UserAlreadyExsist_ErrorMessage()
        {
            // Arrange
            var loans = new List<Loan>()
            {
                new Loan (){Id = 6, UserId = 8, Amount = 4000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 3, LoanStatusId = 3, LoanTypeId = 3}
            };
            var user = new User
            {
                Id = 4,
                FirstName = "Thomas",
                LastName = "Hardy",
                UserName = "admin@gmail.com",
                Password = "admin",
                Age = 20,
                Salary = 1000,
                RoleId = 1,
                IsBlocked = false,
                Loans = loans
            };

            // Act
            var userNameResponse = _service.RegistrationUser(user);

            // Assert
            Assert.NotEqual(user.UserName, userNameResponse);

        }
    }
}
