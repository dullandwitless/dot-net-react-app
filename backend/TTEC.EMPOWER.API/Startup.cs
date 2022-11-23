using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using TTEC.EMPOWER.API.Extensions;
using TTEC.EMPOWER.Business.Mappings;

namespace TTEC.EMPOWER.API
{
    public class Startup
    {
        /// <summary>
        /// Startup class
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure Service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services) {

            //Register Sevices/Mediator
            services.AddInfrastructure();

            // Add services to the container.
            services.AddControllers();

            // Fluent validation
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddHealthChecks();
            //services.ConfigureSerilog(Configuration);
            services.AddApplicationInsightsTelemetry();
            services.AddLogging(options =>
            {
                // hook the Application Insights Provider
                options.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);
            });
            //Logger Registration
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Startup>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                      builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddResponseCompression();
            //AAD
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"))
            .EnableTokenAcquisitionToCallDownstreamApi()
            .AddInMemoryTokenCaches();

            //API Versioning 
            services.AddApiVersioning(x =>
            {
                //Specify the default API Version as 1.0
                x.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(
                     options =>
                     {
                         // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                         // note: the specified format code will format the version as "'v'major[.minor][-status]"
                         options.GroupNameFormat = "'v'VVV";
                         // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                         // can also be used to control the format of the API version in route templates
                         options.SubstituteApiVersionInUrl = true;
                     });

            //Swagger Configuration 
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configure middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILogger<Startup> logger) {

            logger.LogInformation(
               "Configuring for {Environment} environment",
               env.EnvironmentName);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCompression();
            app.UseHttpLogging();

            app.UseCors("CorsPolicy");

            // Response Caching Middleware
            //app.UseResponseCaching();


            // app.ConfigureExceptionHandler();

            //Enable middleware to serve generated Swagger as a JSON endpoint
            // app.AddCustomSwagger(provider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/api/health");
            });
        }
    }
}
