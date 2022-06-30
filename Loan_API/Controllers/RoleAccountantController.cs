using Loan_API.Application.RoleAccountant.Commands;
using Loan_API.Application.RoleAccountant.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_API.Controllers
{
    [Authorize(Roles = "Accountant")]
    public class RoleAccountantController : BaseController
    {

        [HttpGet("GetLoansByUserId/{id}")]
        public async Task<IActionResult> GetLoans(int id)
        {
            var loans = await Mediator.Send(new GetLoansOfUser.Query() { UserId = id });
            return Ok(loans);
        }

        [HttpPatch("UserStatusChangeById/{id}")]
        public async Task<IActionResult> UserStatusChange(int id, [FromBody]bool status)
        {
            var user = await Mediator.Send(new UserStatusChangeById.Command() { UserId = id, Status = status });
            if (string.IsNullOrEmpty(user))
            {
                return NotFound(user);
            }
            return Ok("Update Status of User!");
        }

        [HttpPatch("UpdateLoanByUserId/{userId}/{loanId}")]
        public async Task<IActionResult> UpdateLoanByUserId(int userId, int loanId, UpdateLoanByUserId.Command loan)
        {
            loan.SetUserId(userId, loanId);
            var result = await Mediator.Send(loan);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok($"Update Status of User! Id: {result}");
        }

        [HttpDelete("DeleteLoanById/{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var result = await Mediator.Send(new DeleteLoanOfUser.Command() { LoanId = id });
            if (string.IsNullOrEmpty(result))
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
