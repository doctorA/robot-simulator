using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace Robot_simulator
{
    public class Conf_rezkar
    {
        public Vector3 zacetna_tocka;
        public float debelina_svedra;
        public string komentar;
        public string ime;
        public string tool;
        public string nacin_izvajanja;
        public float visina_svedra_pred_rezkanjem;
        public float visina_svedra_med_pomiki;
        public float globina_med_reskanjem;
        public float hitrost_restkanja;
        public bool vklop_orodja;
        public Vector2 vel_ploscice;
        public string datum;

        public Conf_rezkar()
        {
            zacetna_tocka = new Vector3(1.00f, -89.00f, 179.00f);
            debelina_svedra = 3;
            komentar = "KOMENTAR";
            ime = "NINJA ŽELVE";
            tool = "TOOL 3";
            nacin_izvajanja = "USER 5";
            visina_svedra_pred_rezkanjem = 40;
            visina_svedra_med_pomiki = 5;
            globina_med_reskanjem = -1;
            hitrost_restkanja = 20;
            vklop_orodja = true;
            vel_ploscice = new Vector2(40,60);
            datum = DateTime.Now.ToString();

        }
    }
}
