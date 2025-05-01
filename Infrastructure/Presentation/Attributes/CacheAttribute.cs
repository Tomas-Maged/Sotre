using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int durationInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
         var CacheService = context.HttpContext.RequestServices.GetRequiredService<IServicesManager>().CacheService;
            var CahceKey = GenerateCacheKey(context.HttpContext.Request);
            var result = await  CacheService.GetCachValeuAsync(CahceKey);
            if (!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return; 
            } 
         var ContextResult = await next.Invoke();
            if (ContextResult.Result is OkObjectResult okObjectResult)
            { 
             await  CacheService.SetCacheValeuAsncy(CahceKey, okObjectResult.Value , TimeSpan.FromSeconds(durationInSec));
            }
        }
        private string GenerateCacheKey(HttpRequest request)
        {
            var Key = new StringBuilder();
            Key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q=>q.Key))
            {
                Key.Append($"|{item.Key}-{item.Value}");   
            }
            return Key.ToString();
        }

    }
}
