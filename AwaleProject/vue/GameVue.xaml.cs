using AwaleProject.modele;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour GameVue.xaml
    /// </summary>
    public partial class GameVue : UserControl
    {
        private int width;
        private Game game;
        public GameVue()
        {
            InitializeComponent();
            game = new Game(new Player(""), new Player(""));
            DataContext = game;
        }

        public int Width1 { get => width; set => width = value; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void MouseDown_Elli(object sender, MouseButtonEventArgs e)
        {
            int val = Int32.Parse(((Ellipse)sender).Name.Replace("h", string.Empty));
            if (game.CanChose(val))
            {
                game.ChoseHole(val);
            }
        }
    }
}
