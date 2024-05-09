


using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

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

        List<int> openWordProcesses = new List<int>();
        nint firstWordProcessHwnd = 0;

        public SystemEventsManager(string InstallationDir, string DataDir, ServerComms serverComms, ushort wordCheckDelayMs = 1000)
        {
            thisProcess = Process.GetCurrentProcess();
            this.installationDir = InstallationDir;
            this.dataDir = DataDir;
            this.wordCheckDelayMs = wordCheckDelayMs;
            this.serverComms = serverComms;
            injector = new DllInjector();
            wordListenerThread = new Thread(WordListenerThreadMain);
            wordListenerThread.Start();
            SyncSessions();
        }

        //runs on word exit to sync with servers
        //also runs on computer start to check if there is any unsynced sessions from abrupt exit
        private async void SyncSessions()
        {
            bool isLoggedIn = await serverComms.isLoggedIn();
            if (!isLoggedIn)
            {
                Process[] alreadyRunningThyniusClientProcesses = Process.GetProcessesByName("ThyniusClient");
                if(alreadyRunningThyniusClientProcesses.Length > 0)
                {
                    foreach(Process p in alreadyRunningThyniusClientProcesses)
                    {
                        Utils.BringProcessToFront(p);
                    }
                }
                else
                {
                    Process thyniusClient = Utils.StartThyniusClient(installationDir);
                    Utils.BringProcessToFront(thyniusClient);
                }
                return;
            }

            string[] unsyncedSessions = Directory.GetFiles(dataDir + "Sessions\\");
            foreach (string sessionPath in unsyncedSessions)
            {
                if (!sessionPath.EndsWith("_metadata") && !sessionPath.EndsWith("_addindata") && !sessionPath.EndsWith("_result") && !sessionPath.EndsWith("_xml"))
                {
                    string xmlPath = Utils.GetXmlPath(sessionPath, dataDir);
                    if (await serverComms.SubmitSession(Path.GetFileName(sessionPath), sessionPath, xmlPath))
                    {
                        File.Delete(sessionPath);
                        File.Delete(sessionPath + "_metadata");
                        File.Delete(sessionPath + "_result");
                        File.Delete(xmlPath);
                    }
                }
            }
        }

        private bool HasUserDisabledThynius()
        {
            int temp = 0;
            try
            {
                int.TryParse(File.ReadAllText(dataDir + "\\Enabled"), out temp);
            }
            catch
            {
                File.Create(dataDir + "\\Enabled").Close();
                return false;
            }
            return temp > 0;
        }
        private void OnWordExit(int processId)
        {
            if (HasUserDisabledThynius())
            {
                return;
            }
            firstWordProcessHwnd = 0;
            SyncSessions();
        }
        private void OnWordStartUp(int processId)
        {
            if (HasUserDisabledThynius()) {
                return;
            }
            Process wordProc = Process.GetProcessById(processId);
            DllInjectionResult result = injector.Inject((uint)processId, "C:\\Users\\Cappe\\OneDrive\\Skrivbord\\HP Fusket v2\\hpfuskhemsida\\Themis\\Dll1\\Release\\Thynius32.dll"/*installationDir + "ThyniusDll.dll"*/);
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
