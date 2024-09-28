using DynamicObjectCreationApp.Api.Extensions;
using Serilog;
using System.Reflection;
using FluentValidation;
using DynamicObjectCreationApp.Infracture.Concrete;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();
  
    builder.Services.ConfigureController();
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.RedisCacheSettings(builder.Configuration);
    //builder.Services.AddValidatorsFromAssemblyContaining<AddDynamicObjectDtoValidator>();
    builder.Services.DbContextConfiguration(builder.Configuration);
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.ServiceLifetime();
    builder.Services.RateLimitingSettings();
    builder.Services.AddMediatR(_ => _.RegisterServicesFromAssembly(Assembly.Load("DynamicObjectCreationApp.Application")));
    var app = builder.Build();

    app.MapDefaultEndpoints();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An exception happened while project was started.");

}
finally
{
    Log.CloseAndFlush();

}