using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NitecoTest.Configs;
using NitecoTest.Context;
using NitecoTest.Helper;
using NitecoTest.Middleware;
using NitecoTest.Persistence;

namespace NitecoTest
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
            var jwtConfig = new JwtConfig(Configuration["SSO:SecreteKey"], Configuration["SSO:Issuer"], Configuration["SSO:Audience"], int.Parse(Configuration["SSO:TimeLife"]));
            var rdsConfiguration = new RdsConfiguration(Configuration["Database:Server"], Configuration["Database:DatabaseName"], Configuration["Database:Username"], Configuration["Database:Password"], Configuration["Database:Options"]);


            services.AddRazorPages();

            services.AddDbContext<AppDataContext>(o =>
            {
                o.UseSqlServer(rdsConfiguration.ConnectionString);
                o.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                o.EnableSensitiveDataLogging(true);
            });

            services.AddTransient<IPersistenceFactory, PersistenceFactory>(b => new PersistenceFactory(jwtConfig));

            // DI
            services.DIRegister();

            services.AddSession();

            services.AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["SSO:Issuer"],
                        ValidAudience = Configuration["SSO:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SSO:SecreteKey"]))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<CustomLogging>();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); //Routes for pages
                endpoints.MapControllerRoute("Default", "{controller=Login}/{action=Index}/{id?}"); //Routes for my API controllers
            });
        }
    }
}
