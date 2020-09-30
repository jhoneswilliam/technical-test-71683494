using Api.ActionFilters;
using Api.CrossCutting.DependencyInjection;
using AutoMapper;
using CrossCutting.AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO.Compression;
using System.Net;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()))                    
                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ConfigureRepository.ConfigureDependenciesRepository(services, Configuration);
            ConfigureService.ConfigureDependenciesService(services);

            services.AddLogging(config => config.AddConsole());

            services.AddSingleton(new MapperConfiguration(mc =>
            {                
                mc.AddProfile(new AutoMapperRequestProfile());
                mc.AddProfile(new AutoMapperResponsesProfile());
            }).CreateMapper());

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddMvcCore()
                  .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                  .AddMvcOptions(x =>
                  {
                      x.EnableEndpointRouting = false;
                  })
                  .AddApiExplorer();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");
            app.UseRouting();
            app.UseResponseCompression();


            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        });
                });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseCors(builder => builder
                  .SetIsOriginAllowed((host) => true)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
