


using System.Diagnostics;
using System.IO;
using ThemisClient.Comms;

namespace ThemisClient
{
    class SystemEventsManager
    {
        private Process thisProcess {  get; set; }
        private string themisDir { get; set; }
        private Thread wordListenerThread { get; set; }
        private UInt16 wordCheckDelayMs { get; set; }

        private ServerComms serverComms;

        private DllInjector injector;

        private string dllPath;
        List<int> openWordProcesses = new List<int>();

        public SystemEventsManager(string themisDir, ServerComms serverComms, UInt16 wordCheckDelayMs=1000)
        {
            thisProcess = Process.GetCurrentProcess();
            this.themisDir = themisDir;
            this.wordCheckDelayMs = wordCheckDelayMs;
            this.serverComms = serverComms;
            this.injector = new DllInjector();
            this.dllPath = themisDir + "\\Dll1.dll";
            Debug.WriteLine(dllPath);
            wordListenerThread = new Thread(WordListenerThreadMain);
            wordListenerThread.Start();
            SyncSessions();
        }

        private string GetSessionMetaDataPath(string SessionFileName)
        {
            string[] SessionMetaDatas = Directory.GetFiles(themisDir + "/SessionsMetaData/");
            foreach (string metaDataPath in SessionMetaDatas)
            {
               if(Path.GetFileName(metaDataPath) == SessionFileName)
                {
                    return metaDataPath;
                }
            }
            return null;
        }

        //runs on word exit to sync with servers
        //also runs on computer start to check if there is any unsynced sessions from abrupt exit
        private async void SyncSessions()
        {
            string[] unsyncedSessions = Directory.GetFiles(themisDir + "/Sessions/");
            foreach(string sessionPath in unsyncedSessions)
            {
                if (!sessionPath.EndsWith("_metadata") && !sessionPath.EndsWith("_result"))
                {
                    if (await serverComms.SubmitSession(Path.GetFileName(sessionPath), sessionPath))
                    {
                        //File.Delete(sessionPath);
                    }
                }
            }
        }
        private void OnWordExit(int processId)
        {
            Debug.WriteLine("Word process exited. Process Id: " + processId);
            SyncSessions();
        }
        private void OnWordStartUp(int processId) {
            Thread.Sleep(300);
            Debug.WriteLine("Word process started. Process Id: " + processId);
            injector.Inject((uint)processId, this.dllPath);
        }
        private void WordListenerThreadMain()
        {
            while (true)
            {
                IEnumerable<int> procIds = Process.GetProcessesByName("WINWORD").Select(i => i.Id);
                List<int> temp = new List<int>();
                foreach(int procId in procIds)
                {
                    if (!openWordProcesses.Contains(procId))
                    {
                        temp.Add(procId);
                        OnWordStartUp(procId);
                    }
                }
                foreach(int  processId in temp)
                {
                    openWordProcesses.Add(processId);
                }
                temp = new List<int>();
                foreach(int procId in openWordProcesses)
                {
                    if (!procIds.Contains(procId))
                    {
                        OnWordExit(procId);
                        temp.Add(procId);
                    }
                }
                foreach(int procId in temp)
                {
                    openWordProcesses.Remove(procId);
                }
                Thread.Sleep(this.wordCheckDelayMs);
            }
        }
    }
}
