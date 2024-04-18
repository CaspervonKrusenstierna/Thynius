using System.Diagnostics;
using System.IO;

namespace ThyniusService
{
    public class WindowsBackgroundService : BackgroundService
    {
        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly string thyniusInstallationDir;
        private readonly string thyniusDataDir;

        private void CreateDirs()
        {
            string RoamingDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            Directory.CreateDirectory(RoamingDir + "\\Thynius");
            Directory.CreateDirectory(RoamingDir + "\\Thynius\\Sessions");
            Directory.CreateDirectory(RoamingDir + "\\Thynius\\CookieData");
        }
        public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger)
        {
            _logger = logger;
            thyniusInstallationDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Thynius\\";
            thyniusDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Thynius\\";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + thyniusInstallationDir + "settings" + "\"");
            regeditProcess.WaitForExit();

            if (!Directory.Exists(thyniusDataDir))
            {
                CreateDirs();
            }


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ServerComms serverComms = new ServerComms("https://localhost:7135", thyniusDataDir + "CookieData\\CookieData.txt", _logger);
            SystemEventsManager systemEventsManager = new SystemEventsManager(thyniusInstallationDir, thyniusDataDir, serverComms, _logger);

        }
    }
}
