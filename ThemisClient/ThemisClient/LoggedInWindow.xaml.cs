using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ThyniusClient.Comms;

namespace ThyniusClient
{
    /// <summary>
    /// Interaction logic for LoggedInWindow.xaml
    /// </summary>
    public partial class LoggedInWindow : Window
    {
        public string ActivateDeactivateMessage { get; set; }
        public string ButtonMessage { get; set; }

        public bool isActivated;
        private async void ActivateDeactivate_Clicked(object sender, RoutedEventArgs e)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Thynius\\Enabled";
            if (isActivated)
            {
                File.WriteAllText(filePath, "1");
            }
            else
            {
                File.WriteAllText(filePath, "0");
            }
            Process.GetCurrentProcess().Kill();
        }
        private bool HasUserDisabledThynius()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Thynius\\Enabled";
            try
            {
                int temp = 0;
                int.TryParse(File.ReadAllText(filePath), out temp);
                isActivated = temp == 0;
                return temp > 0;
            }
            catch
            {
                var stream = File.Create(filePath);
                stream.Close();
                File.WriteAllText(filePath, "0");
                isActivated = true;
                return false;
            }
        }
        public LoggedInWindow()
        {
            if (HasUserDisabledThynius())
            {
                ActivateDeactivateMessage = "Vill du aktivera Thynius igen?";
                ButtonMessage = "Aktivera";
            }
            else
            {
                ActivateDeactivateMessage = "Vill du avaktivera Thynius temporärt?";
                ButtonMessage = "Avaktivera";
            }
            this.DataContext = this;
            InitializeComponent();
        }
    }
}
