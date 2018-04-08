using AwaleProject.modele;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AwaleProject.vue
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Online : UserControl
    {
        public Online()
        {
            InitializeComponent();
        }

        private void Join(object sender, RoutedEventArgs e)
        {
            /* MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
             GameVue game = new GameVue();
             mainWindow.ContentArea.Navigate(game);*/
            TextBox ipT = (TextBox)FindName("ipS");
            string ip = ipT.Text;
            TextBox portT = (TextBox)FindName("portS");
            int port = Int32.Parse(portT.Text);
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            GameVue game = new GameVue(ip, port, false);
            mainWindow.ContentArea.Navigate(game);
        }

        private void Host(object sender, RoutedEventArgs e)
        {
            TextBox ipT = (TextBox)FindName("monIp");
            string ip = ipT.Text;
            TextBox portT = (TextBox)FindName("port");
            int port = Int32.Parse(portT.Text);
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            GameVue game = new GameVue(ip, port, true);
            mainWindow.ContentArea.Navigate(game);
        }
    }
}
