using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchiquierV3
{
    class Deplacement
    {
        int[] listeDepl;
        public Deplacement()
        {
            listeDepl = new int[16] { 2, -1, 1, -2, -1, -2, -2, -1, -2, 1, -1, 2, 1, 2, 2, 1 };

        }
        public Boolean estDedans(int x, int y)
        {
            for (int i = 0; i < 16; i += 2)
            {
                if (x == listeDepl[i] && y == listeDepl[i + 1])
                {
                    return true;
                }
            }
            return false;
        }
        public int[] get_liste()
        {
            return listeDepl;
        }
    }
}
