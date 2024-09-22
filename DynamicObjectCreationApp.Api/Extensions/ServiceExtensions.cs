
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture;
using DynamicObjectCreationApp.Infracture.Abstract;
using DynamicObjectCreationApp.Infracture.Concrete;
using Microsoft.EntityFrameworkCore;

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

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DynamicContext>(options =>
            {
                options.UseInMemoryDatabase(configuration.GetConnectionString("InMemoryDb"));
            });
        }


        public static void ServiceLifetime(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDynamicObjectDal, DynamicObjectDal>();
        }

        public static void RedisCacheSettings(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisOutputCache(options =>
            {
                options.Configuration = config.GetConnectionString("Redis");
                options.InstanceName = "DynamicObjectCreationApp";
            });



        }
    }
}
