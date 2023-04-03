using Common.Logging;
using Customer.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Starting Customer API up");

try
{
  // Add services to the container.

  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
  builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));

  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI();
  }

  app.UseHttpsRedirection();

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
  Log.Information("Shut down Customer API complete");
  Log.CloseAndFlush();
}

