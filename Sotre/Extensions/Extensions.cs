using Domian.Contercts;
using Domian.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Identity;
using Services;
using Shared;
using Shared.ErrorsModeLs;
using Sotre.Middlewares;
using System.Text;

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

            services.AddApplicationServices(configuration);
            services.Configureservice();
            services.AddIdentityService();
            services.ConfagerJwtService(configuration);
            return services;
        }

        private static IServiceCollection AddBui1tInService(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        } 
        private static IServiceCollection ConfagerJwtService(this IServiceCollection services , IConfiguration configuration)
        {
            var JwtOptions = configuration.GetSection("JWtoptions").Get<Jwtoption>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = JwtOptions.issuer,
                    ValidAudience = JwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecuretyKey)),

                };
            });
            return services;

        }

        private static IServiceCollection Addswaggerservices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
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
            await DbIInitializer.InitializeIdentityAsync();

            return app;

        }

    }
}
