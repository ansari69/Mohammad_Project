using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiniShop.Web.Controllers
{
    public abstract class ApiController<TEntity> : ControllerBase
    {


        protected readonly IMediator _mediator;

        protected readonly ILogger<TEntity> _logger;

        protected ApiController(IMediator mediator, ILogger<TEntity> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


    }
}
