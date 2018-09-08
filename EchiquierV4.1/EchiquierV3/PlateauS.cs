using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EchiquierV3
{
    class PlateauS : Plateau
    {
        Button bContinuer;
        Button b2;
        int mode;
        int premier = 0;
        Boolean b = false;
        int[] historix;
        int compteur_coup = 0;

        int pas = 1;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ModifPas;


        static int[,] echec = new int[12, 12];

        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        int nb_fuite, min_fuite, lmin_fuite = 0;
        int i, j, k, l, ii = -1, jj = -1;



        public PlateauS(int mode)
        {
            this.taille_cases = 50;
            this.taille_form = this.taille_plateau * this.taille_cases + 120;
            this.Size = new Size(this.taille_form, this.taille_form);
            this.Text = "Echiquier(application du theoreme d'Euler)";
            this.grille = new Case[taille_plateau, taille_plateau];
            for (int i = 0; i < taille_plateau; i++)
            {
                for (int j = 0; j < taille_plateau; j++)
                {
                    this.grille[i, j] = new Case(i, j, taille_cases);
                    this.grille[i, j].Click += new EventHandler(click_Case);
                    this.Controls.Add(this.grille[i, j]);
                }
            }
            this.mode = mode;
            if (mode == 0)
            {
                Random random = new Random();
                ii = random.Next(1, 8);
                jj = random.Next(1, 8);
                grille[ii - 1, jj - 1].BackColor = Color.Blue;
                for (i = 0; i < 12; i++)
                    for (j = 0; j < 12; j++)
                        echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);
                i = ii + 1; j = jj + 1;
                echec[i, j] = 1;
                k = 2;
                // ii et jj evoluent de 1 à 8 !
            }
            else if (mode == 2)
            {
                mode = 1;
            }
            else
            {
                ii = -1;
                jj = -1;
            }

        }




        static int fuite(int i, int j)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (echec[i + depi[l], j + depj[l]] != 0) n--;

            return (n == 0) ? 9 : n;
        }

        private void click_continuer(object sender, EventArgs e)
        {
            if (ii == -1 || jj == -1)
            {
                MessageBox.Show("Choisissez une case !");
            }
            if (this.ii > -1 && this.jj > -1)
            {
                if (k <= 65)
                {
                    for (int v = 0; v < pas; v++)
                    {
                        for (l = 0, min_fuite = 11; l < 8; l++)
                        {
                            ii = i + depi[l]; jj = j + depj[l];

                            nb_fuite = ((echec[ii, jj] != 0) ? 10 : fuite(ii, jj));

                            if (nb_fuite < min_fuite)
                            {
                                min_fuite = nb_fuite; lmin_fuite = l;
                            }
                        }
                        if (min_fuite != 9)
                        {
                            i += depi[lmin_fuite]; j += depj[lmin_fuite];
                            echec[i, j] = k;
                            if (i > 1 && i < 10 && j > 1 && j < 10)
                            {
                                if (compteur_coup >= 2)
                                {
                                    this.grille[historix[compteur_coup - 2] - 2, historix[compteur_coup - 1] - 2].Image = EchiquierV3.Properties.Resources.croix2;
                                }
                                this.grille[i - 2, j - 2].modifEtat(1);
                            }
                            this.historix[compteur_coup] = i;
                            this.historix[compteur_coup + 1] = j;
                            compteur_coup += 2;
                            k++;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Cliquez sur une case de depart !");
            }
        }

        private void cBCaseAleat_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            ii = random.Next(1, 8);
            jj = random.Next(1, 8);
            grille[ii - 1, jj - 1].modifEtat(1);
            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);
            i = ii + 1; j = jj + 1;
            echec[i, j] = 1;
            k = 2;

        }
        




        public void setii(int i)
        {
            this.ii = i + 2;
        }
        public void setjj(int j)
        {
            this.jj = j + 2;
        }
        private void click_Case(object sender, EventArgs e)
        {
            if (sender is Case)
            {
                if (premier == 0 && this.mode > 0)
                {
                    Case p = (Case)sender;
                    p.modifEtat(1);
                    this.ii = p.getX() + 1;
                    this.jj = p.getY() + 1;
                    for (i = 0; i < 12; i++)
                        for (j = 0; j < 12; j++)
                            echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);
                    i = ii + 1; j = jj + 1;
                    echec[i, j] = 1;
                    k = 2;
                    this.xx = i - 2;
                    this.yy = j - 2;
                    this.x = xx;
                    this.y = yy;
                    grille[x, y].modifEtat(1);
                    this.liste_coup[0] = x;
                    this.liste_coup[1] = y;
                    compteur_coup += 2;

                    this.premier++;
                }

            }
        }
    }
}
