using FileHelpers;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Text;

namespace ThyniusService
{
    public static class Utils
    {

        public static void BringProcessToFront(Process process)
        {
            IntPtr handle = process.MainWindowHandle;
            if (IsIconic(handle))
            {
                ShowWindow(handle, SW_RESTORE);
            }

            SetForegroundWindow(handle);
        }

        const int SW_RESTORE = 9;

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr handle, int nCmdShow);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool IsIconic(IntPtr handle);
        
        public static void WriteInputsCsvString(List<FilteredInput> inputs, string path)
        {
            string toReturn = "";
            foreach (FilteredInput input in inputs)
            {
                toReturn += '"' + input.ActionContent + '"' + ",";
                toReturn += (int)input._ActionType + ",";
                toReturn += input.SelectionStart + ",";
                toReturn += input.SelectionEnd + ",";
                toReturn += input.relativeTimePointMs + "\n";
            }
            File.WriteAllText(path + "_result", toReturn);
        }
        public static List<unTreatedInput> ReadInputs(string data)
        {
            var fileHelperEngine = new FileHelperEngine<unTreatedInput>(Encoding.UTF8);
            return fileHelperEngine.ReadString(data).ToList();
        }
        public static InputsMetaData readMetaData(string data)
        {
            var fileHelperEngine = new FileHelperEngine<InputsMetaData>();
            return fileHelperEngine.ReadString(data).ToList()[0];
        }

        public static List<unTreatedItem> ReadItems(string data)
        {
            var fileHelperEngine = new FileHelperEngine<unTreatedItem>(Encoding.UTF8);
            return fileHelperEngine.ReadString(data).ToList();
        }

        public static Process StartThyniusClient(string InstallationDir)
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo(InstallationDir + "ThyniusClient.exe")
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    WorkingDirectory = Path.GetDirectoryName(InstallationDir)
                }
            };

            process.Start();
            return process;
        }

        private static List<String> getXmlPaths(string dataDir)
        {

            List<String> xmlPaths = new List<String>();
            string[] unsyncedSessions = Directory.GetFiles(dataDir + "Sessions\\");

            foreach(string unsyncedSession in unsyncedSessions)
            {
                if (unsyncedSession.EndsWith("_xml"))
                {
                    xmlPaths.Add(unsyncedSession);
                }
            }
            return xmlPaths;
        }
        private static double getFileWriteTime(string filePath)
        {
            return File.GetLastWriteTime(filePath).ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
        public static string GetXmlPath(string sessionPath, string dataDir)
        {
            string toReturn = "";
            double sessionPathWriteTime = getFileWriteTime(sessionPath);
            double closestTime = double.MaxValue;

            foreach(string XmlPath in getXmlPaths(dataDir))
            {
                double currTime = getFileWriteTime(XmlPath);
                if (Math.Abs(currTime - sessionPathWriteTime) < closestTime)
                {
                    closestTime = currTime;
                    toReturn = XmlPath;
                }
            }
            return toReturn;
        }
    }
 }
