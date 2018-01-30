using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using RogueIdentity.ApiDbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RogueIdentity
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
            //new---way check appsetting json file
            services
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("Sqlite")));

            // ===== Add our DbContext ======== only use for other sql providers
           // services.AddDbContext<ApplicationDbContext>();

            // ===== Add Identity ========
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();




            //// ===== Add Jwt Authentication ======== Comment out untill adding Identity
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        //for prod settings check in notes

                        //--------------testing Dev // my localh  = http://localhost:51237/
                        ValidIssuer = " http://localhost:51237/",
                        ValidAudience = " http://localhost:51237/",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Dont put here this is my custom Secret key for authnetication")),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });




            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //use authenitcation
            app.UseAuthentication();
            app.UseMvc();

            // ===== Create tables ======
            dbContext.Database.EnsureCreated();
        }
    }
}
//uses code from Identity aspcore 
//medium article https://medium.com/@lugrugzo/asp-net-core-2-0-webapi-jwt-authentication-with-identity-mysql-3698eeba6ff8
//+ https://github.com/lugrugzo/WebApiJwt/blob/master/appsettings.json
//+ validate tokens https://stormpath.com/blog/token-authentication-asp-net-core

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ prod settings
//put in  app.setting.config--------PRODuction
//{
//  "JwtKey": "SOME_RANDOM_KEY_DO_NOT_SHARE",
//  "JwtIssuer": "http://yourdomain.com",
//  "JwtExpireDays": 30
//}
//
//put in cnfig file PRODuction
//ValidIssuer = Configuration["JwtIssuer"],
//ValidAudience = Configuration["JwtIssuer"],
// IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
//ClockSkew = TimeSpan.Zero // remove delay of token when expire
