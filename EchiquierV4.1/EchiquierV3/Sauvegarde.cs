using System;
using System.Collections.Generic;
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
    class Sauvegarde
    {
        public int[] charger_partie(Plateau pj)
        {
            int[] sauvegarde = null;
            String dossierAppli = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            String cheminFichier = Path.Combine(dossierAppli, "save.sv");
            if (File.Exists(Directory.GetCurrentDirectory() + @"\save.sv"))
            {
                IFormatter formatRestaure = new BinaryFormatter();
                Stream streamRestaure = new FileStream("save.sv", FileMode.Open,
                FileAccess.Read,
                FileShare.Read);
                sauvegarde = (int[])formatRestaure.Deserialize(streamRestaure);
                PlateauJ pJ = new PlateauJ(sauvegarde);
                streamRestaure.Close();
                pJ.Show();
            }
            else
            {
                MessageBox.Show("Fichier de sauvegarde inexistant...");
            }
            pj.Close();
            return sauvegarde;
        }
        public void sauvegarder(int[] liste)
        {
            int[] sauvegarde = liste;
            String dossierAppli = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            String cheminFichier = Path.Combine(dossierAppli, "save.sv");
            if (File.Exists(Directory.GetCurrentDirectory() + @"\save.sv"))
            {
                if (sauvegarde != null)
                {
                    var result = MessageBox.Show(" voulez-vous l'ecraser ?", "Fichier de sauvegarde existant,", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        File.Delete(Directory.GetCurrentDirectory() + @"\save.sv");
                        this.creer_fichier(liste);
                    }
                }
                else
                {
                    MessageBox.Show("Vous n'avez pas joué ! :o");
                }
            }
            else
            {
                this.creer_fichier(liste);
            }
        }
        public void creer_fichier(int[] liste)
        {
            IFormatter format = new BinaryFormatter();
            Stream stream = new FileStream("save.sv", FileMode.Create, FileAccess.Write, FileShare.None);
            format.Serialize(stream, liste);
            stream.Close();
            MessageBox.Show("Sauvegarde effectuée !");
        }
        
    }
}
