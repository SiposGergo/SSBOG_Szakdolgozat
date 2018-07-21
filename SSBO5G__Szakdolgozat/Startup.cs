using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using SSBO5G__Szakdolgozat.Services;
using AutoMapper;
using SSBO5G__Szakdolgozat.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Data.Common;

namespace SSBO5G__Szakdolgozat
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // email username and password
            var emailSettingsSection = Configuration.GetSection("EmailSettings");
            var emailSettings = emailSettingsSection.Get<EmailSettings>();
            var emailUserName = emailSettings.UserName;
            var emailPassword = emailSettings.Password;


            services.AddAutoMapper();
            services.AddScoped<IHikeService, HikeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<IEmailSender, EmailSender>((x) =>
                {
                    return new EmailSender(emailUserName, emailPassword);
                });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HikeX API", Version = "v1" });
            });

            services.AddMvc()
                .AddJsonOptions(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    x.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });


            if (Environment.IsDevelopment())
            {
                services.AddDbContext<ApplicationContext>(
                options => options.UseInMemoryDatabase("MyDb"));
            }
            //else
            {
                services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer("Data Source=tcp:hikexdbserver.database.windows.net,1433;Initial Catalog=hikex_sys_db;User Id=prosipinho@hikexdbserver;Password=hike1855X"));
            }

            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy("x",
                    builder => builder.WithOrigins("http://localhost:4242", "http://127.0.0:4242", "http://localhost:8080", "http://127.0.0:8080")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("WWW-Authenticate")
                    .AllowCredentials());
            });

            // Auth & JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = new TimeSpan(0, 0, 0)
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, ApplicationContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HikeX API");
            });

            app.UseStaticFiles();

            app.UseCors("x");

            app.UseMvc();
            app.UseAuthentication();


            if (env.IsDevelopment())
            {
                DbSeeder.FillWithTestData(context);
                DbSeeder.Fil2(context);
            }
        }
    }
}
