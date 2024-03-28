

using System;
using System.Diagnostics;
using System.IO;
using ThemisClient.Comms;

namespace ThemisClient
{
    class SystemEventsManager
    {
        private Process thisProcess {  get; set; }
        private string dataDir { get; set; }
        private Thread wordListenerThread { get; set; }
        private UInt16 wordCheckDelayMs { get; set; }

        private ServerComms serverComms;

        private DllComms dllComms;

        private DllInjector injector;

        private string dllPath;

        private bool exitWordListenerThread = false;
        public SystemEventsManager(string dataDir, ServerComms serverComms, UInt16 wordCheckDelayMs=100)
        {
            this.thisProcess = Process.GetCurrentProcess();
            this.dataDir = dataDir;
            this.wordListenerThread = new Thread(WordListenerThreadMain);
            this.wordCheckDelayMs = wordCheckDelayMs;
            this.serverComms = serverComms;

            SyncSessions();
        }

        //runs on word exit to sync with servers
        //also runs on computer start to check if there is any unsynced sessions from abrupt exit
        private void SyncSessions()
        {
            string[] unsyncedSessions = Directory.GetFiles(this.dataDir);
            foreach(string sessionPath in unsyncedSessions)
            {
                try
                {
                    serverComms.CreateTextSession(sessionPath, /*FIX THISSS*/1);
                    File.Delete(sessionPath);

                }
                catch {
                    // dont want to delete unsynced sessionPath
                }
            }
        }
        private void OnWordExitHook(object sender, EventArgs e)
        {
            SyncSessions();
            this.exitWordListenerThread = false;
            this.wordListenerThread = new Thread(WordListenerThreadMain);
            this.dllComms.Dispose();
        }
        private void OnWordStartUp(Process wordProcess) {
            wordProcess.EnableRaisingEvents = true;
            wordProcess.Exited += new EventHandler(OnWordExitHook);
            this.injector = new DllInjector();
            this.dllComms = new DllComms();

            injector.Inject("WINWORD", this.dllPath);
        }
        private void WordListenerThreadMain()
        {
            while(!exitWordListenerThread)
            {
                var wordProcess = Process.GetProcessesByName("WINWORD").Single();
                if(wordProcess != null)
                {
                    OnWordStartUp(wordProcess);
                    exitWordListenerThread = true;
                }
                Thread.Sleep(this.wordCheckDelayMs);
            }
        }

    }
}
