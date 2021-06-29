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
using MiniShop.Application.ProductTypes.Commands.DeleteProductType;
using MiniShop.Application.ProductTypes.Commands.UpsertProductType;
using MiniShop.Application.ProductTypes.Queries.GetAllProductTypes;
using MiniShop.Application.ProductTypes.Queries.GetSingleProductType;
using MiniShop.Infrastructure.Security.Stores;

namespace MiniShop.Web.Controllers
{
    [ApiController]
    public class ProductTypeController : ApiController<ProductTypeController>
    {
        public ProductTypeController(IMediator mediator, ILogger<ProductTypeController> logger)
        : base(mediator, logger)
        { }

        /// <summary>
        /// Upsert Product Type
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">UpsertProductTypeVM</response>
        [HttpPost("/product/type/upsert")]
        [ProducesResponseType(typeof(UpsertProductTypeVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> UpsertAsync([FromBody] UpsertProductTypeCommand model)
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
        /// Delete Product Type
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">bool</response>
        [HttpDelete("/product/type/delete/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> DeactivateAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteProductTypeCommand() { ProductTypeId = id });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }


        /// <summary>
        /// Get Single Product Type
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">ProductTypeVM</response>
        [HttpGet("/product/type/{id}")]
        [ProducesResponseType(typeof(ProductTypeVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetSingleAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new GetSingleProductTypeQuery { ProductTypeId = id });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }


        /// <summary>
        /// Get All Product Types
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">GetAllProductTypesVM</response>
        [HttpPost("/product/types")]
        [ProducesResponseType(typeof(GetAllProductTypesVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetAllAsync([FromBody] GetAllProductTypesQuery model)
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
