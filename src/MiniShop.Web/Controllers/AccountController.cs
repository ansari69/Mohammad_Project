using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.Application.Account.Commands.Login;
using MiniShop.Application.Common.Models;

namespace MiniShop.Web.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ApiController<AccountController>
    {
        public AccountController(IMediator mediator, ILogger<AccountController> logger)
          : base(mediator, logger)
        { }


        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand model)
        {
            try
            {
                var response = await _mediator.Send(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }




    }
}
