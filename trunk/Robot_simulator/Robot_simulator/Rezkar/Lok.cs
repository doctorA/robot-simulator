using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Robot_simulator
{
    class Lok : Lik
    {
        Vector2 Center = new Vector2();
        float radius;
        int angle1 = 0;
        int angle2 = 360;
      
        public Lok()
        {
            tip = 4;
        }

        public void glCircle3i(Vector2 p1, float radius)
        {
            float angle;
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Color3(Color.White);
            GL.LineWidth(5f);
            GL.Begin(BeginMode.LineStrip);

            for (int i = angle1; i < angle2; i++)
            {
                 angle = i * 2f * (float)Math.PI / 360f;
                 GL.Vertex2(p1.X + ((float)Math.Cos(angle) * radius), p1.Y + ((float)Math.Sin(angle) * radius));
            }

            GL.End();
            GL.PopMatrix();
        }

        void izracunaj_krog(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            if (!jeOrtogonalno(p1, p2, p3))
                izracunaj_krog_calc(p1, p2, p3);
            else if (!jeOrtogonalno(p1, p3, p2))
                izracunaj_krog_calc(p1, p3, p2);
            else if (!jeOrtogonalno(p2, p1, p3))
                izracunaj_krog_calc(p2, p1, p3);
            else if (!jeOrtogonalno(p2, p3, p1))
                izracunaj_krog_calc(p2, p3, p1);
            else if (!jeOrtogonalno(p3, p2, p1))
                izracunaj_krog_calc(p3, p2, p1);
            else if (!jeOrtogonalno(p3, p1, p2))
                izracunaj_krog_calc(p3, p1, p2);
            else
                return;
        }

        bool jeOrtogonalno(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float y_Delta_a = p2.Y - p1.Y;
            float x_Delta_a = p2.X - p1.X;

            float y_Delta_b = p3.Y - p2.Y;
            float x_Delta_b = p3.X - p2.X;


            if (Math.Abs(x_Delta_a) <= 0.000000001 && Math.Abs(y_Delta_b) <= 0.000000001)
            {
                 return false;
            }

            if (Math.Abs(y_Delta_a) <= 0.0000001)
            {
                return true;
            }
            else if (Math.Abs(y_Delta_b) <= 0.0000001)
            {
                 return true;
            }
            else if (Math.Abs(x_Delta_a) <= 0.000000001)
            {
                 return true;
            }
            else if (Math.Abs(x_Delta_b) <= 0.000000001)
            {
                 return true;
            }
            else return false;

        }

        void izracunaj_krog_calc(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float y_Delta_a = p2.Y - p1.Y;
            float x_Delta_a = p2.X - p1.X;

            float y_Delta_b = p3.Y - p2.Y;
            float x_Delta_b = p3.X - p2.X;

            if (Math.Abs(x_Delta_a) <= 0.000000001 && Math.Abs(y_Delta_b) <= 0.000000001)
            {
                Center.X = 0.5f * (p2.X + p3.X);
                Center.Y = 0.5f * (p1.Y + p2.Y);
                radius = (new Vector2(Center.X-p1.X, Center.Y-p1.Y)).Length;
            }

            if (x_Delta_a == 0 || y_Delta_b == 0)
            {
                return;
            }
            float aSlope = y_Delta_a / x_Delta_a; // 
            float bSlope = y_Delta_b / x_Delta_b;
            if (Math.Abs(aSlope - bSlope) <= 0.000000001)
            {	// tocke so kolinearne	
                
                return;
            }
            Center.X = (aSlope * bSlope * (p1.Y - p3.Y) + bSlope * (p1.X + p2.X) - aSlope * (p2.X + p3.X))/(2*(bSlope-aSlope));
            Center.Y = -1 * (Center.X - (p1.X + p2.X)/2) / aSlope + (p1.Y + p2.Y) / 2;

            radius = (new Vector2(Center.X-p1.X, Center.Y-p1.Y)).Length;

            int temp = (int)izracunajKot(new Vector2(Center.X + radius, Center.Y), Center, p3);
            angle1 = temp;
            angle2 = temp;
            temp = (int)izracunajKot(new Vector2(Center.X + radius, Center.Y), Center, p1);
            if (temp < angle1)
            {
                angle1 = temp;
            }
            else
            {
                angle2 = temp;
            }
            temp = (int)izracunajKot(new Vector2(Center.X + radius, Center.Y), Center, p2);
            if (temp < angle1)
            {
                angle1 = temp;
            }
            else if(temp>angle2)
            {
                angle2 = temp;
            } 
        }

        double izracunajKot(Vector2 p1, Vector2 p2, Vector2 p3)
        {

            Vector2 ab = new Vector2(p1.X - p2.X, p1.Y - p2.Y);
            Vector2 cb = new Vector2(p3.X - p2.X, p3.Y - p2.Y);
            ab.Normalize();
            cb.Normalize();

            /* GL.Begin(BeginMode.Lines);
            {
                GL.Vertex2(p1);
                GL.Vertex2(p2);

                GL.Vertex2(p2);
                GL.Vertex2(p3);
            }*/

            double rslt = (Math.Atan2(cb.Y, cb.X) - Math.Atan2(ab.Y, ab.X));
            //double rslt = Math.Acos(ab.X * cb.X + ab.Y * cb.Y);
            double rs = (rslt * 180) / 3.141592;
            if (rs < 0)
            {
                rs = 360 + rs;
            }
            return rs;
        }

        public override void risi(Conf_rezkar conf)
        {
            if (this.tocke.Count > 2)
            {
                izracunaj_krog(tocke[0], tocke[1], tocke[2]);
                glCircle3i(Center, radius);
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
                // string startPos = string.Format("{0:F3},{1:F3},{2:F3}", conf.zacetna_tocka.X, conf.zacetna_tocka.Y, conf.zacetna_tocka.Z);
                string startPos = conf.zacetna_tocka.X.ToString("0.00", CultureInfo.InvariantCulture) + ',' + conf.zacetna_tocka.Y.ToString("0.00", CultureInfo.InvariantCulture) + ',' + conf.zacetna_tocka.Z.ToString("0.00", CultureInfo.InvariantCulture);
                string hitrost = (string.Format("V:{0:F1}", conf.hitrost_restkanja)).Replace(',', '.');
                string visinaSvedra = conf.visina_svedra_med_pomiki.ToString("0.000", CultureInfo.InvariantCulture);
                string globinaSvedraMedRezkanjem = conf.globina_med_reskanjem.ToString("0.000", CultureInfo.InvariantCulture);

                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[0].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[0].X.ToString("0.000", CultureInfo.InvariantCulture), visinaSvedra, startPos));
                premikiList.Add("MOVL " + hitrost);

                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[0].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[0].X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                premikiList.Add("MOVL " + hitrost);
                //smo dola

                for (int i = 0; i < tocke.Count; i++)
                {
                    tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[i].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[i].X.ToString("0.000", CultureInfo.InvariantCulture), globinaSvedraMedRezkanjem, startPos));
                    premikiList.Add("MOVC " + hitrost);
                }

                tockeList.Add(string.Format("{0},{1},{2},{3}", tocke[tocke.Count - 1].Y.ToString("0.000", CultureInfo.InvariantCulture), tocke[tocke.Count - 1].X.ToString("0.000", CultureInfo.InvariantCulture), visinaSvedra, startPos));
                premikiList.Add("MOVL " + hitrost);
            }




        }

    }
}
