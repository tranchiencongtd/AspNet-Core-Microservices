using Common.Logging;
using Contracts.Common.Interfaces;
using Customer.API.Controllers;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

Log.Information($"Starting {builder.Environment.ApplicationName} up");


try
{
  // Add services to the container.
  builder.Host.UseSerilog(Serilogger.Configure);

  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
  builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));
  // Add inject
  builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
                  .AddScoped<ICustomerService, CustomerService>()
                  ;

  var app = builder.Build();

  app.MapGet("/", () => "Welcome to customer API");
  app.MapCustomersApi();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.jon", $"{builder.Environment.ApplicationName} v1"));
  }

  //app.UseHttpsRedirection(); //product only

  app.UseAuthorization();

  app.MapControllers();

  app.SeedCustomerData();

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
  Log.Information($"Shut down {builder.Environment.ApplicationName} complete");
  Log.CloseAndFlush();
}

