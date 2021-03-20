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
using Microsoft.EntityFrameworkCore;
using ReversiRestApi.DAL;
using ReversiRestApi.Interfaces;
using ReversiRestApi.Models;
using ReversiRestApi.Models.Databse;

namespace ReversiRestApi
{
    public class Startup
    {
        private readonly string MyAllowedSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MSSQL")));

            var dbContext = services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
            services.AddSingleton(typeof(IGameRepository), new GameAccessLayer(dbContext));
            //services.AddSingleton<IGameRepository, GameRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowedSpecificOrigins, builder =>
                {
                    builder.WithOrigins("https://localhost:44324", "http://localhost:5500", "http://127.0.0.1:5500", "https://localhost:5001/api/game/move")
                        .WithMethods("GET", "POST", "PUT")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowedSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
