using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace etp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        DataClasses1DataContext bd = new DataClasses1DataContext(); //accès a notre table 

        private void button1_Click(object sender, EventArgs e)
        {
            //charger information à partir base donnée
            var charger = (from a in bd.profil
                select a.@ref + "---" + a.pseudo + "---" + a.score + "---");
            // copier dans une list box
            listBox1.Items.AddRange(charger.ToArray());
        }
    }
}
