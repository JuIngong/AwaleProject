using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modele;
using Newtonsoft.Json;

namespace AwaleProject.modele
{
    public class Game : INotifyPropertyChanged
    {
        private Board board;
        private Player me;
        private Player other;
        private bool myTurn;
        private bool online = false;
        private bool host;
        private Client cli;
        private Serveur ser;
        private string ip;
        private int port;

        #region INotifyPropertyChanged
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion INotifyPropertyChanged 

        public Game(Player player1, Player player2)
        {
            this.Board = new Board();
            myTurn = true;
            this.me = player1;
            this.other = player2;
        }

        public Game(Player player1, Player player2, string ip, int port, bool host)
        {
            this.Board = new Board();
            myTurn = false;
            Online = true;
            this.Host = host;
            this.me = player1;
            this.other = player2;
            this.ip = ip;
            this.port = port;
        }

        public void StartOnline()
        {
            if (host)
            {
               
                Ser = new Serveur(ip, port);
                Ser.Start();
                Other.Pseudo = Ser.Listen();
                this.OnPropertyChanged("Other");
                Ser.Send(Me.Pseudo);
                myTurn = true;
            }
            else
            {
                Cli = new Client(ip, port);
                Cli.Start();
                Cli.Send(Me.Pseudo);
                Other.Pseudo = Cli.Receive();
                this.OnPropertyChanged("Other");
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork2;
                worker.RunWorkerAsync();
                
            }
        }

        void worker_DoWork2(object sender, DoWorkEventArgs e)
        {
            this.ReceiveMsg();
        }

        public Board Board { get => board; set => board = value; }
        public Player Me { get => me; set => me = value; }
        public Player Other { get => other; set => other = value; }
        public bool MyTurn { get => myTurn; set => myTurn = value; }
        public bool Online { get => online; set => online = value; }
        public bool Host { get => host; set => host = value; }
        internal Client Cli { get => cli; set => cli = value; }
        internal Serveur Ser { get => ser; set => ser = value; }

        public void ChoseHoleOnline(int hole)
        {
            board.ChosePlay(hole);

            int oldScore = 0;
            bool exit = false;
            while (myTurn && !exit && Board.Circular.Idx >= 0 && Board.Circular.Idx <= 5)
            {
                oldScore = Me.Score;
                if ((Me.Score += Board.GetBean(Board.Circular.Idx)) > oldScore)
                {
                    this.OnPropertyChanged("Me");
                    Board.Circular.Next();
                }
                else
                {
                    exit = true;
                }

            }
            int[] list = new int[12];
            for(int i = 0; i<6; i++)
            {
                list[6 + i] = board.Circular[i];
            }
            for (int i = 6; i < 12; i++)
            {
                list[i-6] = board.Circular[i];
            }
            string json = JsonConvert.SerializeObject(list);
            if (Host)
            {
                Ser.Send(json);
                Ser.Send("" + Me.Score);
            }
            else
            {
                Cli.Send(json);
                Cli.Send("" + Me.Score);
            }

            myTurn = !myTurn;
            this.OnPropertyChanged("MyTurn");
            this.OnPropertyChanged("Board");
        }

        public string IsGameEnd()
        {
            int o = 0;
            int m = 0;
            for (int i = 0; i < 6; i++)
            {
                o += board.Circular[i];
            }
            for (int i = 6; i < 12; i++)
            {
                m += board.Circular[i];
            }
            if (Me.Score >= 25 || o == 0)
            {
                this.EndGame("Victoire");
                return Me.Pseudo;
            }
            else if (Other.Score >= 25 || m == 0)
            {
                this.EndGame("Defaite");
                return Other.Pseudo;

            }
            return "";
        }

        public void EndGame(string result)
        {
            if (online) {
                if ( Host)
                {
                    ser.Stop();
                }
                else
                {
                    cli.End();
                }
            }
            string path = "histo";
            if (!File.Exists(path))
            {
                using (var tw = new StreamWriter(path))
                {
                    tw.WriteLine(result + " : " + Me.Pseudo + " : "+ Me.Score + " VS " + Other.Pseudo + ":" +Other.Score);
                }
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(result + " : " + Me.Pseudo + " : " + Me.Score + " VS " + Other.Pseudo + ":" + Other.Score);

                }
            }
        }
            

        public void ReceiveMsg()
        {
            string s;
            int sco;
            if (Host)
            {
                s = Ser.Listen();
                sco = Int32.Parse(Ser.Listen());
            }
            else
            {
                s = Cli.Receive();
                sco = Int32.Parse(Cli.Receive());
            }
            Other.Score = sco;
            int[] list = JsonConvert.DeserializeObject<int[]>(s);
            for (int i = 0; i < 12; i++)
            {
                board.Circular[i] = list[i];
            }
            myTurn = true;
            this.OnPropertyChanged("Board");
            this.OnPropertyChanged("Other");
        }


        public void ChoseHole(int hole)
        {
            board.ChosePlay(hole);
            int oldScore = 0;
            bool exit = false;
            while (myTurn && !exit && Board.Circular.Idx >= 0 && Board.Circular.Idx <= 5)
            {
                oldScore = Me.Score;
                if((Me.Score += Board.GetBean(Board.Circular.Idx)) > oldScore)
                {
                    this.OnPropertyChanged("Me");
                    Board.Circular.Next();
                }
                else
                {
                    exit = true;
                }
                
            }
            while (!myTurn && !exit && Board.Circular.Idx >= 6 && Board.Circular.Idx <= 11)
            {
                oldScore = Other.Score;
                if ((Other.Score += Board.GetBean(Board.Circular.Idx)) > oldScore)
                {
                    this.OnPropertyChanged("Other");
                    Board.Circular.Next();
                }
                else
                {
                    exit = true;
                }
                
            }
            myTurn = !myTurn;
            this.OnPropertyChanged("MyTurn");
            this.OnPropertyChanged("Board");

            Console.WriteLine(me);
            Console.WriteLine(other);

        }

        public bool CanChose(int hole)
        {
            if(!myTurn && hole >= 0 && hole <= 5)
            {
                return Board.CanChose(hole);
            }
            else if(myTurn && hole >= 6 && hole <= 11)
                return Board.CanChose(hole);
            return false;
        }

    }
}
