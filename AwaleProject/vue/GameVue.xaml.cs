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
using Newtonsoft.Json;
using System.Windows.Threading;
using System.ComponentModel;

namespace AwaleProject.vue
{
    /// <summary>
    /// Logique d'interaction pour GameVue.xaml
    /// </summary>
    public partial class GameVue : UserControl
    {
        private Game game;
        private string ip;
        private int port;
        private bool online = false;
        private bool host;
        Action emptyDelegate = delegate { };

        public GameVue()
        {
            InitializeComponent();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            game = new Game(new Player(mainWindow.Pseudo), new Player("Player2"));
            DataContext = game;
        }

        public GameVue(string ip, int port, bool host)
        {
            InitializeComponent();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            online = true;
            game = new Game(new Player(mainWindow.Pseudo), new Player("Player2"), ip, port, host);
            this.ip = ip;
            this.port = port;
            this.host = host;
            DataContext = game;
        }


        private void SetTour()
        {
            Label tour = (Label)this.FindName("tour");
            if (game.MyTurn)
            {
                tour.Content = "Tour : " + game.Me.Pseudo;
            }
            else
            {
                tour.Content = "Tour : " + game.Other.Pseudo;

            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (online)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork2;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted2;
                worker.RunWorkerAsync();
                
            }
            else
            {
                SetTour();
            }
            
        }

        void worker_DoWork2(object sender, DoWorkEventArgs e)
        {
            game.StartOnline();
        }
        void worker_RunWorkerCompleted2(object sender, RunWorkerCompletedEventArgs e)
        {
            SetTour();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            game.ReceiveMsg();
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EndTreatment();
        }

        void EndTreatment()
        {
            string s = game.IsGameEnd();
            if (game.Me.Pseudo == s)
            {
                game.EndGame("Victoire");
                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
                GameEnd end;
                if (online) 
                    end = new GameEnd(game.Me.Pseudo, ip, port, host);
                else
                    end = new GameEnd(game.Me.Pseudo);
                mainWindow.ContentArea.Navigate(end);
            }
            else if (game.Other.Pseudo == s)
            {
                game.EndGame("Defaite");

                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
                GameEnd end;
                if (online)
                    end = new GameEnd(game.Other.Pseudo, ip, port, host);
                else
                    end = new GameEnd(game.Other.Pseudo);
                mainWindow.ContentArea.Navigate(end);

            }
            SetTour();
        }

        private void MouseDown_Elli(object sender, MouseButtonEventArgs e)
        {
            int val = Int32.Parse(((Ellipse)sender).Name.Replace("h", string.Empty));
            if (game.CanChose(val))
            {
                if (!online) { 
                game.ChoseHole(val);
                    EndTreatment();
            }
            else
            {
                game.ChoseHoleOnline(val);
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
                
            }
            
            

        }
    }
}
