using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwaleProject.modele
{
    public class Player
    {
        string pseudo;
        int score;

        public Player(string pseudo)
        {
            Pseudo = pseudo;
            Score = 0;
        }

        public string Pseudo { get => pseudo; set => pseudo = value; }
        public int Score { get => score; set => score = value; }
    }
}
