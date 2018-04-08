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
    public partial class Histo : UserControl
    {
        public Histo()
        {
            InitializeComponent();
            DataContext = this;

            loadList();
        }
        private List<String> list = new List<string>();

        public List<string> List { get => list; set => list = value; }

        private void loadList()
        {
            string[] lines = System.IO.File.ReadAllLines("histo");
            
            foreach (string line in lines)
            {
                List.Add(line);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
                Home game = new Home();
                mainWindow.ContentArea.Navigate(game);
            
        }
    }
}
