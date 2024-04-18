using System.Diagnostics;
using System.IO;

namespace ThemisService
{
    public class WindowsBackgroundService : BackgroundService
    {
        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly string themisInstallationDir;
        private readonly string themisDataDir;

        private void CreateDirs()
        {
            string RoamingDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            Directory.CreateDirectory(RoamingDir + "\\Themis");
            Directory.CreateDirectory(RoamingDir + "\\Themis\\Sessions");
            Directory.CreateDirectory(RoamingDir + "\\Themis\\CookieData");
        }
        public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger)
        {
            _logger = logger;
            themisInstallationDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Themis\\";
            themisDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Themis\\";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + themisInstallationDir + "settings" + "\"");
            regeditProcess.WaitForExit();

            if (!Directory.Exists(themisDataDir))
            {
                CreateDirs();
            }


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ServerComms serverComms = new ServerComms("https://localhost:7135", themisDataDir + "CookieData\\CookieData.txt", _logger);
            SystemEventsManager systemEventsManager = new SystemEventsManager(themisInstallationDir, themisDataDir, serverComms, _logger);

        }
    }
}
