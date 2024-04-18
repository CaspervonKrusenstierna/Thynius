using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using ThyniusClient.Comms;

namespace ThyniusClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        public async void HandleStartup()
        {
            bool isLoggedIn = await serverComms.isLoggedIn();
            if (isLoggedIn)
            {
                LoggedInWindow loggedInWindow = new LoggedInWindow();
                loggedInWindow.Show();
                return;
            }
            MainWindow mainWindow = new MainWindow(serverComms);
            mainWindow.Show();

        }
        ServerComms serverComms { get; set; }

        void Main(object sender, StartupEventArgs e)
        {
            serverComms = new ServerComms("https://localhost:7135", "C:\\Program Files\\Thynius\\CookieData\\CookieData.txt");
            HandleStartup();
        }

    }
}