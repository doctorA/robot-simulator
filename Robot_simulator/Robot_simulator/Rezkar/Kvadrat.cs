using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Globalization;

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
            if (tocke.Count > 0)
            {
                string startPos = conf.zacetna_tocka.X.ToString("0.00", CultureInfo.InvariantCulture) + ',' + conf.zacetna_tocka.Y.ToString("0.00", CultureInfo.InvariantCulture) + ',' + conf.zacetna_tocka.Z.ToString("0.00", CultureInfo.InvariantCulture);
                string hitrost = (string.Format("V={0:F1}", conf.hitrost_restkanja)).Replace(',', '.');
                string visinaSvedra = conf.visina_svedra_med_pomiki.ToString("0.000", CultureInfo.InvariantCulture);
                string globinaSvedraMedRezkanjem = conf.globina_med_reskanjem.ToString("0.000", CultureInfo.InvariantCulture);

                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[0].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[0].X.ToString("0.000", CultureInfo.InvariantCulture), visinaSvedra, startPos));
                premikiList.Add("MOVL " + hitrost);

                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[0].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[0].X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVL " + hitrost);
                //smo dola

                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[1].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[0].X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVL " + hitrost);

                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[1].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[1].X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVL " + hitrost);

                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[0].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[1].X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVL " + hitrost);

                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[0].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[0].X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVL " + hitrost);

                //lets go fly 
                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[0].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[0].X.ToString("0.000", CultureInfo.InvariantCulture), visinaSvedra, startPos));
                premikiList.Add("MOVL " + hitrost);
            }
        }
    }
}
