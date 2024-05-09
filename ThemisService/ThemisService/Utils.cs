using FileHelpers;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ThyniusService
{
    public static class Utils
    {
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

        public static bool StartThyniusClient(string InstallationDir)
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo(InstallationDir + "ThyniusClient.exe")
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    WorkingDirectory = Path.GetDirectoryName(InstallationDir)
                }
            };

            return process.Start();
        }
    }
}
