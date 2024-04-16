using System.Diagnostics;
using ThermisClient.Comms;

namespace ThermisClient
{
    internal static class Program
    {
        static void Main()
        {
            LogicAbstraction logicAbstraction = new LogicAbstraction();
            DllComms dllComms = new DllComms();
            Debug.WriteLine(logicAbstraction.Inject("WINWORD", "C:\\Dll1.dll"));

            return;

        }
    }
}