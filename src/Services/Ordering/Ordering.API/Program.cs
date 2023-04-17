using Common.Logging;
using Contracts.Common.Interfaces;
using Contracts.Messages;
using Infrastructure.Common;
using Infrastructure.Messages;
using Ordering.API.Extensions;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Starting Ordering API up");

try
{
  // Add services to the container.
  builder.Host.AddAppConfigurations();
  builder.Services.AddConfigurationSettings(builder.Configuration);
  builder.Services.AddApplicationServices();
  builder.Services.AddInfrastructureServices(builder.Configuration);
  builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();
  builder.Services.AddScoped<ISeriallizeService, SeriallizeService>();

  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI();
  }

  // Initialize and seed database
  using(var scope = app.Services.CreateScope())
  {
    var orderContextSeed = scope.ServiceProvider.GetRequiredService<OrderContextSeed>();
    await orderContextSeed.InitializeAsync();
    await orderContextSeed.SeedAsync();
  }

  //app.UseHttpsRedirection();

  app.UseAuthorization();

  app.MapControllers();

  app.Run();
}
catch (Exception ex)
{
  string type = ex.GetType().Name;
  if (type.Equals("HostAbortedException", StringComparison.Ordinal)) throw;

  Log.Fatal(ex, "Unhandlerd exception");
}
finally
{
  Log.Information("Shut down Ordering API complete");
  Log.CloseAndFlush();
}

