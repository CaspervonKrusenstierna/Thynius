using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WixToolset.Dtf.WindowsInstaller;
using System.Management;

namespace ThyniusInstallCustomAction
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult CustomActionInstall(Session session)
        {
            string AppdataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            Directory.CreateDirectory(AppdataDir + "\\Thynius");
            Directory.CreateDirectory(AppdataDir + "\\Thynius\\Sessions");

            string thyniusInstallationDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).Replace(" (x86)", "") + "\\Thynius\\";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + thyniusInstallationDir + "settings" + "\"");
            regeditProcess.WaitForExit();



            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true); // make it start automatically
            key.SetValue("ThyniusBackgroundService", thyniusInstallationDir + "ThyniusBackgroundService.exe");

            return ActionResult.Success;
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, System.IO.FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        [CustomAction]
        public static ActionResult CustomActionUninstall(Session session)
        {
            string ThyniusDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Thynius";
            if (Directory.Exists(ThyniusDataDir))
            {
                try
                {
                    DeleteDirectory(ThyniusDataDir);
                }
                catch
                {
                    return ActionResult.Failure;
                }
            }
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true); // make it start automatically
            key.DeleteValue("ThyniusBackgroundService", false);
            return ActionResult.Success;
        }
    }
}

