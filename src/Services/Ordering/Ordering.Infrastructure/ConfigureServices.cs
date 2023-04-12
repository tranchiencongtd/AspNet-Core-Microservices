using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
  public static class ConfigureServices
  {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
    {
      services.AddDbContext<OrderContext>(options =>
      {
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"), builder => builder.MigrationsAssembly(typeof(OrderContext).Assembly.FullName)); // Dua migration vao project co assembly la ordercontext
      });

      services.AddScoped<OrderContextSeed>();

      return services;
    }
  }
}
