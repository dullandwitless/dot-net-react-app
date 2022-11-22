using Microsoft.Extensions.Logging.ApplicationInsights;

namespace TTEC.EMPOWER.API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("From Program, running the host now.");
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            //Logging
            .ConfigureLogging((builderContext, loggingBuilder) =>
            {

                //Add default Logging configuration
                loggingBuilder.AddConfiguration(builderContext.Configuration);

                //Add ApplicationInsights
                loggingBuilder.AddApplicationInsights(builderContext.Configuration.GetSection("ApplicationInsights:InstrumentationKey").Value);

                // Capture all log-level entries from Program
                loggingBuilder.AddFilter<ApplicationInsightsLoggerProvider>(
                    typeof(Program).FullName, LogLevel.Trace);

                // Capture all log-level entries from Startup
                loggingBuilder.AddFilter<ApplicationInsightsLoggerProvider>(
                    typeof(Startup).FullName, LogLevel.Trace);
                //loggingBuilder.AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Trace);
                //loggingBuilder.AddFilter<ConsoleLoggerProvider>("", LogLevel.Trace);


            });
    }
}
