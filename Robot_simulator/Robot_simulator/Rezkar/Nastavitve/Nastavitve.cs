using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Robot_simulator
{
    public partial class Nastavitve : Form
    {
        public Conf_rezkar conf1;
        public Nastavitve(Conf_rezkar conf)
        {
            InitializeComponent();
            textBox_globina.Text = conf.globina_med_reskanjem.ToString();
            textBox_ime.Text = conf.ime;
            textBox_komentar.Text = conf.komentar;
            textBox_nacin.Text = conf.nacin_izvajanja;
            textBox_orodje.Text = conf.tool;
            textBox_premer.Text = conf.debelina_svedra.ToString();
            textBox_sirina.Text = conf.vel_ploscice.X.ToString();
            textBox_visina.Text = conf.vel_ploscice.Y.ToString();
            textBox_visina_med.Text = conf.visina_svedra_med_pomiki.ToString();
            textBox_visina_pred.Text = conf.visina_svedra_pred_rezkanjem.ToString();
            textBox_x.Text = conf.zacetna_tocka.X.ToString();
            textBox_y.Text = conf.zacetna_tocka.Y.ToString();
            textBox_z.Text = conf.zacetna_tocka.Z.ToString();
            textBox_datum.Text = DateTime.Now.ToString();
            textBox_hitrost.Text = conf.hitrost_restkanja.ToString();
            if (conf.vklop_orodja)
            {
                checkBox_vklop.Checked = true;
            }
            this.conf1 = conf;
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conf1.vklop_orodja = checkBox_vklop.Checked;
                conf1.datum = textBox_datum.Text;
                conf1.debelina_svedra = float.Parse(textBox_premer.Text);
                conf1.globina_med_reskanjem = float.Parse(textBox_globina.Text);
                conf1.hitrost_restkanja = float.Parse(textBox_hitrost.Text);
                conf1.ime = textBox_ime.Text;
                conf1.komentar = textBox_komentar.Text;
                conf1.nacin_izvajanja = textBox_nacin.Text;
                conf1.tool = textBox_orodje.Text;
                conf1.vel_ploscice = new Vector2(float.Parse(textBox_sirina.Text), float.Parse(textBox_visina.Text));
                conf1.visina_svedra_med_pomiki = float.Parse(textBox_visina_med.Text);
                conf1.visina_svedra_pred_rezkanjem = float.Parse(textBox_visina_pred.Text);
                conf1.zacetna_tocka = new Vector3(float.Parse(textBox_x.Text), float.Parse(textBox_y.Text), float.Parse(textBox_z.Text));
                this.Close();
           }
           catch
           {
               MessageBox.Show("Zajebo si!");
           }
        }
    }
}
