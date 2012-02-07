using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Robot_simulator
{
    class Kvadrat : Lik
    {
        public Kvadrat()
        {
            tip = 3;
        }

        public override void risi(Conf_rezkar conf)
        {
            if (this.tocke.Count > 1)
            {
                GL.LineWidth(5f);
                GL.Begin(BeginMode.Lines);
                //GL.LineWidth(200f);
                GL.Color3(Color.White);
                    GL.Vertex2(tocke[0].X, tocke[0].Y); //x1y1
                    GL.Vertex2(tocke[1].X, tocke[0].Y); //x2x1

                    GL.Vertex2(tocke[1].X, tocke[0].Y); //x2y1
                    GL.Vertex2(tocke[1].X, tocke[1].Y); //x2y2

                    GL.Vertex2(tocke[1].X, tocke[1].Y); //x2y2
                    GL.Vertex2(tocke[0].X, tocke[1].Y); //x1y2

                    GL.Vertex2(tocke[0].X, tocke[1].Y); //x1y2
                    GL.Vertex2(tocke[0].X, tocke[0].Y); //x1y1

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
            string globinaSvedraMedRezkanjem = (string.Format("{0:F3}", conf.globina_med_reskanjem));

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[0].Y, tocke[0].X, visinaSvedra,startPos));
            premikiList.Add("MOVL " + hitrost);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[0].Y, tocke[0].X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVL " + hitrost);
            //smo dola

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[1].Y, tocke[0].X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVL " + hitrost);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[1].Y, tocke[1].X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVL " + hitrost);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[0].Y, tocke[1].X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVL " + hitrost);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[0].Y, tocke[0].X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVL " + hitrost);

            //lets go fly 
            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", tocke[0].Y, tocke[0].X, visinaSvedra, startPos));
            premikiList.Add("MOVL " + hitrost);
        }
    }
}
