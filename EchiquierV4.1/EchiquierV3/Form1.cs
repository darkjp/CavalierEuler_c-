using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EchiquierV3
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem simulationMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem choixDepart;
        private System.Windows.Forms.ToolStripMenuItem departRand;
        private System.Windows.Forms.ToolStripMenuItem Jouer;
        private System.Windows.Forms.ToolStripMenuItem ChargerPartie;
        private System.Windows.Forms.ToolStripMenuItem nouvelle_partie;
        private PictureBox pb1;
        int[] sauvegarde;

        public Form1()
        {
            InitializeComponent();
            this.pb1 = new PictureBox();
            this.pb1.Size = new Size(280, 300);
            this.pb1.Location = new Point(20, 25);
            this.pb1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pb1.Image = EchiquierV3.Properties.Resources.cavalier2;
            this.pb1.BackgroundImage = EchiquierV3.Properties.Resources.univers;
            this.Controls.Add(pb1);
            this.Size = new Size(300, 420);
            this.BackColor = Color.Black;

            //initialisation des menuStrip
            menuStrip1 = new MenuStrip();

            simulationMenuStrip = new ToolStripMenuItem();
            choixDepart = new ToolStripMenuItem();
            departRand = new ToolStripMenuItem();
            Jouer = new ToolStripMenuItem();
            ChargerPartie = new ToolStripMenuItem();
            nouvelle_partie = new ToolStripMenuItem();

            this.menuStrip1.Location = new System.Drawing.Point(10, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(300, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.BackColor = Color.AliceBlue;
            this.menuStrip1.Text = "menuStrip1";
            this.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                simulationMenuStrip, Jouer
            });

            
            this.simulationMenuStrip.Size = new Size(50, 20);
            this.simulationMenuStrip.Name = "Simulation Menu Strip";
            this.simulationMenuStrip.Text = "Simulation ";
            this.simulationMenuStrip.Click += new EventHandler(click_simulation);

            this.Jouer.DropDownItems.AddRange(new ToolStripItem[] { ChargerPartie, nouvelle_partie });
            this.Jouer.Size = new Size(50, 20);
            this.Jouer.Text = "Jouer";

            this.nouvelle_partie.Size = new Size(50, 20);
            this.nouvelle_partie.Text = "Nouvelle partie";
            this.nouvelle_partie.Click += new EventHandler(departJouer);


            this.ChargerPartie.Size = new Size(50, 20);
            this.ChargerPartie.Text = "Charger partie";
            this.ChargerPartie.Click += new EventHandler(click_charger_partie);


            this.choixDepart.Size = new Size(50, 20);
            this.choixDepart.Text = "choix case depart ";

            this.departRand.Size = new Size(50, 20);
            this.departRand.Text = "Random";


            this.Text = "fenetre de depart";
            this.Size = new Size(600, 600);

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainMenuStrip = menuStrip1;
            this.Controls.Add(menuStrip1);
            InitializeComponent();
        }

        private void click_simulation(object sender, EventArgs e)
        {
            PlateauSimulation pS = new PlateauSimulation(0);
            pS.Show();
        }

        private void click_charger_partie(object sender, EventArgs e)
        {
            PlateauJ p = new PlateauJ();
            PlateauJ pj = new PlateauJ(p.getSave().charger_partie(p));
           
        }

        private void departJouer(object sender, EventArgs e)
        {
            PlateauJ p = new PlateauJ();
            p.Show();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
