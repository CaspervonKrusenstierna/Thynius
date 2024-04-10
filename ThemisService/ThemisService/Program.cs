
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

namespace ThemisService
{
    public class Program
    {
        internal static string Dir;
        public static void Main(string[] args)
        {
            Dir = "C:\\Program Files\\Themis\\";
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "ThemisService";
            });

            LoggerProviderOptions.RegisterProviderOptions<
                EventLogSettings, EventLogLoggerProvider>(builder.Services);

            builder.Services.AddHostedService<WindowsBackgroundService>();

            IHost host = builder.Build();
            host.Run();
        }
    }
}