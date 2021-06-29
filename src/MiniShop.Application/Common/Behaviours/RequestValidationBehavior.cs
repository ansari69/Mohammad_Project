﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

using MediatR;
using Microsoft.Extensions.Logging;
using MiniShop.Application.Common.Exceptions;
using Newtonsoft.Json;

namespace MiniShop.Application.Common.Behaviours
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<TRequest> _logger;


        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            //if (_validators.Any())
            //{

            //    var context = new ValidationContext(request);

            //    var failures = _validators
            //        .Select(v => v.Validate(context))
            //        .SelectMany(result => result.Errors)
            //        .Where(f => f != null)
            //        .ToList();

            //    if (failures.Count != 0)
            //    {
            //        _logger.LogError("{Fails}", failures);
            //        throw new EntryValidationException();

            //    }
            //}

            return next();
        }
    }
}
