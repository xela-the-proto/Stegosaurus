using Stegosaurus.Shard;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
var options = new HostOptions().BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
var host = builder.Build();
host.Run();