using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsCore3CosmosDBMVC.Models;
using ContactsCore3CosmosDBMVC.Models.Abstract;
using ContactsCore3CosmosDBMVC.Models.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace ContactsCore3CosmosDBMVC
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSwaggerGen(cfg =>
      {
        cfg.SwaggerDoc(name: "V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Contacts API", Version = "V1" });
      });
      services.Configure<CosmosUtility>(cfg =>
      {
        cfg.CosmosEndpoint = Configuration["CosmosConnectionString:CosmosEndpoint"];
        cfg.CosmosKey = Configuration["CosmosConnectionString:CosmosKey"];
      });

      services.AddApplicationInsightsTelemetry(cfg =>
      {
        cfg.InstrumentationKey = Configuration["ApplicationInsights:InstrumentationKey"];
      });
      services.AddLogging(cfg =>
      {
        cfg.AddApplicationInsights(Configuration["ApplicationInsights:InstrumentationKey"]);
        // Optional: Apply filters to configure LogLevel Information or above is sent to
        // ApplicationInsights for all categories.
        cfg.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);

        // Additional filtering For category starting in "Microsoft",
        // only Warning or above will be sent to Application Insights.
        //cfg.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
      });
      
      if (Configuration["EnableRedisCaching"] == "true")
      {
        services.AddDistributedRedisCache(cfg => {
          cfg.Configuration = Configuration["ConnectionStrings:RedisConnection"];
          cfg.InstanceName = "master";
        });
      }

      services.AddScoped<IContactRepository, CosmosContactRepository>();
      services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      var appInsightsFlag = app.ApplicationServices.GetService<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>();
      if (Configuration["EnableAppInsightsDisableTelemetry"] == "false")
      {
        appInsightsFlag.DisableTelemetry = false;
      }
      else
      {
        appInsightsFlag.DisableTelemetry = true;
      }

      app.UseStaticFiles();

      app.UseSwagger();
      app.UseSwaggerUI(cfg =>
      {
        cfg.SwaggerEndpoint(url: "/swagger/V1/swagger.json", name: "Contact API");
      });

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(name: "default", pattern: "{controller=Contact}/{action=Index}/{id?}");
        // endpoints.MapGet("/", async context =>
        //       {
        //         await context.Response.WriteAsync("Hello World!");
        //       });
      });
    }
  }
}
