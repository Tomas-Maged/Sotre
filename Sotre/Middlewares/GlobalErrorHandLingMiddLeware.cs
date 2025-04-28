using Azure;
using Domian.Exceptions;
using Shared.ErrorsModeLs;

namespace Sotre.Middlewares
{
    public class GlobalErrorHandLingMiddLeware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandLingMiddLeware> _logger;

        public GlobalErrorHandLingMiddLeware(RequestDelegate next,ILogger<GlobalErrorHandLingMiddLeware> logger)
        {
            _next = next;
            _logger= logger;
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
                // 1. Set Status Code For Response
                // 2. Set Content Type Code For Response
                // 3. Response Object (Body)
                //  4.Return Response
                context.Response.ContentType = "application/json";
                var responce = new ErrorDetaiLs()
                {
                    ErrorMessage = ex.Message,
                };
                responce.StadusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError,
                };
                context.Response.StatusCode = responce.StadusCode;

                await context.Response.WriteAsJsonAsync(responce);
            }


        }
    }
}
