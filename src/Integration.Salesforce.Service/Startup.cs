using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.Salesforce.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Integration.Salesforce.Service
{
    public class Startup
    {
        //TODO: get version from something more flexible
        string version = "v1";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration for reading from appsettings
            services.AddOptions();
            services.Configure<Settings>(Configuration.GetSection("MongoDB"));
            services.Configure<Settings>(Configuration.GetSection("Salesforce"));
            services.Configure<Settings>(Configuration.GetSection("SalesforceURLs"));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new Info { Title = "Revature Housing Salesforce API", Version = version });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsStaging())
            {
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Revature Housing Salesforce API {version}");
                c.RoutePrefix = string.Empty;
            });
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }
}
