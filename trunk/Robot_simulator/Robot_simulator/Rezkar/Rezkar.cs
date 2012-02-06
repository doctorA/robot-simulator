﻿using System;
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
            glControl1.SwapBuffers();
        }

        private void button_crta_Click(object sender, EventArgs e)
        {
            liki.Add(new Crta());
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {

            float x = ((float)e.X / (float)glControl1.Width) * (float)(conf.vel_ploscice.X + 10) - 5f;
            float y = ((float)e.Y / (float)glControl1.Height) * (float)(conf.vel_ploscice.Y + 10) - 5f;
            y = ((float)(conf.vel_ploscice.Y + 10) - 10f) - y;
            if (liki.Count>0)
            {
                if (liki.Last().tip == 2 && liki.Last().tocke.Count==2) //krog
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
                if (liki.Last().tip == 2 && liki.Last().tocke.Count == 0)
                {
                    tempKrog.tocke[0] = new Vector2(x, y);
                }
                else if (liki.Last().tip == 3 && liki.Last().tocke.Count == 0)
                {
                    tempKvadrat.tocke[0] = new Vector2(x, y);
                }
                liki.Last().tocke.Add(new Vector2(x, y));
                glControl1.Invalidate();
            }
        }

        private void button_krog_Click(object sender, EventArgs e)
        {
            tempKrogBool = false;
            tempKvadratBool = false;
            liki.Add(new Krog());
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (liki.Count > 0)
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

        private void button_kvadrat_Click(object sender, EventArgs e)
        {
            tempKrogBool = false;
            tempKvadratBool = false;
            liki.Add(new Kvadrat());
        }
    }
}