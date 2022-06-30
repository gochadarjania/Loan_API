using Loan_API.Domain;
using System;
using Xunit;

namespace Loan_API_XUnit_Tests.RoleAccountant
{
    public class RoleAccountantTests
    {
        private readonly RoleAccountantServiceFake _service;

        public RoleAccountantTests()
        {
            _service = new RoleAccountantServiceFake();
        }
        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNullResponse()
        {
            // Arrange
            var notExistingId = 8;

            // Act
            var nullResponse = _service.DeleteLoanOfUser(notExistingId);

            // Assert
            Assert.NotEqual(notExistingId,nullResponse);
        }
        [Fact]
        public void Remove_ExistingIdPassed_ReturnsIdResponse()
        {
            // Arrange
            var ExistingId = 1;

            // Act
            var idResponse = _service.DeleteLoanOfUser(ExistingId);

            // Assert
            Assert.Equal(ExistingId,idResponse);
        }        
        
        [Fact]
        public void Update_LoanByUserId_ResponseLoanId()
        {
            // Arrange
            var loan = new Loan() { Id = 1, UserId = 2, Amount = 5000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 1, LoanStatusId = 1, LoanTypeId = 1 };

            // Act
            var idResponse = _service.UpdateLoanByUserId(loan.UserId, loan);

            // Assert
            Assert.Equal(loan.Id,idResponse);
        }
        [Fact]
        public void Update_UserStatusChangeById_ResponseStatus()
        {
            // Arrange
            var userId = 1;

            // Act
            var idResponse = _service.UserStatusChangeById(userId, true);

            // Assert
            Assert.Equal(1,idResponse);
        }
        [Fact]
        public void Get_LoansOfUser_ResponseListCount()
        {
            // Arrange
            var userId = 2;

            // Act
            var idResponse = _service.GetLoansOfUser(userId);

            // Assert
            Assert.True(idResponse>2);
        }
    }
}
