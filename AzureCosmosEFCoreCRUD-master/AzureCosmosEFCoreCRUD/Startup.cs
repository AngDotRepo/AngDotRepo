using AzureCosmosEFCoreCRUD.DBContext;
using AzureCosmosEFCoreCRUD.HubConfig;
using AzureCosmosEFCoreCRUD.Repository;
using AzureCosmosEFCoreCRUD.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCosmosEFCoreCRUD
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
            services.AddSignalR();

            #region Inject DB Context
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("dBConnection")));
            #endregion


            //var accountEndpoint = Configuration.GetValue<string>("CosmosDb:Account");
            //var accountKey = Configuration.GetValue<string>("CosmosDb:Key");
            //var dbName = Configuration.GetValue<string>("CosmosDb:DatabaseName");
            //services.AddDbContext<ApplicationDbContext>(x => x.UseCosmos(accountEndpoint, accountKey, dbName));
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200")
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials());
            //});
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AzureCosmosEFCoreCRUD", Version = "v1" });
            });

            #region Inject Repository
            services.AddScoped<IAzureEFRepository, AzureEFRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AzureCosmosEFCoreCRUD v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<VideogameHub>("/Videogame");
            });

        }
    }
}
