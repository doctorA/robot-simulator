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
    class Krog : Lik
    {
        public Krog()
        {
            tip = 2;
        }

 

        public void glCircle3i(Vector2 p1, float radius)
        {
            float angle;
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Color3(Color.White);
            GL.LineWidth(5f);
            GL.Begin(BeginMode.LineLoop);
            for (int i = 0; i < 100; i++)
            {
                angle = i * 2f * (float)Math.PI / 100f;
                GL.Vertex2(p1.X + ((float)Math.Cos(angle) * radius), p1.Y + ((float)Math.Sin(angle) * radius));
            }
            GL.End();
            GL.PopMatrix();
        }  



        public override void risi(Conf_rezkar conf)
        {
            if (this.tocke.Count > 1)
            {
                GL.Enable(EnableCap.LineSmooth);
                GL.ShadeModel(ShadingModel.Smooth);
                GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                glCircle3i(tocke[0], (new Vector2(tocke[0].X - tocke[1].X, tocke[0].Y - tocke[1].Y)).Length);

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

                Vector2 center = tocke[0];
                float radius = (new Vector2(tocke[0].X - tocke[1].X, tocke[0].Y - tocke[1].Y)).Length;

                Vector2 p1 = new Vector2(center.X, center.Y + radius);
                Vector2 p2 = new Vector2(center.X + radius, center.Y);
                Vector2 p3 = new Vector2(center.X, center.Y - radius);
                Vector2 p4 = new Vector2(center.X - radius, center.Y);

                tockeList.Add(string.Format("{0},{1},{2},{3}", p1.Y.ToString("0.000", CultureInfo.InvariantCulture), p1.X.ToString("0.000", CultureInfo.InvariantCulture), visinaSvedra, startPos));
                premikiList.Add("MOVL " + hitrost);

                tockeList.Add(string.Format("{0:},{1},{2},{3}", p1.Y.ToString("0.000", CultureInfo.InvariantCulture), p1.X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVL " + hitrost);

                //krožni premiki
                tockeList.Add(string.Format("{0},{1},{2},{3}", p1.Y.ToString("0.000", CultureInfo.InvariantCulture), p1.X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVC " + hitrost);

                tockeList.Add(string.Format("{0},{1},{2},{3}", p2.Y.ToString("0.000", CultureInfo.InvariantCulture), p2.X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVC " + hitrost);

                tockeList.Add(string.Format("{0},{1},{2},{3}", p3.Y.ToString("0.000", CultureInfo.InvariantCulture), p3.X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVC " + hitrost);

                tockeList.Add(string.Format("{0},{1},{2},{3}", p4.Y.ToString("0.000", CultureInfo.InvariantCulture), p4.X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVC " + hitrost);

                tockeList.Add(string.Format("{0},{1},{2},{3}", p1.Y.ToString("0.000", CultureInfo.InvariantCulture), p1.X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVC " + hitrost);

                //dvignemo sveder na koncu
                tockeList.Add(string.Format("{0},{1},{2},{3}", p1.Y.ToString("0.000", CultureInfo.InvariantCulture), p1.X.ToString("0.000", CultureInfo.InvariantCulture), visinaSvedra, startPos));
                premikiList.Add("MOVL " + hitrost);
            }
        }
    }
}
