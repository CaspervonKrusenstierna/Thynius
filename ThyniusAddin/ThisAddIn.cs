using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Diagnostics;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace ThyniusAddin
{
    public partial class ThisAddIn
    {
        private string ThyniusDataDir;
        private bool HasUserDisabledThynius()
        {
            int temp = 0;
            try
            {
                int.TryParse(File.ReadAllText(ThyniusDataDir + "\\Enabled"), out temp);
            }
            catch
            {
                File.Create(ThyniusDataDir + "\\Enabled").Close();
                return false;
            }
            return temp > 0;
        }
        private void WorkWithDocument(Microsoft.Office.Interop.Word.Document Doc)
        {
            if (HasUserDisabledThynius())
             {
                return;
             }

            var process = new Process
            {
                StartInfo =
              {
                 FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Microsoft Office\\root\\Office16\\WINWORD.EXE",
                 UseShellExecute = true,
                 Arguments = "/w /f \"" + Doc.FullName + "\""
              }
            };
            Doc.Close();
            process.Start();
        }

        void Application_DocumentBeforeSave(Word.Document Doc, ref bool SaveAsUI, ref bool Cancel)
        {
            if (HasUserDisabledThynius())
            {
                return;
            }
            File.WriteAllText(ThyniusDataDir + "\\Sessions\\" + Doc.Name + "_xml", Doc.Content.XML);
        }
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.ThyniusDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Thynius\\";
            this.Application.DocumentOpen += new Word.ApplicationEvents4_DocumentOpenEventHandler(WorkWithDocument);
            this.Application.DocumentBeforeSave += new Word.ApplicationEvents4_DocumentBeforeSaveEventHandler(Application_DocumentBeforeSave);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
