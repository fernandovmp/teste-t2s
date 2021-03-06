using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using TesteT2S.WebApi.Configuration;
using TesteT2S.WebApi.Data;
using TesteT2S.WebApi.Features.Containers.Mappers;
using TesteT2S.WebApi.Features.Report.Data;

namespace TesteT2S.WebApi
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
                .AddControllers()
                .AddFluentValidation(fluentValidations => fluentValidations
                    .RegisterValidatorsFromAssemblyContaining<Startup>()); ;
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TesteT2S.WebApi",
                    Version = "v1"
                });
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.AddFluentValidationRules();
            });

            string connectionString = Configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<ContainerContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.LogTo(Console.WriteLine);
            });
            services.AddTransient<IDbConnection>(servicesProvides => new SqlConnection(connectionString));
            services.AddScoped<IReportRepository, ReportRepository>();

            services.AddAutoMapper(typeof(ContainerProfile));

            var corsOptions = new ApplicationCorsOptions();
            Configuration.Bind("CorsOptions", corsOptions);
            services
                .Configure<ApplicationCorsOptions>(Configuration.GetSection("CorsOptions"))
                .AddCors(options =>
                {
                    options.AddPolicy(corsOptions.PolicyName, configurePolicy =>
                    {
                        configurePolicy
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(corsOptions.AllowedOrigin);
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

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "docs";
                options.SwaggerEndpoint("/docs/v1/swagger.json", "TesteT2S.WebApi v1");
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            ApplicationCorsOptions corsOptions = app.ApplicationServices
                .GetService<IOptions<ApplicationCorsOptions>>().Value;
            app.UseCors(corsOptions.PolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
