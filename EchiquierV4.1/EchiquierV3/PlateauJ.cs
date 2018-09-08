using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EchiquierV3
{
    
    class PlateauJ : Plateau
    {
        int[] sauvegarde = null;
        Deplacement d = new Deplacement();
        public PlateauJ() {
            initComp();
        }
        public PlateauJ(int[] save)
        {
            initComp();
            
            sauvegarde = save;
            if (sauvegarde.Length > 2)
            {
                this.compteur_coup = 0;
                this.xx = this.grille[sauvegarde[0], sauvegarde[1]].getX();
                this.yy = this.grille[sauvegarde[0], sauvegarde[1]].getY();
                this.x = this.grille[sauvegarde[0], sauvegarde[1]].getX();
                this.y = this.grille[sauvegarde[0], sauvegarde[1]].getY();
                this.liste_coup[0] = x;
                this.liste_coup[ 1] = y;

                for (int i = 2; i < sauvegarde.Length ; i += 2)
                {
                    //this.grille[x, y].Image = EchiquierV3.Properties.Resources.croix2;

                    this.grille[x, y].modifEtat(1);
                    this.liste_coup[i] = this.grille[sauvegarde[i], sauvegarde[i + 1]].getX();
                    this.liste_coup[i + 1] = this.grille[sauvegarde[i], sauvegarde[i + 1]].getY();
                    colorCase(0);
                    this.x = this.grille[sauvegarde[i], sauvegarde[i + 1]].getX();
                    this.y = this.grille[sauvegarde[i], sauvegarde[i + 1]].getY();
                    this.grille[x, y].modifEtat(1);
                    colorCase(2);

                    compteur_coup += 2;
                    if (nb_coups_possible() == 0)
                    {
                        MessageBox.Show("Vous avez perdu :/\n vous pouvez recommencer ^^\nIl vous restait : " + get_nb_cases_restante() + " coups restant");
                    }
                }
            }else if (sauvegarde.Length == 2)
            {
                compteur_coup = 0;
                this.x = save[0];
                this.y = save[1];
                this.liste_coup[0] = x;
                this.liste_coup[1] = y;
                this.xx = this.x;
                this.yy = this.y;
                this.grille[x, y].modifEtat(1);
                this.colorCase(2);
                compteur_coup += 2;
            }
            
        }
        private void click_modif_taille_case(object sender, EventArgs e)
        {
            Entre_chiffre eC = new Entre_chiffre(this, 1);
            eC.Show();

        }
        public void initComp()
        {
            this.grille = new Case[taille_plateau, taille_plateau];
            this.retour_arriereB.Click += new EventHandler(retourArriereClick);
            this.modifier_taille_caseTSM.Click += new EventHandler(click_modif_taille_case);
            this.nouvelle_partieTSM.Click += new EventHandler(departJouer);
            for (int i = 0; i < taille_plateau; i++)
            {
                for (int j = 0; j < taille_plateau; j++)
                {
                    this.grille[i, j] = new Case(i, j, taille_cases);
                    this.Controls.Add(grille[i, j]);
                    this.grille[i, j].Click += new EventHandler(case_click);
                }
            }
            this.liste_coup = new int[128];
            EchiquierV3.Properties.Resources.croix2.SetResolution(this.taille_cases, taille_cases);
            this.regle =
                "*********************Etape 1*********************\n\n" +
                "Cliquez sur la case de départ que vous souhaitez\n\n" +
                "********************Etape 2 (le jeu)**************\n" +
                "\nVous pouvez cliquer sur les cases correspondant aux\n" +
                "deplacement possible du cavalier\n" +
                "Vous pouvez revenir en arrière avec le bouton du meme nom\n" +
                "Vous ne pouvez passer qu'une seule fois par case\n" +
                "Le jeu se fini si vous avez rempli les 64 cases de \n" +
                "l'echiquier\n" +
                "Ou si vous n'avez plus de coups possible\n\n" +
                "*********************Etape 3 (optionnelle)**********\n\n" +
                "Vous pouvez aller sur fichier/sauvegarder pour \n" +
                "sauvegarder la partie en cour (une sauvegarde max)\n" +
                "Vous pouvez charger la derniere partie sauvegardée\n" +
                "en faisant fichier/charger\n" +
                "Abandonner vous permet de quitter la partie en cour\n";
            this.help =
                "*******************Parametres******************\n" +
                "\nChanger les couleur :\n" +
                "\tvous pouvez changer la couleur des cases noirs ou blanches\n" +
                "\tVous pouvez changer la couleur des cases Jouées \n" +
                "\tVous pouvez changer la couleur des cases proposées\n" +
                "Changer la taille des case et par la meme la taille \n" +
                "de la fenêtre.\n\n" +
                "******************fichier******************\n\n" +
                "Sauvegarder sauvegarde la partie en cour (une seule \n" +
                "sauvegarde maximum)\n" +
                "Charger charge la dernière partie sauvegarder si elle\n" +
                "existe, et l'ouvre dans une nouvelle fenêtre (ferme\n" +
                "la fenêtre courante ).\n" +
                "Nouvelle partie cré un nouveau plateau vide\n";
        }
        private void departJouer(object sender, EventArgs e)
        {
            PlateauJ p = new PlateauJ();
            this.Close();
            p.Show();
        }

        private void case_click(object sender, EventArgs e)
        {
            if (sender is Case)
            {
                Case c = (Case)sender;

                if (compteur_coup == 0)
                {
                    this.x = c.getX();
                    this.y = c.getY();
                    c.modifEtat(1);
                    this.xx = this.x;
                    this.yy = this.y;
                    this.liste_coup[this.compteur_coup] = c.getX();
                    this.liste_coup[this.compteur_coup + 1] = c.getY();


                    compteur_coup += 2;
                    colorCase(2);
                    for(int i = 1; i < pas; i++)
                    {
                        this.coup_unique(c);
                    }

                }
                else if (compteur_coup >= 128)
                {
                    MessageBox.Show("Vous avez Gagné !!!");

                }
                else
                {
                    
                    if (c.getEtat() == 2)
                    {
                        for (int i = 0; i < this.pas; i++)
                        {
                            this.coup_unique(c);
                        }
                    }
                }

            }
        }
        public void coup_unique(Case c)
        {
            this.grille[x, y].Image = EchiquierV3.Properties.Resources.croix2;

            c.modifEtat(1);
            this.liste_coup[compteur_coup] = c.getX();
            this.liste_coup[compteur_coup + 1] = c.getY();
            colorCase(0);
            this.x = c.getX();
            this.y = c.getY();

            colorCase(2);

            compteur_coup += 2;
            if (nb_coups_possible() == 0)
            {
                MessageBox.Show("Vous avez perdu :/\n vous pouvez recommencer ^^\nIl vous restait : " + get_nb_cases_restante() + " coups restant");
            }
        }
        public void colorCase(int f)
        {
            int[] t = d.get_liste();

            for (int i = 0; i < t.Length; i += 2)
            {
                if (x + t[i] < 8 && x + t[i] >= 0 && y + t[i + 1] < 8 && y + t[i + 1] >= 0)
                {
                    if (this.grille[x + t[i], y + t[i + 1]].getEtat() != 1)
                    {
                        this.grille[x + t[i], y + t[i + 1]].modifEtat(f);
                    }
                }
            }


        }
        public int nb_coups_possible()
        {
            int ct = 0;
            int[] t = d.get_liste();
            for (int i = 0; i < 16; i += 2)
            {
                if (x + t[i] < 8 && x + t[i] >= 0 && y + t[i + 1] < 8 && y + t[i + 1] >= 0)
                    if (grille[x + t[i], y + t[i + 1]].getEtat() == 2) ++ct;
            }
            return ct;
        }
        public int get_nb_cases_restante()
        {
            int ct = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int e = 0; e < 8; ++e)
                {
                    if (grille[i, e].getEtat() == 0 || grille[i, e].getEtat() == 2) ++ct;
                }
            }

            return ct;
        }
        private void retourArriereClick(object sender, EventArgs e)
        {
            colorCase(0);
            for (int v = 0; v < this.pas; v++)
            {
                if (compteur_coup > 2)
                {
                    this.grille[liste_coup[compteur_coup - 2], liste_coup[compteur_coup - 1]].raz();
                    this.grille[liste_coup[compteur_coup - 2], liste_coup[compteur_coup - 1]].modifEtat(0);
                    compteur_coup -= 2;
                    x = liste_coup[compteur_coup - 2];
                    y = liste_coup[compteur_coup - 1];
                    this.grille[x, y].Image = null;
                }
            }
            colorCase(2);
        }

    }
}
