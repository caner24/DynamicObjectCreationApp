var builder = DistributedApplication.CreateBuilder(args);


var cache = builder.AddRedis("cache", port: 6379).WithDataVolume();

builder.AddProject<Projects.DynamicObjectCreationApp_Api>("dynamicobjectcreationapp-api")
    .WithReference(cache);

builder.Build().Run();
