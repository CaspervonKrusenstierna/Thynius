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
using ThemisClient.Comms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ThemisClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void LoginButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        public MainWindow()
        {

            ServerComms serverComms = new ServerComms("", "");
            if (!serverComms.isLoggedIn()){
                InitializeComponent();
                Style = (Style)FindResource(typeof(Window));
            }
        }
    }
}