using Loan_API.Application.RoleUser.Commands;
using Loan_API.Application.RoleUser.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_API.Controllers
{
    [Authorize(Roles = "User")]
    public class RoleUserController : BaseController
    {
        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoan.Command loan, [FromHeader] string authorization)
        {
            loan.SetUserId(authorization);
            var result = await Mediator.Send(loan);
            if (result == 0)
            {
                return BadRequest("You can't add a loan!");
            }

            return Ok($"Create {result}");
        }

        [HttpPatch("UpdateLoan/{loanId}")]
        public async Task<IActionResult> UpdateLoan(int loanId, [FromBody] UpdateLoanById.Command loan, [FromHeader] string authorization)
        {
            loan.SetUserId(authorization, loanId);
            var result = await Mediator.Send(loan);
            if (result == 0)
            {
                return BadRequest("You can't Update a loan!");
            }

            return Ok($"Update loan, Id: {result}");
        }

        [HttpPatch("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser.Command user, [FromHeader] string authorization)
        {
            user.SetUserId(authorization);
            var result = await Mediator.Send(user);
            var u = await Mediator.Send(user);
            if (result == 0)
            {
                return BadRequest("You can't Update a info!");
            }

            return Ok($"Update user, Id: {result}");
        }

        [HttpDelete("DeleteLoanById/{loanId}")]
        public async Task<IActionResult> DeleteLoanById(int loanId, [FromHeader] string authorization)
        {
            var loan = new DeleteLoanById.Command() { LoanId = loanId };
            loan.SetUserId(authorization);
            var result = await Mediator.Send(loan);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("You can't delete loan!");
            }
            return Ok(result);
        }

        [HttpGet("GetLoanById/{id}")]
        public async Task<IActionResult> GetLoan(int id, [FromHeader] string authorization)
        {
            var loan = new GetLoanById.Query() { LoanId = id };
            loan.SetUserId(authorization);
            var result = await Mediator.Send(loan);

            if (result == null)
            {
                return NotFound("No found loan!");
            }
            return Ok(result);
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromHeader] string authorization)
        {
            var queryUser = new GetUserById.Query();
            queryUser.SetUserId(authorization);
            var user = await Mediator.Send(queryUser);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("GetLoansOfUser")]
        public async Task<IActionResult> GetLoansOfUser([FromHeader] string authorization)
        {
            var userIdSet = new GetLoansByUser.Query();
            userIdSet.SetUserId(authorization);
            var loans = await Mediator.Send(userIdSet);
            if (loans == null)
            {
                return NotFound();
            }
            return Ok(loans);
        }
    }
}