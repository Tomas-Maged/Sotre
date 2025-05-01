using Azure;
using Domian.Exceptions;
using Shared.ErrorsModeLs;
using System.ComponentModel.DataAnnotations;

namespace Sotre.Middlewares
{
    public class GlobalErrorHandLingMiddLeware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandLingMiddLeware> _logger;

        public GlobalErrorHandLingMiddLeware(RequestDelegate next, ILogger<GlobalErrorHandLingMiddLeware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            try
            {
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var responce = new ErrorDetaiLs()
                    {
                        StadusCode = StatusCodes.Status404NotFound,
                        ErrorMessage = $"End Point {context.Request.Path} is not found"
                    };
                    await context.Response.WriteAsJsonAsync(responce);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                var responce = new ErrorDetaiLs()
                {
                    ErrorMessage = ex.Message,
                };
                responce.StadusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                    ValidationException => HandleVa1idatioExceptionAsync((ValidationException)ex, responce),
                    _ => StatusCodes.Status500InternalServerError,
                };
                context.Response.StatusCode = responce.StadusCode;

                await context.Response.WriteAsJsonAsync(responce);
            }
        }
        private static Task HandelNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetaiLs()
            {
                StadusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = "Internal Server Error"
            };
            return context.Response.WriteAsJsonAsync(response);
        }

        private static int HandleVa1idatioExceptionAsync(ValidationException ex, ErrorDetaiLs responce)
        {
            // ValidationException does not have an Errors property. 
            // Replace this with a meaningful implementation or remove it if unnecessary.
            responce.Errors = new List<string> { ex.Message }; // Example: Add the exception message as an error.
            return StatusCodes.Status400BadRequest;
        }
    }
}
