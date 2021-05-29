using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zelda.Data;
using System.IO;
using AutoMapper;
using Zelda.Api.Services;
using Microsoft.Net.Http.Headers;

namespace Zelda
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zelda", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, "Zelda.Api.xml");
                c.IncludeXmlComments(filePath);
            });
            services.AddDbContext<ZeldaContext>(options =>
                    options.UseNpgsql(Configuration["ConnectionStrings:ZeldaContext"]));
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<ILinksRepository, LinksRepository>();
            services.AddTransient<ITagsRepository, TagsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(policy =>
                    policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
                    .AllowAnyMethod()
                    .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
                    .AllowCredentials());
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zelda v1"));
                app.UseReDoc(c =>
                {
                    c.RoutePrefix = "docs";
                    c.SpecUrl = "/swagger/v1/swagger.json";
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
