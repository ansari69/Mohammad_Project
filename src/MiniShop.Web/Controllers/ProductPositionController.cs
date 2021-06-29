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
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Application.ProductPositions.Commands.DeleteProductPosition;
using MiniShop.Application.ProductPositions.Commands.UpsertProductPosition;
using MiniShop.Application.ProductPositions.Queries.GetAllProductPositions;
using MiniShop.Application.ProductPositions.Queries.GetSingleProductPosition;
using MiniShop.Infrastructure.Security.Stores;

namespace MiniShop.Web.Controllers
{

    [ApiController]
    public class ProductPositionController : ApiController<ProductPositionController>
    {

        public ProductPositionController(IMediator mediator, ILogger<ProductPositionController> logger)
         : base(mediator, logger)
        { }

        /// <summary>
        /// Insert and update Product status
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">UpsertProductPositionVM</response>
        [HttpPost("/product/status/upsert")]
        [ProducesResponseType(typeof(UpsertProductPositionVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> UpsertAsync([FromBody] UpsertProductPositionCommand model)
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
        /// Delete Product status
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">bool</response>
        [HttpDelete("/product/status/delete/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> DeactivateAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteProductPositionCommand() { ProductPositionId = id });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }

        /// <summary>
        /// Get single Product status
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">ProductPositionVM</response>
        [HttpGet("/product/status/{id}")]
        [ProducesResponseType(typeof(ProductPositionVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetSingleAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new GetSingleProductPositionQuery { ProductPositionId = id });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }


        /// <summary>
        /// Get All Product status
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">GetAllProductPositionsVM</response>
        [HttpPost("/product/status")]
        [ProducesResponseType(typeof(GetAllProductPositionsVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetAllAsync([FromBody] GetAllProductPositionsQuery model)
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
