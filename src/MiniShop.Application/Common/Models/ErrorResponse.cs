using Microsoft.Extensions.Logging;
using MiniShop.Application.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Models
{
   public class ErrorResponse
    {
        public string ErrorMessage { get; set; }

        public ErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public static ErrorResponse Create<T>(Exception ex, ILogger<T> logger)
        {
            if (ex.Source == "Application")
            {
                logger.LogWarning("Handled Error Occured: {@ErrorMessage}\nStack Trace: {@StackTrace}",
                    ex.Message, ex.StackTrace);
                return new ErrorResponse(ex.Message);
            }

            logger.LogError("Unhandled Erro Occured: {@ErrorMessage}\nStack Trace: {@StackTrace}",
             ex.Message, ex.StackTrace);

            return new ErrorResponse(ErrorMessages.UnknownError);
        }
    }
}
