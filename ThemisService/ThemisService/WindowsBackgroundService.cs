namespace ThemisService
{
    public class WindowsBackgroundService : BackgroundService
    {
        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly string themisDir;
        public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger)
        {
            _logger = logger;
            themisDir = Program.Dir;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ServerComms serverComms = new ServerComms("https://localhost:7135",themisDir+"CookieData\\CookieData.txt");
            SystemEventsManager systemEventsManager = new SystemEventsManager(themisDir, serverComms, _logger);

        }
    }
}
