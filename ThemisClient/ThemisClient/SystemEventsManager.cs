

using System;
using System.Diagnostics;
using System.IO;
using ThemisClient.Comms;

namespace ThemisClient
{
    class SystemEventsManager
    {
        private Process thisProcess {  get; set; }
        private string themisDir { get; set; }
        private Thread wordStartListenerThread { get; set; }
        private bool exitWordListenerThread;
        private Thread wordExitListenerThread { get; set; }
        bool exitWordExitListenerThread;
        private UInt16 wordCheckDelayMs { get; set; }

        private ServerComms serverComms;

        private DllComms dllComms;

        private DllInjector injector;

        private string dllPath;

        public SystemEventsManager(string themisDir, ServerComms serverComms, UInt16 wordCheckDelayMs=1000)
        {
            thisProcess = Process.GetCurrentProcess();
            this.themisDir = themisDir;
            this.wordCheckDelayMs = wordCheckDelayMs;
            this.serverComms = serverComms;

            StartWordStartListener();
            SyncSessions();
        }

        private void StartWordStartListener()
        {
            exitWordListenerThread = false;
            wordStartListenerThread = new Thread(WordStartListenerThreadMain);
            wordStartListenerThread.Start();
        }
        private void StopWordStartListener()
        {
            exitWordListenerThread = true;
        }
        private void StartWordExitListener()
        {
            exitWordExitListenerThread = false;
            wordExitListenerThread = new Thread(WordExitThreadListenerMain);
            wordExitListenerThread.Start();
        }
        private void StopWordExitListener()
        {
            exitWordExitListenerThread = true;
        }
        //runs on word exit to sync with servers
        //also runs on computer start to check if there is any unsynced sessions from abrupt exit
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
    
        private async void SyncSessions()
        {
            string[] unsyncedSessions = Directory.GetFiles(themisDir + "/Sessions/");
            foreach(string sessionPath in unsyncedSessions)
            {
                Debug.WriteLine(Path.GetFileName(sessionPath));

                if (await serverComms.SubmitSession(Path.GetFileName(sessionPath), sessionPath))
                {
                    File.Delete(sessionPath);
            }
    
            }
        }
        private void OnWordExit()
        {
            StopWordExitListener();
            StartWordStartListener();

            SyncSessions();
            this.exitWordListenerThread = false;
            this.wordStartListenerThread = new Thread(WordStartListenerThreadMain);
            this.dllComms.Dispose();
        }
        private void OnWordStartUp(Process wordProcess) {
            StopWordStartListener();
            StartWordExitListener();

            this.injector = new DllInjector();
            this.dllComms = new DllComms();
            injector.Inject("WINWORD", this.dllPath);
        }
        private void WordStartListenerThreadMain()
        {
            while(!exitWordListenerThread)
            {
                var wordProcess = Process.GetProcessesByName("WINWORD").SingleOrDefault();
                if(wordProcess != null)
                {
                    OnWordStartUp(wordProcess);
                }
                Thread.Sleep(this.wordCheckDelayMs);
            }
        }
        private void WordExitThreadListenerMain()
        {
            while (!exitWordExitListenerThread)
            {
                var wordProcess = Process.GetProcessesByName("WINWORD").SingleOrDefault();
                if (wordProcess == null)
                {
                    OnWordExit();
                }
                Thread.Sleep(this.wordCheckDelayMs);
            }
        }
    }
}
