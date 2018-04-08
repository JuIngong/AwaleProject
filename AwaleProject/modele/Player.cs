using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwaleProject.modele
{
    public class Player : INotifyPropertyChanged
    {
        string pseudo;
        int score;

        public Player(string pseudo)
        {
            Pseudo = pseudo;
            Score = 0;
        }

         #region INotifyPropertyChanged
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion INotifyPropertyChanged 

       
        public string Pseudo { get => pseudo; set => pseudo = value; }
        public int Score { get => score; set { score = value;
                OnPropertyChanged("Score");
            } }
    }

}
