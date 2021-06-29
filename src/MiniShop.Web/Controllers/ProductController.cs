using System;
using System.Collections.Generic;
using System.IO;
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
using MiniShop.Application.Products.Commands.AddImageForProduct;
using MiniShop.Application.Products.Commands.ConfirmProductByManager;
using MiniShop.Application.Products.Commands.InsertProduct;
using MiniShop.Application.Products.Commands.UpsertProductLike;
using MiniShop.Application.Products.Queries.GetForInsertProduct;
using MiniShop.Application.Products.Queries.GetImageProduct;
using MiniShop.Application.Products.Queries.GetProductsByFilter;
using MiniShop.Application.Products.Queries.GetProductsForManager;
using MiniShop.Application.Products.Queries.GetSingleProduct;
using MiniShop.Infrastructure.Security.Stores;

namespace MiniShop.Web.Controllers
{
    [ApiController]
    public class ProductController : ApiController<ProductController>
    {

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
            : base(mediator, logger)
        { }


        /// <summary>
        /// Insert Product
        /// </summary>
        /// <remarks>Claims: FullAccess, Operator</remarks>
        /// <response code="200">InsertProductVM</response>
        [HttpPost("/product/insert")]
        [ProducesResponseType(typeof(InsertProductVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Shared.Operator)]
        public async Task<IActionResult> UpsertAsync([FromBody] InsertProductCommand model)
        {
            try
            {
                model.CreatorId = User.FindFirst("Identifier").Value;
                var response = await _mediator.Send(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }


        /// <summary>
        /// Confirm Product By Manager 
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">bool</response>
        [HttpPut("/product/confirm/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> ConfirmByManagerAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new ConfirmProductByManagerCommand() 
                { 
                    ProductId = id 
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }

        /// <summary>
        /// Upsert Product Like 
        /// </summary>
        /// <remarks>Claims: FullAccess,Operator,Simple</remarks>
        /// <response code="200">bool</response>
        [HttpPost("/product/upsert/like")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.SimpleUser.Simple)]
        public async Task<IActionResult> UpsertProductLikeAsync([FromBody] UpsertProductLikeCommand model)
        {
            try
            {
                model.CreatorId = User.FindFirst("Identifier").Value;
                var response = await _mediator.Send(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }


        /// <summary>
        /// Get Products By Filter 
        /// </summary>
        /// <response code="200">GetProductsByFilterVM</response>
        [HttpPost("/products")]
        [ProducesResponseType(typeof(GetProductsByFilterVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsByFilterAsync([FromBody] GetProductsByFilterQuery model)
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
        /// Get Products for manager 
        /// </summary>
        /// <remarks>Claims: FullAccess</remarks>
        /// <response code="200">GetProductsForManagerVM</response>
        [HttpPost("/products/manager")]
        [ProducesResponseType(typeof(GetProductsForManagerVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Base.AdminPanel)]
        public async Task<IActionResult> GetProductsForManagerAsync([FromBody] GetProductsForManagerQuery model)
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
        /// Get Single Product
        /// </summary>
        /// <response code="200">GetSingleProductVM</response>
        [HttpGet("/product/{id}")]
        [ProducesResponseType(typeof(GetSingleProductVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleProductAsync([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new GetSingleProductQuery()
                {
                    ProductId = id
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }


        /// <summary>
        /// Get for insert Product
        /// </summary>
        /// <response code="200">GetForInsertProductVM</response>
        [HttpGet("/product/insert")]
        [ProducesResponseType(typeof(GetProductsForManagerVM), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Shared.Operator)]
        public async Task<IActionResult> GetForInsertProductAsync()
        {
            try
            {
                var response = await _mediator.Send(new GetForInsertProductQuery()
                {
                    
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }



        /// <summary>
        /// Upload file for Product
        /// </summary>
        /// <remarks>Claims: FullAccess, Operator</remarks>
        /// <response code="200">bool</response>
        [HttpPut("/product/insert/image/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApplicationPolicyStore.Shared.Operator)]
        public async Task<IActionResult> Upsert1Async(IFormFile files,[FromRoute] string id)
        {
            try
            {
                var model = new AddImageForProductCommand
                {
                    ProductId = id,
                    Files = files
                };
                var response = await _mediator.Send(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }






        /// <summary>
        /// Get Single Product
        /// </summary>
        /// <response code="200">GetSingleProductVM</response>
        [HttpGet("/product/image/{id}")]
        [ProducesResponseType(typeof(GetSingleProductVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleProduct111Async([FromRoute] string id)
        {
            try
            {
                var response = await _mediator.Send(new GetImageProductQuery()
                {
                    ProductId = id
                });

                if(response == null)
                {
                    return Ok(response);
                }
                else
                {
                    var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "ImagesFolder/" + response);
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                    return File(fileBytes, "image/jpeg", response);
                }


             //   return Ok(response);


            }
            catch (Exception ex)
            {
                return BadRequest(ErrorResponse.Create(ex, _logger));
            }
        }


    }
}
