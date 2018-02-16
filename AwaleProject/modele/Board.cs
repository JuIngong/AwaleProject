using modele;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwaleProject.modele
{
    public class Board
    {
        CircularList<int> circular = new CircularList<int>(12);

        public Board()
        {
            Circular.SetAll(4);
        }

        public CircularList<int> Circular { get => circular; set => circular = value; }

        public void ChosePlay(int hole)
        {
            Circular.Idx = hole;
            int nbB = Circular.Value;
            Circular.Value = 0;
            for (int i = nbB; i > 0; i--)
            {
                Circular.Previous();
                Circular.Value ++;
            }
        }
        
        public bool CanChose(int hole)
        {
            if (Circular[hole] > 0)
                return true;
            return false;
        }

        public int GetBean(int hole)
        {
            int beans = Circular[hole];
            if(beans == 2 || beans == 3)
            {
                Circular[hole] = 0;
                return beans;
            }
            else
            {
                return 0;
            }
        }

    }

}


