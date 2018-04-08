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
    public partial class GameEnd : UserControl
    {
        public GameEnd(string player)
        {
            InitializeComponent();
            Label p = (Label)this.FindName("Player");
            p.Content = player;
        }

        private void NewLocalGame(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            GameVue game = new GameVue();
            mainWindow.ContentArea.Navigate(game);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            Home game = new Home();
            mainWindow.ContentArea.Navigate(game);
        }
    }
}
