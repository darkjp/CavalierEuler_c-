using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EchiquierV3
{
    class Case : Button
    {
        int x, y;
        int etat = 0;
        int taille;
        Color couleurCaseNoir = Color.Black;
        Color couleurCaseBlanche = Color.White;
        Color couleurCaseJoue = Color.Red;
        Color couleurCasePropose = Color.Green;
        Image croix;
        public Case(int nX, int nY, int taille)
        {
            this.x = nX;
            this.y = nY;

            this.croix = EchiquierV3.Properties.Resources.croix2;
           
            this.taille = taille;
            this.Size = new Size(taille, taille);
            this.Location = new Point(this.x * taille + 50, this.y * taille + 50);
            this.modifEtat(this.etat);
            this.ImageAlign = ContentAlignment.MiddleCenter;
            
            
        }
        public void raz()
        {
            this.Image = null;
            this.BackgroundImage = null;
        }
        public void modifEtat(int nEtat)
        {
            
            if (nEtat == 0)
            {
                if (this.x % 2 == 0)
                {
                    if (this.y % 2 == 0) this.BackColor = this.couleurCaseBlanche;
                    else this.BackColor = this.couleurCaseNoir;
                }
                else
                {
                    if (this.y % 2 == 0) this.BackColor = this.couleurCaseNoir;
                    else this.BackColor = this.couleurCaseBlanche;
                }
            }else if (nEtat == 1)
            {

                this.BackgroundImageLayout = ImageLayout.Stretch;
                this.BackgroundImage = EchiquierV3.Properties.Resources.cavalier;
                this.BackColor = couleurCaseJoue;
                if (etat == nEtat )
                {
                    this.BackColor = Color.Orange;
                    this.BackgroundImage = null;
                   if (this.taille > 74) this.Image = EchiquierV3.Properties.Resources.croix2;
                }

            }else if (nEtat == 2)
            {
                this.BackColor = this.couleurCasePropose;
            }
            this.etat = nEtat;
            this.Refresh();
        }
        public void modifColorCaseNoir(Color nColor)
        {
            this.couleurCaseNoir = nColor;
            this.modifEtat(this.etat);
        }
        public int getX()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }
        public int getEtat()
        {
            return this.etat;
        }
        public void modifColorCaseBlanche(Color nColor)
        {
            this.couleurCaseBlanche = nColor;
            this.modifEtat(this.etat);
        }
        public void modifColorCaseJoue(Color nColor)
        {
            this.couleurCaseJoue = nColor;
            
        }
        public void modifColorCasePropose(Color nColor)
        {
            this.couleurCasePropose = nColor;
        }
        public void modifTaille(int nTaille)
        {
            this.taille = nTaille;
            this.Size = new Size(this.taille, this.taille);
            this.Location = new Point(this.taille * x + 50, this.taille * y + 50);
            this.Refresh();
        }

    }

}
