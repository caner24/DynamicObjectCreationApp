

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


    }
}
