


using System.Diagnostics;
using System.IO;

namespace ThyniusService
{
    class SystemEventsManager
    {
        private Process thisProcess { get; set; }
        private string installationDir { get; set; }

        private string dataDir { get; set; }
        private Thread wordListenerThread { get; set; }
        private ushort wordCheckDelayMs { get; set; }

        private ServerComms serverComms;

        private DllInjector injector;

        private readonly ILogger<WindowsBackgroundService> logger;

        private string dllPath;
        List<int> openWordProcesses = new List<int>();

        public SystemEventsManager(string InstallationDir, string DataDir, ServerComms serverComms, ILogger<WindowsBackgroundService> logger, ushort wordCheckDelayMs = 1000)
        {
            thisProcess = Process.GetCurrentProcess();
            this.installationDir = InstallationDir;
            this.dataDir = DataDir;
            this.wordCheckDelayMs = wordCheckDelayMs;
            this.serverComms = serverComms;
            this.logger = logger;
            injector = new DllInjector();
            dllPath = installationDir + "\\ThyniusDll.dll";
            wordListenerThread = new Thread(WordListenerThreadMain);
            wordListenerThread.Start();
            SyncSessions();
        }

        //runs on word exit to sync with servers
        //also runs on computer start to check if there is any unsynced sessions from abrupt exit
        private async void SyncSessions()
        {
            string[] unsyncedSessions = Directory.GetFiles(dataDir + "Sessions/");
            foreach (string sessionPath in unsyncedSessions)
            {
                if (!sessionPath.EndsWith("_metadata") && !sessionPath.EndsWith("_result"))
                {
                    if (await serverComms.SubmitSession(Path.GetFileName(sessionPath), sessionPath))
                    {
                        //File.Delete(sessionPath);
                        //File.Delete(sessionPath + "_metadata");
                        //File.Delete(sessionPath + "_result");
                    }
                }
            }
        }

        private bool HasUserDisabledThynius()
        {
            int temp = 0;
            int.TryParse(File.ReadAllText(installationDir + "\\Enabled"),out temp);
            return temp > 0;
        }
        private void OnWordExit(int processId)
        {
            if (HasUserDisabledThynius())
            {
                return;
            }
            SyncSessions();
        }
        private void OnWordStartUp(int processId)
        {
            if (HasUserDisabledThynius()) {
                return;
            }
            Thread.Sleep(300);
            injector.Inject((uint)processId, dllPath);
        }
        private void WordListenerThreadMain()
        {
            while (true)
            {
                IEnumerable<int> procIds = Process.GetProcessesByName("WINWORD").Select(i => i.Id);
                List<int> temp = new List<int>();
                foreach (int procId in procIds)
                {
                    if (!openWordProcesses.Contains(procId))
                    {
                        temp.Add(procId);
                        OnWordStartUp(procId);
                    }
                }
                foreach (int processId in temp)
                {
                    openWordProcesses.Add(processId);
                }
                temp = new List<int>();
                foreach (int procId in openWordProcesses)
                {
                    if (!procIds.Contains(procId))
                    {
                        OnWordExit(procId);
                        temp.Add(procId);
                    }
                }
                foreach (int procId in temp)
                {
                    openWordProcesses.Remove(procId);
                }
                Thread.Sleep(wordCheckDelayMs);
            }
        }
    }
}
