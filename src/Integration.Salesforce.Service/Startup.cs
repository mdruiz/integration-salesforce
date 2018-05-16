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
        public void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
        {
            // Configuration for reading from appsettings
            services.AddOptions();
            List<string> Strings = new List<string>();

            if (env.IsStaging())
            {
                Strings.Add(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"));
                Strings.Add(Environment.GetEnvironmentVariable("MONGODB_DATABASE"));
                Strings.Add(Environment.GetEnvironmentVariable("SALESFORCE_CLIENT_ID"));
                Strings.Add(Environment.GetEnvironmentVariable("SALESFORCE_CLIENT_SECRET"));
                Strings.Add(Environment.GetEnvironmentVariable("SALESFORCE_PASSWORD"));
                Strings.Add(Environment.GetEnvironmentVariable("SALESFORCE_URLS_LOGIN_URL"));
                Strings.Add(Environment.GetEnvironmentVariable("SALESFORCE_URLS_RESOURCE_URL_EXTENSION"));
                Strings.Add(Environment.GetEnvironmentVariable("SALESFORCE_USERNAME"));
            }

            else
            {
                Strings.Add(Configuration.GetSection("MongoDB:ConnectionString").ToString());
                Strings.Add(Configuration.GetSection("MongoDB:Database").ToString());
                Strings.Add(Configuration.GetSection("Salesforce:ClientId").ToString());
                Strings.Add(Configuration.GetSection("Salesforce:ClientSecret").ToString());
                Strings.Add(Configuration.GetSection("Salesforce:UserName").ToString());
                Strings.Add(Configuration.GetSection("Salesforce:Password").ToString());
                Strings.Add(Configuration.GetSection("SalesforceURLs:LoginUrl").ToString());
                Strings.Add(Configuration.GetSection("SalesforceURLs:ResourceUrlExtension").ToString());
            }

            Settings sModel = new Settings(Strings);
            services.AddSingleton(sModel);

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