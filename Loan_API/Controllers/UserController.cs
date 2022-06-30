using Loan_API.Application.Login.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_API.Controllers
{
    public class UserController : BaseController
    {

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationUser.Command user)
        {
            return Ok(await Mediator.Send(user));
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser.Command user)
        {
            var result = await Mediator.Send(user);
            return Ok(result);
        }
    }
}
