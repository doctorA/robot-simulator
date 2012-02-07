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
using GLU = OpenTK.Graphics.Glu;

namespace Robot_simulator
{
    public partial class Rezkar : Form
    {
        Conf_rezkar conf = new Conf_rezkar();
        List<Lik> liki = new List<Lik>();
        bool tempKrogBool = false;
        bool tempKvadratBool = false;
        bool tempBrisi = false;
        bool tempPremakni = false;
        bool tempPremakniPremikam = true;
        int premikam_index1 = -1;
        int premikam_index2 = -1;
        Krog tempKrog = new Krog();
        Kvadrat tempKvadrat = new Kvadrat();
       

        public Rezkar()
        {
            InitializeComponent();
            tempKrog.tocke.Add(new Vector2());
            tempKrog.tocke.Add(new Vector2());
            tempKvadrat.tocke.Add(new Vector2());
            tempKvadrat.tocke.Add(new Vector2());
        }

        private void SetupViewport()
        {
            GL.Enable(EnableCap.LineSmooth);
            GL.ShadeModel(ShadingModel.Smooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            int w = glControl1.Width;
            int h = glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-5, conf.vel_ploscice.X+5, -5, conf.vel_ploscice.Y+5, -1, 1);
            GL.Viewport(0, 0, w, h);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            SetupViewport();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Begin(BeginMode.Quads);
            GL.Color3(Color.Blue);
            GL.Vertex2(0, 0);
            GL.Vertex2(conf.vel_ploscice.X, 0);
            GL.Vertex2(conf.vel_ploscice.X, conf.vel_ploscice.Y);
            GL.Vertex2(0, conf.vel_ploscice.Y);
            GL.End();

            foreach (Lik L in liki)
            {
                L.risi(conf);
            }
            if (tempKrogBool)
            {
                tempKrog.risi(conf);
            }
            if (tempKvadratBool)
            {
                tempKvadrat.risi(conf);
            }
            if (liki.Count > 0)
            {
                if (liki.Last().tip == 4 && (liki.Last().tocke.Count == 1 || liki.Last().tocke.Count == 2))
                {
                    GL.Color3(Color.Yellow);
                    GL.PointSize(10f);
                    GL.Begin(BeginMode.Points);
                    for (int i = 0; i < liki.Last().tocke.Count; i++)
                    {
                        GL.Vertex2(liki.Last().tocke[i]);
                    }
                    GL.End();
                }
            }
            glControl1.SwapBuffers();
        }

        private void button_crta_Click(object sender, EventArgs e)
        {
            tempBrisi = false;
            tempKrogBool = false;
            tempKvadratBool = false;
            liki.Add(new Crta());
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {

            float x = ((float)e.X / (float)glControl1.Width) * (float)(conf.vel_ploscice.X + 10) - 5f;
            float y = ((float)e.Y / (float)glControl1.Height) * (float)(conf.vel_ploscice.Y + 10) - 5f;
            y = ((float)(conf.vel_ploscice.Y + 10) - 10f) - y;
            if (liki.Count>0)
            {
                if (tempBrisi)
                {
                    float razdalja = 3f;
                    int index = -1;
                    Vector2 v = (new Vector2(x, y));
                    for (int i = 0; i < liki.Count; i++ )
                    {
                        if (razdalja > liki[i].razdalja(v))
                        {
                            razdalja = liki[i].razdalja(v);
                            index = i;
                        }
                    }
                    if(index>=0)
                    {
                        liki.RemoveAt(index);
                    }
                }
                else if (tempPremakni)
                {
                    if (!tempPremakniPremikam)
                    {
                        float razdalja = 3f;
                        int index1 = -1;
                        int index2 = 0;
                        int temp = 0;
                        Vector2 v = (new Vector2(x, y));
                        for (int i = 0; i < liki.Count; i++)
                        {
                            if (razdalja > liki[i].razdalja_ind(v, ref temp))
                            {
                                razdalja = liki[i].razdalja_ind(v, ref index2);
                                index1 = i;
                            }
                        }
                        if (index1 >= 0)
                        {
                            tempPremakniPremikam = true;
                            premikam_index1 = index1;
                            premikam_index2 = index2;
                        }
                    }
                    else
                    {
                        tempPremakniPremikam = false;
                    }
                }
                else
                {
                    if (liki.Last().tip == 2 && liki.Last().tocke.Count == 2) //krog
                    {
                        tempKrogBool = false;
                        liki.Add(new Krog());
                        //tempKrog = new Krog();
                    }
                    else if (liki.Last().tip == 3 && liki.Last().tocke.Count == 2) //kvadrat
                    {
                        tempKvadratBool = false;
                        liki.Add(new Kvadrat());
                    }
                    else if (liki.Last().tip == 4 && liki.Last().tocke.Count == 3)
                    {
                        liki.Add(new Lok());
                    }
                    if (liki.Last().tip == 2 && liki.Last().tocke.Count == 0)
                    {
                        tempKrog.tocke[0] = new Vector2(x, y);
                    }
                    else if (liki.Last().tip == 3 && liki.Last().tocke.Count == 0)
                    {
                        tempKvadrat.tocke[0] = new Vector2(x, y);
                    }
                    liki.Last().tocke.Add(new Vector2(x, y));
                }
                glControl1.Invalidate();
            }
        }

        private void button_krog_Click(object sender, EventArgs e)
        {
            tempKrogBool = false;
            tempKvadratBool = false;
            tempBrisi = false;
            tempPremakni = false;
            tempPremakniPremikam = false;
            liki.Add(new Krog());
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (liki.Count > 0)
            {
                if (tempPremakniPremikam && tempPremakni)
                {
                    float x = ((float)e.X / (float)glControl1.Width) * (float)(conf.vel_ploscice.X + 10) - 5f;
                    float y = ((float)e.Y / (float)glControl1.Height) * (float)(conf.vel_ploscice.Y + 10) - 5f;
                    y = ((float)(conf.vel_ploscice.Y + 10) - 10f) - y;
                    liki[premikam_index1].tocke[premikam_index2] = new Vector2(x, y);
                    glControl1.Invalidate();
                }
                else
                {
                    if (liki.Last().tip == 2 && liki.Last().tocke.Count == 1) //krog in imamo določeno središče
                    {
                        tempKrogBool = true;
                        float x = ((float)e.X / (float)glControl1.Width) * (float)(conf.vel_ploscice.X + 10) - 5f;
                        float y = ((float)e.Y / (float)glControl1.Height) * (float)(conf.vel_ploscice.Y + 10) - 5f;
                        y = ((float)(conf.vel_ploscice.Y + 10) - 10f) - y;
                        tempKrog.tocke[1] = new Vector2(x, y);
                        glControl1.Invalidate();
                    }
                    else if (liki.Last().tip == 3 && liki.Last().tocke.Count == 1) //krog in imamo določeno središče
                    {
                        tempKvadratBool = true;
                        float x = ((float)e.X / (float)glControl1.Width) * (float)(conf.vel_ploscice.X + 10) - 5f;
                        float y = ((float)e.Y / (float)glControl1.Height) * (float)(conf.vel_ploscice.Y + 10) - 5f;
                        y = ((float)(conf.vel_ploscice.Y + 10) - 10f) - y;
                        tempKvadrat.tocke[1] = new Vector2(x, y);
                        glControl1.Invalidate();
                    }
                }
            }
        }

        private void button_kvadrat_Click(object sender, EventArgs e)
        {
            tempKrogBool = false;
            tempKvadratBool = false;
            tempBrisi = false;
            tempPremakni = false;
            tempPremakniPremikam = false;
            liki.Add(new Kvadrat());
        }

        private void button_lok_Click(object sender, EventArgs e)
        {
            tempKrogBool = false;
            tempKvadratBool = false;
            tempBrisi = false;
            tempPremakni = false;
            tempPremakniPremikam = false;
            liki.Add(new Lok());
        }

        private void button_brisi_Click(object sender, EventArgs e)
        {
            tempKrogBool = false;
            tempKvadratBool = false;
            tempPremakni = false;
            tempPremakniPremikam = false;
            tempBrisi = true;
        }

        private void button_premakni_Click(object sender, EventArgs e)
        {
            tempPremakni = true;
            tempPremakniPremikam = false;
            tempKrogBool = false;
            tempKvadratBool = false;
            tempBrisi = false;
        }
    }
}
