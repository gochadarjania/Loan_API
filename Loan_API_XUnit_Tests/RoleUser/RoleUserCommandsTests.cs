using Loan_API.Domain;
using Loan_API_XUnit_Tests.RoleUser.Commands;
using Loan_API_XUnit_Tests.RoleUser.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Loan_API_XUnit_Tests.RoleUser
{
    public class RoleUserCommandsTests
    {
        private readonly CommandUserRoleServices _serviceCommand;

        public RoleUserCommandsTests()
        {
            _serviceCommand = new CommandUserRoleServices();
        }
        [Fact]
        public void CreateLoan_AddSucsses_ReturnsOne()
        {
            // Arrange
            var loan = new Loan()
            {
                Id = 7,
                UserId = 4,
                Amount = 4000,
                LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20),
                CurrencyId = 3,
                LoanStatusId = 3,
                LoanTypeId = 3
            }
            ;

            // Act
            var response = _serviceCommand.CreateLoan(loan);

            // Assert
            Assert.Equal(1, response);

        }
        [Fact]
        public void CreateLoan_AddFaildeBlockedUser_Return0()
        {
            // Arrange
            var loan = new Loan()
            {
                Id = 7,
                UserId = 2,
                Amount = 4000,
                LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20),
                CurrencyId = 3,
                LoanStatusId = 3,
                LoanTypeId = 3
            }
            ;

            // Act
            var response = _serviceCommand.CreateLoan(loan);

            // Assert
            Assert.Equal(0, response);

        }

        [Fact]
        public void DeleteLoan_Sucsses_ReturnsNotFound()
        {
            // Arrange
            var userId = 4;
            var loanId = 8;

            // Act
            var response = _serviceCommand.DeleteLoanById(userId, loanId);

            // Assert
            Assert.Equal("Not Found", response);

        }

        [Fact]
        public void DeleteLoan_Sucsses_ReturnsYoyCantDelete()
        {
            // Arrange
            var userId = 2;
            var loanId = 1;

            // Act
            var response = _serviceCommand.DeleteLoanById(userId, loanId);

            // Assert
            Assert.Equal("You can't delete loan!", response);

        }

        [Fact]
        public void DeleteLoan_Sucsses_ReturnsLoanIsDeleted()
        {
            // Arrange
            var userId = 3;
            var loanId = 4;

            // Act
            var response = _serviceCommand.DeleteLoanById(userId, loanId);

            // Assert
            Assert.Equal("Loan is deleted!", response);

        }

        [Fact]
        public void UpdateLoan_Sucsses_ReturnIsUpdated()
        {
            // Arrange
            var userId = 3;
            var loanId = 4;
            var loan = new Loan()
            {
                Amount = 4000,
                LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20),
                CurrencyId = 3,
                LoanTypeId = 3
            }
           ;
            // Act
            var response = _serviceCommand.UpdateLoanById(userId, loanId, loan);

            // Assert
            Assert.Equal("Loan is updated!", response);

        }

        [Fact]
        public void UpdateLoan_Sucsses_ReturnNotFound()
        {
            // Arrange
            var userId = 4;
            var loanId = 8;
            var loan = new Loan()
            {
                Amount = 4000,
                LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20),
                CurrencyId = 3,
                LoanTypeId = 3
            };
            // Act
            var response = _serviceCommand.UpdateLoanById(userId, loanId, loan);

            // Assert
            Assert.Equal("Not Found", response);

        }
        [Fact]
        public void UpdateLoan_Sucsses_ReturnCantUpdate()
        {
            // Arrange
            var userId = 2;
            var loanId = 1;
            var loan = new Loan()
            {
                Amount = 4000,
                LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20),
                CurrencyId = 3,
                LoanTypeId = 3
            };
            // Act
            var response = _serviceCommand.UpdateLoanById(userId, loanId, loan);

            // Assert
            Assert.Equal("You can't update loan!", response);

        }
        [Fact]
        public void UpdateUser_Sucsses_ReturnIsUpdated()
        {
            // Arrange
            var userId = 4;
            var user = new User()
            {
                FirstName = "Jan",
                LastName  = "TestNew",
                UserName  = "UserNametes",
                Password  = "Passtest",
                Age       = 25,
                Salary    = 1500
            };
            // Act
            var response = _serviceCommand.UpdateUserById(userId, user);

            // Assert
            Assert.Equal("User is updated!", response);

        }

        [Fact]
        public void UpdateUser_Sucsses_ReturnNotFound()
        {
            // Arrange
            var userId = 38;
            var user = new User()
            {
                FirstName = "Jan",
                LastName = "TestNew",
                UserName = "UserNametes",
                Password = "Passtest",
                Age = 25,
                Salary = 1500
            };
            // Act
            var response = _serviceCommand.UpdateUserById(userId, user);

            // Assert
            Assert.Equal("Not Found", response);

        }
        [Fact]
        public void UpdateUser_Sucsses_ReturnCantUpdate()
        {
            // Arrange
            var userId = 2;
            var user = new User()
            {
                FirstName = "Jan",
                LastName = "TestNew",
                UserName = "UserNametes",
                Password = "Passtest",
                Age = 25,
                Salary = 1500
            };
            // Act
            var response = _serviceCommand.UpdateUserById(userId, user);

            // Assert
            Assert.Equal("You can't update User!", response);

        }
    }
}
