using Serilog;

namespace StatementService.Extensions
{
    public static class LoggingExtensions
    {
        public static void AddSerilogLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
