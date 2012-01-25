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
        public Rezkar()
        {
            InitializeComponent();
        }

        private void SetupViewport()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-5, 45, -5, 55, -1, 1);
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
            GL.Vertex2(40, 0);
            GL.Vertex2(40,50);
            GL.Vertex2(0, 50);

            glControl1.SwapBuffers();
        }

        private void button_crta_Click(object sender, EventArgs e)
        {
        }
    }
}
