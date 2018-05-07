using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceStack;

namespace Agency
{
    public abstract class Startup
    {
        private IConfiguration Configuration { get; }
        protected Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Register your ServiceStack AppHost as a .NET Core module
            app.UseServiceStack(new AppHost { 
                AppSettings = new NetCoreAppSettings(Configuration) // Use **appsettings.json** and config sources
            }); 
  
            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}