﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace UserServiceAPI.WebAPI.CustomExceptionsFilter
{
    /// <summary>
    /// Класс, реализующий фильр исключений.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode;
            string errorMessage;

            switch (context.Exception)
            {
                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = context.Exception.Message;
                    break;
                case KeyNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    errorMessage = "not found";
                    break;
                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorMessage = "Access is denied";
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage = "unexpected error";
                    break;
            }
            var response = new JsonResult(new { error = errorMessage })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }

    }
}
