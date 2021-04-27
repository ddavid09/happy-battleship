using HappyBattleship.Library;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;


namespace HappyBattleship.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:3000");
                });
            });

            services.AddSignalR();

            services.AddTransient<IPlayer, Player>();
            services.AddTransient<IShootingStrategy, RandomShootingStrategy>();
            services.AddTransient<IFleetCreator, RandomFleetCreator>();
            services.AddTransient<IBoard, Board>();
            services.AddTransient<ISimulation, HappyBattleshipWebSimulation>();

            services.AddSingleton<ISimulationRepository, SimulationRepository>();
            services.AddSingleton<ILogger>(s =>
            {
                return new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .CreateLogger();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<HappyBattleshipSimulationHub>("/battleship");
            });


        }
    }
}
