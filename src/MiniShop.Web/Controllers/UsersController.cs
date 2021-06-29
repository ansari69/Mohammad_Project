using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.Application.Common.Models;
using MiniShop.Application.Users.Commands.ConfirmUserByManger;
using MiniShop.Application.Users.Commands.InsertUser;
using MiniShop.Application.Users.Commands.UpsertUserRoles;
using MiniShop.Application.Users.Queries.GetAllUsers;
using MiniShop.Infrastructure.Security.Stores;

namespace MiniShop.Web.Controllers
{
    [ApiController]
    public class UsersController : ApiController<UsersController>
    {

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
       : base(mediator, logger)
        { }


        /// <summary>
        /// Insert user
        /// </summary>
        /// <response code="200">InsertUserCommandVM</response>
        [HttpPost("/user/upsert")]
        [ProducesResponseType(typeof(InsertUserCommandVM), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> UpsertAsync([FromBody] InsertUserCommand model)
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


        /// <summary>
        /// Confirm Use rBy Manger 
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">bool</response>
        [HttpPut("/user/confirm/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> DeactivateAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new ConfirmUserByMangerCommand() { UserId = id });
                return Ok();
            }
            catch (Exception ex)
            {
                 return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }


        /// <summary>
        /// Get all users
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">GetAllUsersVM</response>
        [HttpPost("/users")]
        [ProducesResponseType(typeof(GetAllUsersVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetAllAsync([FromBody] GetAllUsersQuery model)
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


        /// <summary>
        /// Add Role for user
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">UpsertUserRolesVM</response>
        [HttpPost("/user/add/roles")]
        [ProducesResponseType(typeof(UpsertUserRolesVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> UpsertUserRolesAsync([FromBody] UpsertUserRolesCommand model)
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
