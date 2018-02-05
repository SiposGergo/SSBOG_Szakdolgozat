using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSBO5G__Szakdolgozat.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace SSBO5G__Szakdolgozat
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddMvc();

            services.AddDbContext<ApplicationContext>(
                options => options.UseInMemoryDatabase("myDatabse"));

            services.AddCors();

            services.AddCors(options =>
            {
                options.AddPolicy("x",
                    builder => builder.WithOrigins("http://localhost:4242", "http://127.0.0:4242", "http://localhost:8080", "http://127.0.0:8080")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationContext context)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticFiles();

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4242", "http://127.0.0:4242", "http://localhost:8080", "http://127.0.0.1:8080")
                .AllowAnyHeader().
                AllowAnyMethod().
                AllowCredentials());

            app.UseMvc();
            DbSeeder.FillWithTestData(context);
        }
    }
}
