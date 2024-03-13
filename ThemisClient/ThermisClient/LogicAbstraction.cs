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
            injector.Inject("WINWORD", "C:\\Users\\Cappe\\OneDrive\\Skrivbord\\HP Fusket v2\\hpfuskhemsida\\Themis\\ThemisDll\\x64\\Debug\\ThemisDll.dll");

        }
        public bool onLogin(String Username, String Password)
        {
            return comms.Login(Username, Password);
        }
    }
}
