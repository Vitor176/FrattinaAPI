using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using FrattinaAPI.Repository;

namespace FrattinaAPI
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
            services.AddScoped<IValidatePasswordRepository, ValidatePasswordRepository>();
            services.AddSingleton<Messages.Messages>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FrattinaAPI", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddLogging(loggingBuilder =>
            {
                try
                {
                    if (!Directory.Exists("./logs"))
                        Directory.CreateDirectory("./logs");


                    Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("./logs/", rollingInterval: RollingInterval.Day)
                    .CreateLogger();


                }
                catch (Exception e)
                {

                    throw;
                }


                loggingBuilder.AddSerilog();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FrattinaAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            try
            {

                app.UseExceptionHandler("/Logs/Error");
                app.UseHsts();
                app.UseSerilogRequestLogging();
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
