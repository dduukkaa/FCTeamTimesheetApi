using System;
using System.Text.Json.Serialization;
using FCTeamTimesheet.Interfaces.ApiClients;
using FCTeamTimesheet.Interfaces.Services;
using FCTeamTimesheet.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Refit;
using Newtonsoft.Json;
using FCTeamTimesheet.Configuration;

namespace FCTeamTimesheet
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
            services.AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                    .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.Configure<AppointmentParameters>(opt => Configuration.GetSection("AppointmentParameters").Bind(opt));

            services.AddTransient<IAppointmentService, AppointmentService>();

            services.AddRefitClient<IFCTeamApiClient>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://fcteam-api.fcamara.com.br");
                c.Timeout = TimeSpan.FromSeconds(5);
            });

            services.AddSwaggerGen(opt =>
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "FCTeam Timesheet Register", Version = "v1" }
            ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseSwagger();

            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
