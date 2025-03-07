using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Core.Specifications;
using Talabat.Extentions;
using Talabat.Helpers;
using Talabat.Middlewares;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;
namespace Talabat;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        builder.Services
            .AddStoreDbContext(builder.Configuration)
            .AddGenericRepo()
            .AddSpecificService()
            .AddMapper()
            .AddResolvers()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        builder.Services.AddSingleton<IConnectionMultiplexer>(
                serviceProvicer => {
                    var connection = builder.Configuration.GetConnectionString("Redis") ?? "localhost";
                    return ConnectionMultiplexer.Connect(connection);
                });

        builder.Services.AddScoped(typeof(ICartRepository), typeof(CartRepository));
        builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityCS")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

        builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        builder.Services.AddScoped(typeof(IOrderService),typeof(OrderService));
        builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));

        builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        await app.UseSeedingData(app.Services);

        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseStaticFiles(); // must be used here

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
