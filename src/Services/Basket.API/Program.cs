using Basket.API.Extensions;
using Common.Logging;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

Log.Information($"Starting {builder.Environment.ApplicationName} up");

try
{
  
  builder.Host.UseSerilog(Serilogger.Configure);
  builder.Host.AddAppConfigurations();

  // Add services to the container.
  builder.Services.ConfigureServices();
  builder.Services.ConfigureRedis(builder.Configuration);
  builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.jon", $"{builder.Environment.ApplicationName} v1"));
  }

  //app.UseHttpsRedirection(); // product only 

  app.UseAuthorization();

  app.MapControllers();

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

