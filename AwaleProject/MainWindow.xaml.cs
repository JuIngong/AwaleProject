using AwaleProject.vue;
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

namespace AwaleProject
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string pseudo;

        public MainWindow()
        {
            InitializeComponent();
            Home h = new Home();
            this.ContentArea.Navigate(h);
        }

        public string Pseudo { get => pseudo; set => pseudo = value; }
    }
}
