using System;
using System.Collections.Generic;
using System.Linq;
using DentalScheduler.Config.DI;
using DentalScheduler.UseCases.Scheduling.Dto.Output;
using DentalScheduler.Web.Api.Filters;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;

namespace DentalScheduler.Web.Api
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
            services.AddDependencies(Configuration);

            services.AddTransient(typeof(Lazy<>), typeof(Lazy<>));
            
            services.AddControllers();

            services.AddAuthorization();

            services.AddOData();
            
            // services.AddMvc();
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<ODataCommonParametersFilter>(); 

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
                
                c.SwaggerDoc("v1", 
                    new OpenApiInfo 
                    { 
                        Title = "Dental Scheduler",
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(policy => policy.WithOrigins(new string[] { "https://localhost:5001" })
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.EnableDependencyInjection();
                endpoints.MapControllers();
                endpoints.Select().Filter().Expand().OrderBy().Count().MaxTop(1000).SkipToken();
                endpoints.MapODataRoute(routeName: "odata", routePrefix: "odata", model: GetEdmModel());
            });
        }

        private IEdmModel GetEdmModel()
        {
            ODataModelBuilder odataBuilder = new ODataConventionModelBuilder();

            odataBuilder.EntitySet<RoomOutput>("Room")
                .EntityType.HasKey(e => e.ReferenceId);

            odataBuilder.EntitySet<DentalTeamOutput>("DentalTeam")
                .EntityType.HasKey(e => e.ReferenceId);

            var ts = odataBuilder.EntitySet<TreatmentSessionOutput>("TreatmentSession")
                .EntityType.HasKey(e => e.ReferenceId);

            odataBuilder.EntitySet<TreatmentOutput>("Treatment")
                .EntityType.HasKey(e => e.ReferenceId);

            odataBuilder.EntitySet<PatientOutput>("Patient")
                .EntityType.HasKey(e => e.ReferenceId);

            return odataBuilder.GetEdmModel();
        }
    }
}
