
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

namespace ThyniusService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "ThyniusService";
            });

            LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

            builder.Services.AddHostedService<WindowsBackgroundService>();

            IHost host = builder.Build();
            host.Run();
        }
    }
}