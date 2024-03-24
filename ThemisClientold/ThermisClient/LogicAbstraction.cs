using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ThermisClient
{
    public class LogicAbstraction
    {
        private DllInjector injector;
        public LogicAbstraction()
        {
            this.injector = new DllInjector();
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
