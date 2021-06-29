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
using MiniShop.Application.ProductColors.Commands.DeleteProductColor;
using MiniShop.Application.ProductColors.Commands.UpsertProductColor;
using MiniShop.Application.ProductColors.Queries.GetAllProductColors;
using MiniShop.Application.ProductColors.Queries.GetSingleProductColor;
using MiniShop.Infrastructure.Security.Stores;

namespace MiniShop.Web.Controllers
{
    [ApiController]
    public class ProductColorController : ApiController<ProductColorController>
    {

        public ProductColorController(IMediator mediator, ILogger<ProductColorController> logger)
            : base(mediator, logger)
        { }


        /// <summary>
        /// Insert and update color
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">UpsertProductColorVM</response>
        [HttpPost("/product/color/upsert")]
        [ProducesResponseType(typeof(UpsertProductColorVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> UpsertAsync([FromBody] UpsertProductColorCommand model)
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
        /// Delete color
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">bool</response>
        [HttpDelete("/product/color/delete/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> DeactivateAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteProductColorCommand() { ProductColorId = id });
                return Ok();
            }
            catch (Exception ex)
            {
               // return BadRequest();

                 return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }

        /// <summary>
        /// Get single colors
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">ProductColorVM</response>
        [HttpGet("/product/color/{id}")]
        [ProducesResponseType(typeof(ProductColorVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetSingleAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new GetSingleProductColorQuery { ProductColorId = id });
                return Ok(response);
            }
            catch (Exception ex)
            {
                 return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }

        /// <summary>
        /// Get all colors
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">GetAllProductColorsVM</response>
        [HttpPost("/product/colors")]
        [ProducesResponseType(typeof(GetAllProductColorsVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetAllAsync([FromBody] GetAllProductColorsQuery model)
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
