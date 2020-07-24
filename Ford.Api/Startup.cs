using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ford.Api.Middlewares;
using Ford.Core;
using Ford.Core.Abstracts;
using Ford.Core.Abstracts.Services;
using Ford.DataAccess;
using Ford.Security.Bearer;
using Ford.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Ford.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string connectionString =
                "Server=tcp:ford-test.database.windows.net,1433;Initial Catalog=ford-test;Persist Security Info=False;User ID=ford-admin;Password=frd31415.FD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            
            
            services.AddDbContext<FordDbContext>(options => options.UseSqlServer(connectionString));
            
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "Fiver.Security.Bearer",
                        ValidAudience = "Fiver.Security.Bearer",
                        IssuerSigningKey = JwtSecurityKey.Create("fiver-secret-key"),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("username", policy => policy.AddRequirements(new UserAuthorizeRequirement()));
            });

            services.AddMvc();
            
             services.AddScoped<IUserRepository, UserRepository>();
             services.AddScoped<IVehicleRepository, VehicleRepository>();
             services.AddScoped<IUserVehicleRepository, UserVehicleRepository>();
             services.AddScoped<IUserService, UserService>();
             services.AddScoped<IVehicleService, VehicleService>();
             
             
             ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());

             return services.BuildServiceProvider();
             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseAuthentication();
            
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}