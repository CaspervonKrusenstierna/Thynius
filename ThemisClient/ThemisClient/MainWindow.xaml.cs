using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThyniusClient.Comms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ThyniusClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private async void LoginButton_Clicked(object sender, RoutedEventArgs e)
        {
            bool Result = await serverComms.LoginAsync(UsernameTextBox.Text, PasswordTextBox.Password);
            if (!Result)
            {
                ErrorMessage.Text = "Felaktig information";
                ErrorMessage.Foreground = Brushes.Red;
                return;
            }
            LoggedInWindow loggedInWindow = new LoggedInWindow(serverComms);
            loggedInWindow.Show();
            this.Close();
        }

        private ServerComms serverComms;
        public MainWindow(ServerComms serverComms)
        {
            this.serverComms = serverComms;
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));

        }
    }
}