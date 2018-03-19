using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaniWaniBlack.Data.DAL;
using KaniWaniBlack.Data.DAL.Interfaces;
using KaniWaniBlack.Services.Services;
using KaniWaniBlack.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using KaniWaniBlack.Data.Models;
using KaniWaniBlack.Helper.Services.HttpFactory;
using KaniWaniBlack.Helper.Services;

namespace KaniWaniBlack.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; //TODO: SSL
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime
        public void ConfigureServices(IServiceCollection services)
        {
            //Use JWT for authentication
            Logger.LogInfo("Starting configuration startup");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"].ToString()))
                    };
                });

            services.AddMvc();

            //KaniWaniBlack DB connection string
            string connectionString = Configuration["ConnectionString:KaniWaniBlackEntities"];
            services.AddDbContext<KaniWaniBlackContext>(x => x.UseSqlServer(connectionString));

            //Dependency injection
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<WaniKaniVocab>, GenericRepository<WaniKaniVocab>>();
            services.AddScoped<IGenericRepository<UserVocab>, GenericRepository<UserVocab>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICryptoService, CryptoService>();
            services.AddTransient<IWaniKaniService, WaniKaniService>();
            services.AddTransient<IHttpClientFactory, HttpClientFactory>();

            //TODO: restrict origin for prod
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });

            services.Configure<IISOptions>(x =>
            {
                x.ForwardClientCertificate = false;
                x.AutomaticAuthentication = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("customRoute", "api/{controller=User}/{action=LoginUser}");
            });
            app.UseCors("CorsPolicy");
        }
    }
}