using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Ordering.Application.Common.Behaviours;

namespace Ordering.Application
{
  public static class ConfigureServices
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
      services.AddAutoMapper(Assembly.GetExecutingAssembly())
        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
        .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
      ;
  }
}
