using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwaleProject.modele
{
    public class Game : INotifyPropertyChanged
    {
        private Board board;
        private Player me;
        private Player other;
        private bool myTurn;

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

        public Board Board { get => board; set => board = value; }
        public Player Me { get => me; set => me = value; }
        public Player Other { get => other; set => other = value; }
        public bool MyTurn { get => myTurn; set => myTurn = value; }

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
