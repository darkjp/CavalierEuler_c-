using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EchiquierV3
{
    class Entre_chiffre : Form
    {
        TextBox tb1;
        Button b1;
        String text;
        Label l1;
        int rep;
        int choix;
        Plateau pl;
        public Entre_chiffre(Plateau p, int choix)
        {
            this.tb1 = new TextBox();
            this.l1 = new Label();
            this.l1.Location = new System.Drawing.Point(0, 10);
            this.l1.Size = new System.Drawing.Size(45, 20);
            this.b1 = new Button();
            this.Size = new System.Drawing.Size(200, 100);
            this.Text = "entrez un chiffre :";
            this.tb1.Text = "";
            this.tb1.Size = new System.Drawing.Size(100, 20);
            this.tb1.Location = new System.Drawing.Point(50, 10);

            this.b1.Size = new System.Drawing.Size(50, 20);
            this.b1.Location = new System.Drawing.Point(10, 31);
            this.b1.Text = "OK";
            this.b1.Click += new EventHandler(cliquez);

            this.pl = p;
            this.Controls.Add(b1);
            this.Controls.Add(l1);
            this.Controls.Add(tb1);
            this.choix = choix;
        }
        public void cliquez(object sender, EventArgs e)
        {
            this.text = this.tb1.Text;
            if (int.TryParse(text, out this.rep))
            {
                if (choix == 1) pl.changerTailleCase(rep);
                else pl.changer_pas(rep);
                this.Close();
            }
        }
        
    }
}
