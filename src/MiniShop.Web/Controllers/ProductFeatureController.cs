using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.Application.Common.Models;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Application.ProductFeatures.Commands.DeleteProductFeature;
using MiniShop.Application.ProductFeatures.Commands.UpsertProductFeature;
using MiniShop.Application.ProductFeatures.Queries.GetAllProductFeatures;
using MiniShop.Application.ProductFeatures.Queries.GetSingleProductFeature;
using MiniShop.Infrastructure.Security.Stores;

namespace MiniShop.Web.Controllers
{
    [ApiController]
    public class ProductFeatureController : ApiController<ProductFeatureController>
    {

        public ProductFeatureController(IMediator mediator, ILogger<ProductFeatureController> logger)
         : base(mediator, logger)
        { }


        /// <summary>
        /// Insert and update features for Products
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">UpsertProductFeatureVM</response>
        [HttpPost("/product/feature/upsert")]
        [ProducesResponseType(typeof(UpsertProductFeatureVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> UpsertAsync([FromBody] UpsertProductFeatureCommand model)
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
        /// Delete Product Feature 
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">bool</response>
        [HttpDelete("/product/feature/delete/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> DeactivateAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteProductFeatureCommand() { ProductFeatureId = id });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));

            }
        }


        /// <summary>
        /// Get single feature
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">ProductFeatureVM</response>
        [HttpGet("/product/feature/{id}")]
        [ProducesResponseType(typeof(ProductFeatureVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetSingleAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new GetSingleProductFeatureQuery { ProductFeatureId = id });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }


        /// <summary>
        /// Get All Product Features
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">GetAllProductFeaturesVM</response>
        [HttpPost("/product/features")]
        [ProducesResponseType(typeof(GetAllProductFeaturesVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetAllAsync([FromBody] GetAllProductFeaturesQuery model)
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
