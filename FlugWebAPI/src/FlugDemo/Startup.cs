using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FlugDemo.Data;
using Microsoft.DotNet.InternalAbstractions;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.Swagger.Model;
using FlugDemo.Swagger;
using System.IdentityModel.Tokens.Jwt;
using Swashbuckle.SwaggerGen.Generator;

namespace FlugDemo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        private string GetXmlCommentsPath()
        {
            var env = PlatformServices.Default.Application;
            return Path.Combine(env.ApplicationBasePath, "FlugDemo.xml");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddTransient<IFlugRepository, FlugRepository>();

            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(GetXmlCommentsPath());

                
                var oauth2Options = new OAuth2Scheme
                {

                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "https://steyer-identity-server.azurewebsites.net/identity/connect/authorize",
                    Scopes = new Dictionary<string, string>
                    {
                        { "voucher", "Buy a travel-voucher" }
                    }
                };
                options.OperationFilter<AuthOperationFilter>();
                options.AddSecurityDefinition("oauth2", oauth2Options);
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                Authority = "https://steyer-identity-server.azurewebsites.net/identity",
                Audience = "https://steyer-identity-server.azurewebsites.net/identity/resources",
            });




            if (env.IsDevelopment()) { 
                app.UseDeveloperExceptionPage();
                app.UseDirectoryBrowser();
            }

            app.UseSwagger();
            app.UseSwaggerUi();
            
            app.UseMvc();
        }
    }
}
