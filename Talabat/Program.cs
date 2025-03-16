using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
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
            //.AddJsonOptions(
            //options => 
            //    options.JsonSerializerOptions.ReferenceHandler = 
            //        System.Text.Json.Serialization.ReferenceHandler.Preserve
            //        );

        builder.Services
            .AddStoreDbContext(builder.Configuration)
            .AddGenericRepo()
            .AddSpecificService()
            .AddMapper()
            .AddResolvers()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AppPolicy", options =>
            {
                options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });

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

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = false;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:Audience"],
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT:Issure"],
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = 
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration.GetSection("JWT")["SecurityKey"] 
                            ?? "asljdjklsahdjkshddasjkhdksa"))

            };

        });

        builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        builder.Services.AddScoped(typeof(IOrderService),typeof(OrderService));
        builder.Services.AddScoped(typeof(IProductService),typeof(ProductService));
        builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
        builder.Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

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

        app.MapControllers();

        app.UseCors("AppPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        app.Run();
    }
}
