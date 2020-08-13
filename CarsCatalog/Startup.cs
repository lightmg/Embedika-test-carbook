using CarsCatalog.Db;
using CarsCatalog.Helpers;
using CarsCatalog.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarsCatalog
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEntityStorage, FileEntityStorage>(s => 
                new FileEntityStorage(PathHelpers.GetPath("storage.json")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            var httpsLogsPath = PathHelpers.GetPath("logs", "http");
            app.UseMiddleware<RequestResponseLoggingMiddleware>(httpsLogsPath);
            app.UseMiddleware<HttpStatisticsMiddleware>(httpsLogsPath);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}");
            });
        }
    }
}