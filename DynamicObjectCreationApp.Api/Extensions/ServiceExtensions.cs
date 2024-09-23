
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture;
using DynamicObjectCreationApp.Infracture.Abstract;
using DynamicObjectCreationApp.Infracture.Concrete;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading.RateLimiting;
using DynamicObjectCreationApp.Presentation.ActionFilters;
using FluentValidation;
using DynamicObjectCreationApp.Entity.Dto;
using DynamicObjectCreationApp.Application.Validation.FluentValidation.Dynamic;
using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;

namespace DynamicObjectCreationApp.Api.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureController(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            })
 .AddApplicationPart(typeof(DynamicObjectCreationApp.Presentation.Controllers.DynamicObjectController).Assembly)
 .AddNewtonsoftJson(opt =>
     opt.SerializerSettings.ReferenceLoopHandling =
     Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }


        public static void DbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<DynamicContext>(options => options.UseMySql(connectionString, serverVersion, b => b.MigrationsAssembly("DynamicObjectCreationApp.Api").
            EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)));
        }


        public static void ServiceLifetime(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDynamicObjectDal, DynamicObjectDal>();

            services.AddScoped<IValidator<AddDynamicDataCommandRequest>, AddDynamicObjectDtoValidator>();
            services.AddScoped<IValidator<DeleteDynamicDataCommandRequest>, DeleteDynamicDataDtoValidator>();
            services.AddScoped<IValidator<UpdateDynamicDataCommandRequest>, UpdateDynamicDataValidator>();
            services.AddScoped<IValidator<GetDynamicDataByIdQueryRequest>, GetDynamicDataByIdDtoValidator>();

            services.AddScoped<ValidationFilterAttribute>();
        }

        public static void RedisCacheSettings(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisOutputCache(options =>
            {
                options.Configuration = config.GetConnectionString("Redis");
                options.InstanceName = "DynamicObjectCreationApp";
            });
        }

        public static void RateLimitingSettings(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter =
                PartitionedRateLimiter.Create<HttpContext, string>(
                    httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ??
                        httpContext.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 5,
                            QueueLimit = 2,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            Window = TimeSpan.FromMinutes(1)
                        }));
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        Log.Warning($"Rate limiting detected ");
                        await context.HttpContext.Response.WriteAsync(
                            $"Çok fazla istekde bulundunuz. Lütfen sonra tekrar deneyin {retryAfter.TotalMinutes} dakika. ", cancellationToken: token);
                    }
                    else
                    {
                        Log.Warning($"Rate limiting detected ");
                        await context.HttpContext.Response.WriteAsync(
                            "Çok fazla istekde bulundunuz. Lütfen sonra tekrar deneyin. ", cancellationToken: token);
                    }
                };
            });
        }
    }
}
