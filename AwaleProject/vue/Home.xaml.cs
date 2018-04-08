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
    public partial class Home : UserControl
    {
        public Home(string pseudo)
        {
            InitializeComponent();
            TextBox p = (TextBox)FindName("pseudo");
            p.Text = pseudo;
        }

        private void NewLocalGame(object sender, RoutedEventArgs e)
        {
            TextBox p = (TextBox)FindName("pseudo");
            string pseudo = p.Text;
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            GameVue game = new GameVue(pseudo);
            mainWindow.ContentArea.Navigate(game);
        }

        private void OnlineGame(object sender, RoutedEventArgs e)
        {
            TextBox p = (TextBox)FindName("pseudo");
            string pseudo = p.Text;
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Pseudo = pseudo;
            Online game = new Online(pseudo);
            mainWindow.ContentArea.Navigate(game);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox p = (TextBox)FindName("pseudo");
            string pseudo = p.Text;
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            Histo game = new Histo(pseudo);
            mainWindow.ContentArea.Navigate(game);
        }
    }
}
