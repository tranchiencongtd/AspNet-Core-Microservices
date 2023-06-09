﻿using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Common;

namespace Basket.API.Extensions
{
  public static class ServiceExtensions
  {
    public static IServiceCollection ConfigureServices(this IServiceCollection services) =>
      services.AddScoped<IBasketRepository, BasketRepository>()
        .AddTransient<ISeriallizeService, SeriallizeService>()
      ;

    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
      var redisConnectionString = configuration.GetSection("CacheSettings:ConnectionString").Value;
      if(string.IsNullOrEmpty(redisConnectionString))
        throw new ArgumentException("Redis connection string is not configured");

      //Redis configuration
      services.AddStackExchangeRedisCache(options =>
      {
        options.Configuration = redisConnectionString;
      });
    }
  }
}
