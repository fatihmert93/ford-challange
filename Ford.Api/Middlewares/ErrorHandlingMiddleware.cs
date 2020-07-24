using System;
using System.Net;
using System.Threading.Tasks;
using Ford.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;

namespace Ford.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
            //_exceptionManager = DependencyService.GetService<IExceptionManager>();
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                //_exceptionManager.Handle(ex);

            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            else if (exception is BadHttpRequestException) code = HttpStatusCode.BadRequest;
            else
            {
                code = HttpStatusCode.BadRequest;

            }

            ResponseModel responseModel = new ResponseModel { Status = false, ErrorMessage = exception.Message, StatusCode = (int)HttpStatusCode.BadRequest };

            var result = JsonConvert.SerializeObject(responseModel);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}