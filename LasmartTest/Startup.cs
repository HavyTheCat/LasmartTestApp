using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LasmartTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using LasmartTest.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LasmartTest.Services;

namespace LasmartTest
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<LasmartTestContext>();

            services.AddAuthentication()
             .AddCookie()
             .AddJwtBearer(cfg =>
             {
                 cfg.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidIssuer = _config["Tokens:Issuer"],
                     ValidAudience = _config["Tokens:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                 };
             }
                 );

            services.AddMvc();

            var connectionString = _config["connectionStrings:LasmartTestDBConnectionString"];
            services.AddDbContext<LasmartTestContext>(o => o.UseSqlServer(connectionString));

            services.AddTransient<LasmartTestSeeder>();

            services.AddScoped<IEquipmentRepository, EquipmentRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            LasmartTestContext lasmartTestContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happend. sorry");
                    });
                });
            }

            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { Controller = "App", Action = "Index" });
            });


            if (env.IsDevelopment())
            {
                //Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<LasmartTestSeeder>();
                    seeder.Seed().Wait();
                }
            }
        }
    }
}
