using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RSoft.Lib.Common.Web.Extensions;
using RSoft.Lib.Web.Extensions;
using RSoft.Mail.Business.IoC;
using RSoft.Mail.Web.Grpc.Host.Extensions;

namespace RSoft.Mail.Web.Grpc.Host
{

    /// <summary>
    /// Provides methods to setup-up start-up application
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Create a new Startup instance
        /// </summary>
        /// <param name="configuration">Configuration object instance</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration object
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection object instance</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            services.AddJwtToken(Configuration);
            services.AddMailServices(Configuration);
            services.AddHealthChecks();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder object instance</param>
        /// <param name="env">IWebHostEnvironment object instance</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureLangague();
            app.UseApplicationHealthChecks();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapServices();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
