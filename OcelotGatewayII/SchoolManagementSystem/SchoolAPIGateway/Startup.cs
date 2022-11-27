using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;

namespace SchoolAPIGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Azure AD
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
            {
                jwtOptions.Audience = "fae54d7a-4774-4a7c-855f-2cecff201135";
                jwtOptions.Authority = "https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47";
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.Events = new JwtBearerEvents { OnAuthenticationFailed = AuthenticationFailed, OnTokenValidated = AuthenticationTokenValidated };
                jwtOptions.TokenValidationParameters.ValidIssuer = "https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47/v2.0";
                jwtOptions.TokenValidationParameters.ValidateAudience = false;
                jwtOptions.TokenValidationParameters.ValidateIssuer = false;
            });

        services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, jwtOptions => {
            jwtOptions.Events = new JwtBearerEvents { OnAuthenticationFailed = AuthenticationFailed, OnTokenValidated = AuthenticationTokenValidated };
        });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            //.AddMicrosoftIdentityWebApi(Configuration,"AzureAD");
            //services.AddProtectedWebApi(Configuration).AddProtectedApiCallsWebApis(Configuration).AddInMemoryTokenCaches();

            // Call Ocelot
            services.AddOcelot(Configuration);
            services.AddControllers();

            IdentityModelEventSource.ShowPII = true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot().Wait();
        }

        private Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationFailed: {arg.Exception.Message}";
            arg.Response.ContentLength = s.Length;

            //uncomment this out to view error on token
            //arg.Response.Body.Write(System.Text.Encoding.UTF8.GetBytes(s), 0, s.Length);
            return Task.FromResult(0);
        }

        private Task AuthenticationTokenValidated(TokenValidatedContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationTokenValidated: {arg.Result}";
            return Task.FromResult(0);
        }
    }
}