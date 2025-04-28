using Domian.Contercts;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;
using Shared.ErrorsModeLs;
using Sotre.Middlewares;

namespace Sotre.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection ReisterA11Services(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBui1tInService();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            services.Addswaggerservices();

            services.AddInfrastructureServices(configuration);

            services.AddApplicationServices();

            services.Configureservice();

            return services;
        }

        private static IServiceCollection AddBui1tInService(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        } 
        private static IServiceCollection Addswaggerservices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection Configureservice(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var Errors = ActionContext.ModelState.Where(m => m.Value.Errors.Any())
                     .Select(m => new ValidationError()
                     {
                         Field = m.Key,
                         Errors = m.Value.Errors.Select(Errors => Errors.ErrorMessage)
                     });

                    var response = new ValidationErrorResponse()
                    {
                        Errors = Errors.ToList(),
                    };
                    return new BadRequestObjectResult(response);


                };
            });


            return services;
        }

        public static async Task<WebApplication> Configuremiddlewares(this WebApplication app)
        {
            await app.InitializeDatabaseAsync();

            app.UseMiddleware<GlobalErrorHandLingMiddLeware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            return app;

        }
        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DbIInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbIInitializer.InitializeAsync();

            return app;

        }

    }
}
