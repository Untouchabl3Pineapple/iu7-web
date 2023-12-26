﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using db_cp.Models;
using db_cp.Services;
using db_cp.Interfaces;
using db_cp.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using db_cp.Logger;
using Microsoft.AspNetCore.Http;

using Microsoft.OpenApi.Models;

using db_cp.Utils;

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Collections.Generic;
using OpenTelemetry;
using System.Diagnostics;


namespace db_cp
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IConfiguration configuration;

        public Startup(IWebHostEnvironment hostEnv, IConfiguration configuration)
        {
            this.configuration = configuration;
            _configuration = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerProvider>(new LoggerProvider(configuration));
            services.AddLogging();
            services.AddMemoryCache(); // Добавляет службу кэширования

            services.AddDbContext<AppDBContext>(options => options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    });

            services.AddTransient<IButtonsEventsService, ButtonsEventsService>();
            services.AddTransient<IButtonsPostsService, ButtonsPostsService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IEventsTypesService, EventsTypesService>();
            services.AddTransient<IUsersService, UsersService>();

            services.AddTransient<IButtonsEventsRepository, ButtonsEventsRepository>();
            services.AddTransient<IButtonsPostsRepository, ButtonsPostsRepository>();
            services.AddTransient<IEventsRepository, EventsRepository>();
            services.AddTransient<IEventsTypesRepository, EventsTypesRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();

            services.AddControllersWithViews();

            // Регистрируйте Swagger-генератор
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tractor plant API", Version = "v1" });
                c.OperationFilter<RoleDescriptionOperationFilter>();

            });

            services.AddOpenTelemetry()
            .WithTracing(
                builder => builder

                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()

                .AddConsoleExporter()
                .AddOtlpExporter() // jaeger

                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Tractor APP"))

                )
            .WithMetrics(
                builder => builder

                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation()
                .AddProcessInstrumentation()

                .AddPrometheusExporter()

                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Tractor APP"))
                );





            // AutoMapper
            services.AddAutoMapper(typeof(AutoMappingProfile));

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:8080")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOpenTelemetryPrometheusScrapingEndpoint();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("Category");

            logger.LogInformation("Application started.");

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/plain";

                    var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    //logger.LogError(exception, $"Unhandled exception. Path: {exceptionHandlerPathFeature.Path}");

                    await context.Response.WriteAsync("An error occurred while processing your request.");
                });
            });

            app.Use(async (context, next) =>
            {
                var requestPath = context.Request.Path.Value;
                var requestMethod = context.Request.Method;

                logger.LogInformation($"Request received. Method: {requestMethod}, Path: {requestPath}");

                await next.Invoke();

                var responseStatusCode = context.Response.StatusCode;

                //logger.LogInformation($"Request completed. Method: {requestMethod}, Path: {requestPath}, Status Code: {responseStatusCode}");
            });

            //loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logs"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Logout}/{id?}");

            });

            // Включите Swagger и SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tractor plant API");
                c.RoutePrefix = "swagger"; // Настраивайте URL для доступа к SwaggerUI
            });
        }
    }
}
