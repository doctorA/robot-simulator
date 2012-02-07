using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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
            string startPos = string.Format("{0:F3},{1:F3},{2:F3}", conf.zacetna_tocka.X, conf.zacetna_tocka.Y, conf.zacetna_tocka.Z);
            string hitrost = (string.Format("V:{0:F1}", conf.hitrost_restkanja));
            string visinaSvedra = (string.Format("{0:F3}", conf.visina_svedra_med_pomiki));
            string globinaSvedraMedRezkanjem = (string.Format("{0:F3}", conf.globina_med_reskanjem));

             Vector2 center = tocke[0];
             float radius = (new Vector2(tocke[0].X - tocke[1].X, tocke[0].Y - tocke[1].Y)).Length

            Vector2 p1 = new Vector2(center.X, center.Y + radius);
            Vector2 p2 = new Vector2(center.X + radius, center.Y);
            Vector2 p3 = new Vector2(center.X, center.Y -radius);
            Vector2 p3 = new Vector2(center.X-radius, center.Y);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", p1.Y, p1.X, visinaSvedra, startPos));
            premikiList.Add("MOVL " + hitrost);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", p1.Y, p1.X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVL " + hitrost);

            //krožni premiki
            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", p1.Y, p1.X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVC " + hitrost);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", p2.Y, p2.X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVC " + hitrost);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", p3.Y, p3.X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVC " + hitrost);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", p4.Y, p4.X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVC " + hitrost);

            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", p1.Y, p1.X, globinaSvedraMedRezkanjem, startPos));
            premikiList.Add("MOVC " + hitrost);

            //dvignemo sveder na koncu
            tockeList.Add(string.Format("{0:F3},{1:F3},{2},{3}", p1.Y, p1.X, visinaSvedra, startPos));
            premikiList.Add("MOVL " + hitrost);
        }
    }
}
