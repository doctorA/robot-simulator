using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Robot_simulator
{
    public class Crta : Lik
    {
        public Crta()
        {
            tip = 1;
        }

        public override void risi(Conf_rezkar conf)
        {
            if (this.tocke.Count > 1)
            {
                GL.LineWidth(5f);
                GL.Begin(BeginMode.Lines);
                //GL.LineWidth(200f);
                GL.Color3(Color.White);
                for (int i = 0; i < tocke.Count-1; i++)
                {
                    GL.Vertex2(tocke[i]);
                    GL.Vertex2(tocke[i + 1]);
                }
                GL.End();

                GL.PointSize(10f);
                GL.Color3(Color.Red);
                GL.Begin(BeginMode.Points);
                for (int i = 0; i < tocke.Count; i++)
                {
                    GL.Vertex2(tocke[i]);
                }
                GL.End();
            }
        }

        public override void toJBI(Conf_rezkar conf, List<string> tockeList, List<string> premikiList)
        {
            string startPos = string.Format("{0:F3},{1:F3},{2:F3}", conf.zacetna_tocka.X, conf.zacetna_tocka.Y, conf.zacetna_tocka.Z);
            string hitrost = (string.Format("V:{0:F1}", conf.hitrost_restkanja));
            string visinaSvedra = (string.Format("{0:F3}", conf.visina_svedra_med_pomiki));
            string globinaSvedraMedRezkanjem= (string.Format("{0:F3}", conf.globina_med_reskanjem));

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[0].Y, tocke[0].X, visinaSvedra,startPos));
            premikiList.Add("MOVL " + hitrost);

            for (int i = 0; i < tocke.Count; i++)
            {
                tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[i].Y, tocke[i].X, globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVL " + hitrost);
            }

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[tocke.Count - 1].Y, tocke[tocke.Count - 1].X, visinaSvedra, startPos));
            premikiList.Add("MOVL " + hitrost);

        }

    }
}
