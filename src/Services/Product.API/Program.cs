using Common.Logging;
using Product.API.Extensions;
using Product.API.Persistences;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

Log.Information($"Starting {builder.Environment.ApplicationName} up");

try
{
  builder.Host.UseSerilog(Serilogger.Configure);
  builder.Host.AddAppConfigurations();
  // Add services to the container.
  builder.Services.AddInfrastructure(builder.Configuration);

  var app = builder.Build();

  app.UseInfrastructure();
  app.MigrateDatabase<ProductContext>((context, _) =>
  {
    ProductContextSeed.SeedProductAsync(context, Log.Logger).Wait();
  }).Run();

  app.Run();
}
catch (Exception ex)
{
  string type = ex.GetType().Name;
  if (type.Equals("HostAbortedException", StringComparison.Ordinal)) throw;

  Log.Fatal(ex, $"Unhandlerd exception: {ex.Message}");
}
finally
{
  Log.Information($"Shut down {builder.Environment.ApplicationName} complete");
  Log.CloseAndFlush();
}

