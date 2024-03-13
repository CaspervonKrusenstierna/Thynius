using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ThermisClient
{
    public class LogicAbstraction
    {
        private Communication comms;
        private DllInjector injector;
        public LogicAbstraction()
        {
            this.comms = new Communication();
            this.injector = new DllInjector();

        }
        public bool onLogin(String Username, String Password)
        {
            return comms.Login(Username, Password);
        }
        public bool isLoggedIn()
        {
            return false;
        }

        public bool isProcessRunning(string ProcessName)
        {
            return true;
        }
        public ThermisClient.DllInjectionResult Inject(string sProcName, string sDllPath)
        {
            return injector.Inject(sProcName, sDllPath);
        }
    }
}
