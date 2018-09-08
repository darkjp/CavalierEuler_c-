using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EchiquierV3
{
    class PlateauSimulation : Plateau
    {
        Button bContinuer;
        Button bRetourArriere;
        Button choix_case_depart;
        TrackBar selectPasTB;
        Label l_pas;
        Label l_menu_pas;
        Label compteur;
        int mode;
        int premier = 0;
        Boolean b = false;
        int[] historix;






        static int[,] echec = new int[12, 12];

        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        int nb_fuite, min_fuite, lmin_fuite = 0;
        int i, j, k, l, ii = -1, jj = -1;



        public PlateauSimulation(int mode)
        {
            
            this.taille_cases = 50;
            this.taille_form = this.taille_plateau * this.taille_cases + 120;
            this.Size = new Size(this.taille_form + 200, this.taille_form);
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
            this.x = -1;
            this.y = -1;
            this.xx = x;
            this.yy = y;
            Random random = new Random();
            ii = random.Next(1, 8);
            jj = random.Next(1, 8);



            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);
            i = ii + 1; j = jj + 1;
            grille[ii - 1, jj - 1].modifEtat(1);
            echec[i, j] = 1;
            k = 2;

            this.mode = mode;
            if (mode == 0)
            {

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
            initComp();

        }
        public void changerTailleCase(int nTaille)
        {
            this.taille_cases = nTaille;
            for (int i = 0; i < taille_plateau; i++)
            {
                for (int j = 0; j < taille_plateau; j++)
                {
                    this.grille[i, j].modifTaille(nTaille);
                    this.grille[i, j].Refresh();
                }
            }
            this.taille_plateau = 8;
            this.taille_form = this.taille_plateau * this.taille_cases + 120;
            this.Size = AutoScaleDimensions.ToSize();
            this.taille_cases = 50;
            this.taille_form = this.taille_plateau * this.taille_cases + 120;
            this.taille_form += 200;
            this.Size = new Size(this.taille_form, this.taille_form - 200);
            this.bContinuer.Location = new Point(taille_form + 200, taille_form / 2);
            this.selectPasTB.Location = new Point(taille_form + 250, 100);
            this.l_pas.Location = new Point(taille_form + 200, 60);
            this.bRetourArriere.Location = new Point(taille_form + 100, taille_form / 2 + 30);
            this.Controls.Add(bContinuer);
            this.Controls.Add(selectPasTB);
            this.Controls.Add(l_pas);
            this.Controls.Add(bRetourArriere);
            this.Refresh();
        }
        private void click_modif_taille_case(object sender, EventArgs e)
        {
            Entre_chiffre eC = new Entre_chiffre(this, 1);
            eC.Show();

        }
        public void setXX(int n)
        {
            this.xx = n;
        }
        public void setYY(int n)
        {
            this.yy = n;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PlateauSimulation
            // 
            this.BackgroundImage = global::EchiquierV3.Properties.Resources.univers;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(904, 733);
            this.Name = "PlateauSimulation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void initComp()
        {
            this.modifier_taille_caseTSM.Click += new EventHandler(click_modif_taille_case);
            this.bContinuer = new Button();
            this.bContinuer.Size = new Size(100, 20);
            this.bContinuer.Location = new Point(taille_form + 10, taille_form / 2);
            this.bContinuer.Text = "Continuer";
            this.bContinuer.Enabled = true;
            this.bContinuer.Visible = true;
            this.bContinuer.Click += new EventHandler(click_continuer);
            this.Controls.Add(this.bContinuer);
            this.choix_case_depart = new Button();
            this.choix_case_depart.Location = new Point(taille_form - 35, taille_form - 150);
            this.choix_case_depart.Size = new Size(190, 30);
            this.choix_case_depart.Font = new Font("verdana", 10);
            this.choix_case_depart.Text = "Choix Aleatoire de Depart";
            this.choix_case_depart.Click += new EventHandler(choix_case_depart_click);
            this.Controls.Add(this.choix_case_depart);
            this.historix = liste_coup;
            this.selectPasTB = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.selectPasTB)).BeginInit();
            this.SuspendLayout();
            this.selectPasTB.Size = new Size(190, 50);
            this.selectPasTB.Location = new Point(taille_form - 50, 130);
            this.selectPasTB.Minimum = 1;
            this.selectPasTB.Maximum = 15;
            this.selectPasTB.Enabled = true;
            this.selectPasTB.ValueChanged += new System.EventHandler(selectTB);
            this.Controls.Add(selectPasTB);
            this.l_pas = new Label();
            this.l_pas.Font = new Font("arial", 12);
            this.l_pas.Text = "pas : 1";
            this.l_pas.Location = new Point(taille_form, 80);
            this.Controls.Add(l_pas);
            this.l_menu_pas = new Label();
            this.l_menu_pas.Font = new Font("verdana", 15);
            this.l_menu_pas.Location = new Point(taille_form - 40, 15);
            this.l_menu_pas.Size = new Size(200, 60);
            this.l_menu_pas.Text = "Choisissez votre pas :";
            this.Controls.Add(l_menu_pas);
            this.compteur = new Label();
            this.compteur.Text = "Case numéros : " + this.compteur_coup;
            this.compteur.Font = new Font("Verdana", 15);
            this.compteur.Size = new Size(200, 60);
            this.compteur.Location = new Point(this.taille_form - 40, taille_form - 100);
            this.Controls.Add(compteur);

            ((System.ComponentModel.ISupportInitialize)(this.selectPasTB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.nouvelle_partieTSM.Click += new EventHandler(departJouer);
            this.bRetourArriere = new Button();
            this.bRetourArriere.Text = "Retour en Arriere";
            this.bRetourArriere.Size = new Size(100, 20);
            this.bRetourArriere.Location = new Point(taille_form + 10, taille_form / 2 + 30);
            this.bRetourArriere.Click += new EventHandler(retourEnArriere);
            this.Controls.Add(this.bRetourArriere);
            this.BackgroundImage = EchiquierV3.Properties.Resources.univers;
            this.retour_arriereB.Visible = false;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            this.regle = "" +
                "*********************Etape 1*********************\n\n" +
                "Cliquez sur la case de départ que vous souhaitez\n" +
                "Ou cliquez sur continuer si vous voulez qu'elle soit\n" +
                "Choisi aléatoirement\n\n" +
                "********************Etape 2 (la simulation)*********\n" +
                "\nVous pouvez soit :\n" +
                "\tCliquer sur continuer pour avancer de n pas\n" +
                "\tCliquer sur retour arrière pour revenir de n pas\n" +
                "\n" +
                "*********************Etape 3 (optionnelle)**********\n\n" +
                "Vous pouvez recommencer une simulation en faisant :\n" +
                "\tfichier/recommencer\n" +
                "Abandonner vous permet de quitter la partie en cour\n" +
                "";
        }
        public void modif_case_echec(int x, int y, int n)
        {
            echec[x, y] = n;
        }
        private void choix_case_depart_click(object sender, EventArgs e)
        {

            PlateauSimulation ps = new PlateauSimulation(0);

            
            ps.grille[ps.ii - 1, ps.jj - 1].modifEtat(1);
            ps.modif_case_echec(i, j, 1);
            ps.k = 2;
            this.Close();
            ps.Show();
        }


        private void departJouer(object sender, EventArgs e)
        {
            if (MessageBox.Show("Attention la progression ne sera pas sauvegarder", "recommencer ?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PlateauSimulation pS = new PlateauSimulation(0);
                this.Close();
                pS.Show();
            }
        }

        private void selectTB(object sender, EventArgs e)
        {
            if (sender is TrackBar)
            {
                TrackBar tB = (TrackBar)sender;
                this.l_pas.Text = "pas : " + tB.Value;
                this.pas = tB.Value;
                this.Refresh();
            }
        }
        public void retourEnArriere(object sender, EventArgs e)
        {
            for (int v = 0; v < pas; v++)
            {
                if (compteur_coup > 0)
                {
                    echec[historix[compteur_coup - 2], historix[compteur_coup - 1]] = 0;
                    this.grille[historix[compteur_coup - 2] - 2, historix[compteur_coup - 1] - 2].modifEtat(0);
                    this.grille[historix[compteur_coup - 2] - 2, historix[compteur_coup - 1] - 2].BackgroundImage = null;
                    this.grille[historix[compteur_coup - 2] - 2, historix[compteur_coup - 1] - 2].Image = null;
                    compteur_coup -= 2;
                    k--;
                }
            }
        }



        private void click_continuer(object sender, EventArgs e)
        {
            int a = compteur_coup / 2 + 1;
            this.compteur.Text = "Case numéros : " + a;
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
        }
        static int fuite(int i, int j)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (echec[i + depi[l], j + depj[l]] != 0) n--;

            return (n == 0) ? 9 : n;
        }
        private void click_Case(object sender, EventArgs e)
        {
            if (sender is Case)
            {
                if (premier == 0 && this.xx == -1)
                {
                    grille[ii-1, jj-1].modifEtat(0);
                    grille[ii-1, jj-1].BackgroundImage = null;
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
