using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Memory;
namespace PE2022test

{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseSession();
            app.UseMvc(routes =>
              {
                  routes.MapRoute(
                      name: "default",
                      template: "{controller=Home}/{action=BookIndex}/{id?}");
              });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
        }
    }
}
