var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DynamicObjectCreationApp_Api>("dynamicobjectcreationapp-api");

builder.Build().Run();
