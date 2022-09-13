using Core.Application;
using Hangfire;
using Infrastructure.Database;
using LoansAPI.Extensions;
using LoansAPI.Middlewares;
using LoansAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;


namespace LoansAPI
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
            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Presentation.WebApi", Version = "v1" });

                  var jwtSecurityScheme = new OpenApiSecurityScheme
                  {
                      Scheme = "bearer",
                      In = ParameterLocation.Header,
                      Type = SecuritySchemeType.Http,
                      Description = "ქვედა ტექსტბოქსში ჩაწერეთ *_მხოლოდ_* თქვენი token !",

                      Reference = new OpenApiReference
                      {
                          Id = JwtBearerDefaults.AuthenticationScheme,
                          Type = ReferenceType.SecurityScheme
                      }
                  };
                  c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                  c.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                    { jwtSecurityScheme, Array.Empty<string>() }
                  });

              });
            services.AddControllers();
            services.AddDbLayer(Configuration);
            services.AddAppLayer(Configuration);
            services.AddJwtAuthenticationConfigs(Configuration);
            services.AddJwtAuthorizationConfigs();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation.LoansAPI v1"));

            app.UseMiddleware<LoggingMiddlewares>();


            app.UseHttpsRedirection();
            

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}


