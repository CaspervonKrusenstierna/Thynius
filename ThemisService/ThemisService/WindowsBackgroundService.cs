using System.Diagnostics;

namespace ThyniusService
{
    public class WindowsBackgroundService : BackgroundService
    {
        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly string thyniusInstallationDir;
        private readonly string thyniusDataDir;

        private void CreateDirs()
        {
            string AppdataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(AppdataDir + "\\Thynius");
            Directory.CreateDirectory(AppdataDir + "\\Thynius\\Sessions");
        }
        public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger)
        {
            _logger = logger;
            _logger.LogInformation("yOOO");
            thyniusInstallationDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Thynius\\";
            thyniusDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Thynius\\";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + thyniusInstallationDir + "settings" + "\"");
            regeditProcess.WaitForExit();

            if (!Directory.Exists(thyniusDataDir))
            {
                CreateDirs();
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             CreateDirs();
            ServerComms serverComms = new ServerComms("https://thynius.com", thyniusDataDir + "CookieData.txt", _logger);
            SystemEventsManager systemEventsManager = new SystemEventsManager(thyniusInstallationDir, thyniusDataDir, serverComms, _logger);
        }
    }
}
