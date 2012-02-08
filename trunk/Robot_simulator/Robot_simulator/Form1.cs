using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using Lightwave;

using System.IO;

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
        string robo_app_TU_NOT_MATA_VIDVA_ZA_SPARSAT_SMAJLI = "";
        Color ozadje = Color.LightBlue;
        int cout_utrip = 0;
        System.Timers.Timer aTimer;

        #endregion
        //ej

        #region FORMA
        public Form1()
        {
            InitializeComponent();
            glControl1.MouseWheel += new MouseEventHandler(glControl1_MouseWheel);

            richTextBox2.Text += GetTimestamp(DateTime.Now) + "Waiting for input..." + Environment.NewLine;
        }
        #endregion

        #region OPENGL


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
            float[] light0_diffuse = { 0.1f, 0.1f, 0.1f, 1f };
            float[] light0_position = { 100.0f, 100.0f, 0.0f, 1.0f };
            float[] light1_position = { -100.0f, 100.0f, 100.0f, 1.0f };
            float[] light0_specular = { 0.8f, 0.8f, 0.8f, 1.0f };
            float[] light0_shininess = { 8.0f };

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
            GL.ClearColor(ozadje);

            GL.Enable(EnableCap.Lighting);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0, 0, zoom*2);
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
                    zoom += 2;
                else if (e.Delta < 0)
                    zoom += (-2);
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
            robo_app_TU_NOT_MATA_VIDVA_ZA_SPARSAT_SMAJLI = lol.GLIHIC_vs_DUGIC;
            if (robo_app_TU_NOT_MATA_VIDVA_ZA_SPARSAT_SMAJLI.Length > 0)
            {
                richTextBox1.Text = robo_app_TU_NOT_MATA_VIDVA_ZA_SPARSAT_SMAJLI;
                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Loaded new input..." + Environment.NewLine;
            }
            glControl1.MakeCurrent();
            glControl1.Invalidate();
           // glControl1.s
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //groupBox1
            groupBox1.Left = this.Width - groupBox1.Width - 20;
            glControl1.Width = this.Width - groupBox1.Width - 40;
            glControl1.Height = this.Height - 65;
            this.setup_viewport();
            glControl1.Invalidate();
        }

        #region Ovrednotenje ukazov in sintaktična analiza

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JBI Files(*.JBI)|*.JBI";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(openFile.FileName);
                richTextBox1.Text = streamReader.ReadToEnd();
                streamReader.Close();

                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Loaded new input..." + Environment.NewLine;
            }
        }

        private static String GetTimestamp(DateTime value)
        {
            return value.ToString("(HH:mm:ss): ");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            richTextBox2.Text += GetTimestamp(DateTime.Now) + "Analizing input..." + Environment.NewLine;

            richTextBox2.Text += GetTimestamp(DateTime.Now) + "Valid input..." + Environment.NewLine;
            richTextBox2.Text += GetTimestamp(DateTime.Now) + "Starting robot..." + Environment.NewLine;
            richTextBox2.Text += GetTimestamp(DateTime.Now) + "Waiting for input..." + Environment.NewLine;
        }
        #endregion



       public void bla()
       {
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 200;
            aTimer.Enabled=true;
            cout_utrip = 0;

     }
 

     private void OnTimedEvent(object source, ElapsedEventArgs e)
     {
         if (cout_utrip < 2000)
         {

             if (Color.LightBlue == ozadje)
             {
                 ozadje = Color.Red;
             }
             else
             {
                 ozadje = Color.LightBlue;
             }
             cout_utrip++;
             robot.rotacija1 += 2;
             if (robot.rotacija1 > 180)
                 robot.rotacija1 = -180;
             glControl1.Invalidate();
         }
         else
         {
             aTimer.Stop();
         }
         
     }



        private void button3_Click(object sender, EventArgs e)
        {
            bla();
        }

    }
}
