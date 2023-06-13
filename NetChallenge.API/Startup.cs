using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NetChallenge.Application.Support;
using NetChallenge.Application.Support.Helpers;
using NetChallenge.Application.Support.Middleware;
using NetChallenge.Infrastructure.Support;

namespace NetChallenge.API
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
            services
                .AddControllers(o =>
                { 
                    o.Filters.Add(new HttpResponseExceptionFilter());
                })
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
                });
            
            services.AddHealthChecks();
            
            services.AddInfrastructure();
            services.AddApplication();

            services.AddSwaggerGen(c =>
            {
                c.MapType<TimeSpan>(() => new OpenApiSchema
                    {
                        Type = "string",
                        Format = @"hh\:mm\:ss",
                        Reference = null,
                        Example = new OpenApiString("hh:mm:ss"),
                        Nullable = false
                    }
                );
                
                c.SwaggerDoc("apidocs", new OpenApiInfo
                {
                    Title = "NetChallenge API",
                    Description = "Provides an interface to manage office rentals.",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UsePathBase("/api");

            app.UseRouting();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/apidocs/swagger.json", "NetChallenge API V1");
                c.RoutePrefix = "documentation";
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                endpoints.MapHealthChecks("health/ready", new HealthCheckOptions()
                {
                    Predicate = check => check.Tags.Contains("ready")
                });

                endpoints.MapHealthChecks("health/live", new HealthCheckOptions()
                {
                    Predicate = _ => false
                });
            });
        }
    }
}