using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EchiquierV3
{
    [Serializable]
    class Plateau : Form
    {

        /*--------------------------------------*/
        /*-----Bouttons et menus deroulants-----*/
        /*--------------------------------------*/
        protected MenuStrip menu1;
        protected ToolStripMenuItem parametresTSM;
        protected ToolStripMenuItem couleur_case_noirTSM;
        protected ToolStripMenuItem couleur_case_blancheTSM;
        protected ToolStripMenuItem couleur_case_joueeTSM;
        protected ToolStripMenuItem couleur_case_proposeeTSM;
        protected ToolStripMenuItem modifier_pasTSM;
        protected ToolStripMenuItem modifier_taille_caseTSM;
        protected ToolStripMenuItem fichierTSM;
        protected ToolStripMenuItem sauvegarderTSM;
        protected ToolStripMenuItem chargerTSM;
        protected ToolStripMenuItem nouvelle_partieTSM;
        protected ToolStripMenuItem helpTSM;
        protected ToolStripMenuItem regles_jeuTSM;
        protected ToolStripMenuItem how_to_useTSM;
        protected Button retour_arriereB;
        protected Button abandonnerB;

        /*-------------------------------------*/
        /*---variables internes au programme---*/
        /*-------------------------------------*/
        protected int compteur_coup = 0;
        protected int x, xx, y, yy;
        protected int pas = 1;
        protected int taille_plateau;
        protected int taille_cases;
        protected int taille_form;
        protected int[] liste_coup;
        protected int[] sauvegarde;
        protected string regle = "";
        protected string help = "";
        protected Case[,] grille;
        protected Sauvegarde save = new Sauvegarde();

        /*----------------------------------*/
        /*-----Parametres de la fenetre-----*/
        /*----------------------------------*/
        protected Color couleur_case_noir = Color.Black;
        protected Color couleur_case_blanche = Color.White;
        


        public Plateau()
        {
            this.init_comp();
        }
        public void init_comp()
        {
            /*--------------------------------------*/
            /*----initialisation des parametres-----*/
            /*--------------------------------------*/
            this.menu1 = new MenuStrip();
            this.parametresTSM = new ToolStripMenuItem();
            this.couleur_case_noirTSM = new ToolStripMenuItem();
            this.couleur_case_blancheTSM = new ToolStripMenuItem();
            this.couleur_case_joueeTSM = new ToolStripMenuItem();
            this.couleur_case_proposeeTSM = new ToolStripMenuItem();
            this.modifier_pasTSM = new ToolStripMenuItem();
            this.modifier_taille_caseTSM = new ToolStripMenuItem();
            this.fichierTSM = new ToolStripMenuItem();
            this.sauvegarderTSM = new ToolStripMenuItem();
            this.chargerTSM = new ToolStripMenuItem();
            this.nouvelle_partieTSM = new ToolStripMenuItem();
            this.helpTSM = new ToolStripMenuItem();
            this.regles_jeuTSM = new ToolStripMenuItem();
            this.how_to_useTSM = new ToolStripMenuItem();
            this.retour_arriereB = new Button();
            this.abandonnerB = new Button();


            /*-----------------------------------------*/
            /*----initialisation de la Form Plateau----*/
            /*-----------------------------------------*/

            this.taille_cases = 100;
            this.taille_plateau = 8;
            this.taille_form = this.taille_plateau * this.taille_cases + 120;
            this.Size = new Size(this.taille_form, this.taille_form);
            this.Text = "Echiquier(application du theoreme d'Euler)";
            


            /*-----------------------------------------*/
            /*--initialisation du menustrip principal--*/
            /*-----------------------------------------*/
            this.menu1.Location = new System.Drawing.Point(0, 0);
            this.menu1.Name = "menuStrip1";
            this.menu1.Size = new System.Drawing.Size(250, 24);
            this.menu1.BackColor = Color.Gray;
            this.menu1.TabIndex = 0;
            this.menu1.Text = "menuStrip1";
            this.SuspendLayout();
            this.menu1.SuspendLayout();
            this.liste_coup = new int[128];
            for(int i = 0; i < 128; i++)
            {
                liste_coup[i] = -1;
            }
            this.menu1.Items.AddRange(new ToolStripItem[] 
            {
                this.parametresTSM,
                this.fichierTSM,
                this.helpTSM
            });


            /*--------------------------*/
            /*--initialisation des TSM--*/
            /*--------------------------*/
            this.parametresTSM.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.couleur_case_blancheTSM, 
                this.couleur_case_noirTSM,
                this.couleur_case_joueeTSM,
                this.couleur_case_proposeeTSM,
                this.modifier_pasTSM,
                this.modifier_taille_caseTSM
            });
            this.parametresTSM.Text = "Paramètres";
            this.parametresTSM.Size = new Size(60, 20);

            this.couleur_case_noirTSM.Text = "Changer couleur cases Noirs";
            this.couleur_case_noirTSM.Size = new Size(150, 20);
            this.couleur_case_noirTSM.Click += new EventHandler(click_changer_case_noir);

            this.couleur_case_blancheTSM.Text = "Changer couleur cases Blanches";
            this.couleur_case_blancheTSM.Size = new Size(150, 20);
            this.couleur_case_blancheTSM.Click += new EventHandler(click_changer_case_blanche);

            this.couleur_case_joueeTSM.Text = "Changer couleur cases Jouées";
            this.couleur_case_joueeTSM.Size = new Size(150, 20);
            this.couleur_case_joueeTSM.Click += new EventHandler(click_changer_case_jouee);

            this.couleur_case_proposeeTSM.Text = "Changer couleur cases Proposées";
            this.couleur_case_proposeeTSM.Size = new Size(150, 20);
            this.couleur_case_proposeeTSM.Click += new EventHandler(click_changer_case_proposee);

            this.modifier_pasTSM.Text = "Modifier le pas de retour/avancé";
            this.modifier_pasTSM.Size = new Size(150, 20);

            this.modifier_taille_caseTSM.Text = "Modifier la taille d'une case";
            this.modifier_taille_caseTSM.Size = new Size(150, 20);
            

            this.fichierTSM.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.chargerTSM,
                this.sauvegarderTSM,
                this.nouvelle_partieTSM
            });
            this.fichierTSM.Size = new Size(50, 20);
            this.fichierTSM.Text = "Fichier";

            this.chargerTSM.Text = "Charger la dernière partie";
            this.chargerTSM.Size = new Size(150, 20);
            this.chargerTSM.Click += new EventHandler(click_charger);

            this.sauvegarderTSM.Text = "Sauvegarder";
            this.sauvegarderTSM.Size = new Size(150, 20);
            this.sauvegarderTSM.Click += new EventHandler(click_sauvegarde);

            this.nouvelle_partieTSM.Text = "Nouvelle partie";
            this.nouvelle_partieTSM.Size = new Size(150, 20);
            

            this.helpTSM.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.regles_jeuTSM,
                this.how_to_useTSM
            });
            this.helpTSM.Text = "Help";
            this.helpTSM.Size = new Size(20, 20);

            this.regles_jeuTSM.Text = "Rêgles du jeu";
            this.regles_jeuTSM.Size = new Size(150, 20);
            this.regles_jeuTSM.Click += new EventHandler(regles_click);

            this.how_to_useTSM.Text = "How to Use";
            this.how_to_useTSM.Size = new Size(150, 20);
            this.how_to_useTSM.Click += new EventHandler(help_click);



            this.MainMenuStrip = menu1;
            this.menu1.ResumeLayout(false);
            this.menu1.PerformLayout();
            this.Controls.Add(menu1);

            /*---------------------------------------------*/
            /*----Fin de l'initialisation des MenuStrip----*/
            /*---------------------------------------------*/

            /*-------------------------------------*/
            /*-----Initialisation des Bouttons-----*/
            /*-------------------------------------*/
            this.abandonnerB.Text = "Abandonner...";
            this.abandonnerB.Size = new Size(100, 20);
            this.abandonnerB.Location = new Point(300, 0);
            this.abandonnerB.Enabled = true;
            this.abandonnerB.Click += new EventHandler(abandonnerClic);

            this.retour_arriereB.Size = new Size(100, 20);
            this.retour_arriereB.Text = "Retour arrière";
            this.retour_arriereB.Location = new Point(300, 30);
            this.retour_arriereB.Enabled = true;

            this.Controls.Add(this.abandonnerB);
            this.Controls.Add(this.retour_arriereB);

            /*---------------------------------------*/
            /*------Fin initialisation Bouttons------*/
            /*---------------------------------------*/

        }

        private void help_click(object sender, EventArgs e)
        {
            string str = "";
            str = this.help;
            MessageBox.Show(str, "aide du jeu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void regles_click(object sender, EventArgs e)
        {
            string str = "";
            str = this.regle;
            MessageBox.Show(str, "règles du jeu", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void click_changer_case_proposee(object sender, EventArgs e)
        {
            Color nColor = Color.Green;
            nColor = this.getColor(nColor);
            for(int i = 0; i < taille_plateau; i++)
            {
                for(int j =  0; j < taille_plateau; j++)
                {
                    this.grille[i, j].modifColorCasePropose(nColor);
                    this.grille[i, j].Refresh();
                }
            }
            this.Refresh();
        }

        private void click_changer_case_jouee(object sender, EventArgs e)
        {
            Color nColor = Color.Red;
            nColor = this.getColor(nColor);
            for (int i = 0; i < taille_plateau; i++)
            {
                for(int j = 0; j < taille_plateau; j++)
                {
                    this.grille[i, j].modifColorCaseJoue(nColor);
                    this.grille[i, j].Refresh();
                }
            }
            for (int i = 0; i < compteur_coup; i+=2)
            {
                this.grille[liste_coup[i], liste_coup[i + 1]].modifEtat(1) ;
            }
            this.Refresh();
        }

        
        public void changerTailleCase(int nTaille)
        {
            this.taille_cases = nTaille;
            for(int i = 0; i < taille_plateau; i++)
            {
                for(int j = 0; j < taille_plateau; j++)
                {
                    this.grille[i, j].modifTaille(nTaille);
                    this.grille[i, j].Refresh();
                }
            }
            this.taille_plateau = 8;
            this.taille_form = this.taille_plateau * this.taille_cases + 120;
            this.Size = new Size(taille_form, taille_form);
            this.Refresh();
        }

        private void click_changer_case_blanche(object sender, EventArgs e)
        {
            Color nColor = Color.White;
            nColor = this.getColor(nColor);

            for (int i = 0; i < taille_plateau; i++)
            {
                for (int j = 0; j < taille_plateau; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0) grille[i, j].modifColorCaseBlanche(nColor);
                    if (i % 2 == 1 && j % 2 == 1) grille[i, j].modifColorCaseBlanche(nColor);
                    grille[i,j].Refresh();
                }
            }
            this.Refresh();
        }

        private void click_changer_case_noir(object sender, EventArgs e)
        {
            Color nColor = Color.Black;
            nColor= this.getColor(nColor);
           
            for(int i = 0;  i < taille_plateau; i++)
            {
                for(int j = 0; j < taille_plateau; j++)
                {
                    if (i % 2 == 0 && j % 2 == 1) grille[i, j].modifColorCaseNoir(nColor);
                    if (i % 2 == 1 && j % 2 == 0) grille[i, j].modifColorCaseNoir(nColor);
                    grille[i, j].Refresh();
                }
            }
            this.Refresh();
        }
        public Color getColor(Color colorbase) {
            ColorDialog cD = new ColorDialog();
            if (cD.ShowDialog() == DialogResult.OK)
            {
                return cD.Color;
            }
            return colorbase;
        }

        private void abandonnerClic(object sender, EventArgs e)
        {
           DialogResult d =  MessageBox.Show("Vous abandonnez", "abandonnez ?", MessageBoxButtons.YesNo);

            if (d == DialogResult.Yes) this.Close();
        }

        public Case getCase(int x, int y)
        {
            return this.grille[x, y];
        }
        private void click_charger(object sender, EventArgs e)
        {
            this.sauvegarde = save.charger_partie(this);
        }
        public void click_sauvegarde(object sender, EventArgs e)
        {
            int i;
            int[] sv = new int[compteur_coup];
            for(i = 0; i < compteur_coup; i++)
            {
                sv[i] = liste_coup[i];
            }

            
            
            this.sauvegarde = sv;
            save.sauvegarder(sauvegarde);

        }
        public Sauvegarde getSave()
        {
            return this.save;
        }
        public void changer_pas(int npas)
        {
            this.pas = npas;
        }
        
    }
}
