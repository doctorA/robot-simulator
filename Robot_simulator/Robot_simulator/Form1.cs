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

using Lightwave;

namespace Robot_simulator
{
    public partial class Form1 : Form
    {
        #region lol1
        double zoom = -25;
        double rtri = 0;
        double rtri2 = 0;
        double trans_X = 0;
        double trans_Y = 0;
        Vector2 mouse_last = new Vector2();
        bool mouseDownLeft = false;
        bool mouseDownRight = false;
        int height;
        int width;
        Robot robot = new Robot(0,60,-35,90,0,0);
        #endregion
        //ej

        #region lol2
        public Form1()
        {
            InitializeComponent();
            glControl1.MouseWheel += new MouseEventHandler(glControl1_MouseWheel);
        }
        #endregion

        #region lol3


        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            setup_viewport();
        }

        private void setup_viewport()
        {
            height = glControl1.Height;
            width = glControl1.Width;
            if (height == 0)
            {
                height = 1;
            }
            GL.Viewport(0, 0, width, height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            OpenTK.Graphics.Glu.Perspective(45.0, (double)width / (double)height, 0.1, 1500.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            float[] light0_ambient = { 0.0f, 0.0f, 0.0f, 1f };
            float[] light0_diffuse = { 0.9f, 0.9f, 0.9f, 1f };
            float[] light0_position = { 100.0f, 100.0f, 0.0f, 1.0f };
            float[] light1_position = { -100.0f, 100.0f, 100.0f, 1.0f };
            float[] light0_specular = { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] light0_shininess = { 2.0f };

            GL.Material(MaterialFace.Front, MaterialParameter.Specular, light0_specular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, light0_shininess);
            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, light0_ambient);

            GL.Light(LightName.Light0, LightParameter.Position, light0_position);
            GL.Light(LightName.Light0, LightParameter.Ambient, light0_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light0_diffuse);

            GL.Light(LightName.Light1, LightParameter.Position, light1_position);
            GL.Light(LightName.Light1, LightParameter.Ambient, light0_ambient);
            GL.Light(LightName.Light1, LightParameter.Diffuse, light0_diffuse);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            GL.Enable(EnableCap.ColorMaterial);
            GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            glControl1.Invalidate();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(Color.Black);

            GL.Enable(EnableCap.Lighting);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0, 0, zoom);
            GL.Translate(-trans_X / 20, trans_Y / 20 - 20, -50);
            GL.Rotate(-rtri, 0.0, 1.0, 0.0);
            GL.Rotate(rtri2, 1.0, 0.0, 0.0);
            GL.Rotate(-90, 1.0, 0.0, 0.0);

            robot.narisi();

            glControl1.SwapBuffers();
        }


        #region mouse eventi
        private void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                if (e.Delta > 0)
                    zoom += 1;
                else if (e.Delta < 0)
                    zoom += (-1);
                glControl1.Invalidate();
            }
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLeft = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                mouseDownRight = true;
            }
            mouse_last = new Vector2(e.X, e.Y);
            glControl1.Invalidate();
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownLeft = false;
            mouseDownRight = false;
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDownLeft)
            {
                rtri += mouse_last.X - e.X;
                rtri2 += mouse_last.Y - e.Y;
                mouse_last = new Vector2(e.X, e.Y);
                glControl1.Invalidate();
            }
            if (mouseDownRight)
            {
                trans_X += mouse_last.X - e.X;
                trans_Y += mouse_last.Y - e.Y;
                mouse_last = new Vector2(e.X, e.Y);
                glControl1.Invalidate();
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }
        #endregion

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            robot.rotacija1 = trackBar1.Value;
            label1.Text = trackBar1.Value.ToString();
            glControl1.Invalidate();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            robot.rotacija2 = trackBar2.Value;
            label2.Text = trackBar2.Value.ToString();
            glControl1.Invalidate();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            robot.rotacija3 = trackBar3.Value;
            label3.Text = trackBar3.Value.ToString();
            glControl1.Invalidate();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            robot.rotacija4 = trackBar4.Value;
            label4.Text = trackBar4.Value.ToString();
            glControl1.Invalidate();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            robot.rotacija5 = trackBar5.Value;
            label5.Text = trackBar5.Value.ToString();
            glControl1.Invalidate();
        }

        private void trackBar6_ValueChanged(object sender, EventArgs e)
        {
            robot.rotacija6 = trackBar6.Value;
            label6.Text = trackBar6.Value.ToString();
            glControl1.Invalidate();
        }


        //zbiši ko neboš rabo več :)
        private void button_tmp_Click(object sender, EventArgs e)
        {
            Rezkar lol = new Rezkar();
            lol.ShowDialog();
            glControl1.MakeCurrent();
            glControl1.Invalidate();
           // glControl1.s
        }


    }
}