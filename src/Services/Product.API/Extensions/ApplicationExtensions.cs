﻿namespace Product.API.Extensions
{
  public static class ApplicationExtensions
  {
    public static void UseInfrastructure(this IApplicationBuilder app)
    {

      app.UseSwagger();
      app.UseSwaggerUI();

      app.UseRouting();
      //app.UseHttpsRedirection(); for product only

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}
