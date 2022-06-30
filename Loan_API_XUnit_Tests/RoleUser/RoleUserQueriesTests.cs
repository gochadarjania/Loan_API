using Loan_API_XUnit_Tests.RoleUser.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Loan_API_XUnit_Tests.RoleUser
{
    public class RoleUserQueriesTests
    {
        private readonly QueryUserRoleServices _serviceQuery;
        public RoleUserQueriesTests()
        {

            _serviceQuery = new QueryUserRoleServices();
        }

        [Fact]
        public void GetLoan_Sucsses_ReturnLoanId()
        {
            // Arrange
            var userId = 2;
            var loanId = 2;
            // Act
            var response = _serviceQuery.GetLoanById(userId, loanId);

            // Assert
            Assert.Equal(loanId, response);

        }

        [Fact]
        public void GetLoans_Sucsses_ReturnLoansListCount()
        {
            // Arrange
            var userId = 2;
            var loansLength = 3;
            // Act
            var response = _serviceQuery.GetLoansByUserId(userId);

            // Assert
            Assert.Equal(loansLength, response);

        }

        [Fact]
        public void GetUser_Sucsses_ReturnUserName()
        {
            // Arrange
            var userId = 1;
            var userName = "admin@gmail.com";

            // Act
            var response = _serviceQuery.GetUserById(userId);

            // Assert
            Assert.Equal(userName, response);

        }
    }
}
