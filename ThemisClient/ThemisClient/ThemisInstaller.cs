using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemisClient
{
    public static class ThemisInstaller
    {
        public static bool isInstalled()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Directory.Exists(appdata + "\\Themis");
        }
        public static bool Install()
        {
            return true;
        }
        public static bool startService()
        {

            System.Diagnostics.Process.Start(pathToServiceExecutable, "-install");
            return true;
        }
        
    }
}
