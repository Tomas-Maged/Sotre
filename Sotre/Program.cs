using Domian.Contercts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Data.Repository;
using Services;
using Services.Abstractions;
using Shared.ErrorsModeLs;
using Sotre.Extensions;
using Sotre.Middlewares;
using System.Threading.Tasks;

namespace Sotre
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.ReisterA11Services(builder.Configuration);

            var app = builder.Build();

            await app.Configuremiddlewares();
            app.Run();
        }
    }
}
