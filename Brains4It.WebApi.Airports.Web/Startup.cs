using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brains4It.WebApi.Airports.Elastic.Helpers;
using Brains4It.WebApi.Airports.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;

namespace Brains4It.WebApi.Airports.Web
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
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Airport API", Version = "v1" });
            });

            services.AddTransient(typeof(IElasticClient), u => 
                EsConnectionFactory.CreateEsClient(
                    "http://54.36.183.104:9200/",
                    "airport"));
            services.AddTransient<IDataAccess, DataAccess>();
            services.AddTransient<IDataAccess, DataAccess>();
            services.AddTransient<IAirportManager, AirportManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Airport API V1");
            });
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Web Api didn't find anything!");
            });
        }
    }
}
