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
using Antlr.Runtime;
using Antlr.Runtime.Misc;
using Antlr.Runtime.Tree;

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
            GL.Translate(-trans_X / 20, trans_Y / 20-20, -50);
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
            else if (e.Button == MouseButtons.Middle)
            {
                zoom = -25;
                rtri = 0;
                rtri2 = 0;
                trans_X = 0;
                trans_Y = 0;
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
            string input = richTextBox1.Text;

            if (input.Length > 0)
            {
                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Analizing input..." + Environment.NewLine;
                if (checkGrammar(input) == 0)
                {
                    richTextBox2.Text += GetTimestamp(DateTime.Now) + "Valid input..." + Environment.NewLine;
                    richTextBox2.Text += GetTimestamp(DateTime.Now) + "Starting robot..." + Environment.NewLine;
                }
                else
                {
                    richTextBox2.Text += GetTimestamp(DateTime.Now) + "Invalid input..." + Environment.NewLine;
                }
                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Waiting for input..." + Environment.NewLine;
            }
            else
            {
                richTextBox2.Text += GetTimestamp(DateTime.Now) + "No input..." + Environment.NewLine;
            }
        }

        private int checkGrammar(string vhod)
        {
            //try
            //{
                ANTLRStringStream sStream = new ANTLRStringStream(vhod);
                RobotLanguageLexer lexer = new RobotLanguageLexer(sStream);

                CommonTokenStream tStream = new CommonTokenStream(lexer);

                RobotLanguageParser parser = new RobotLanguageParser(tStream);

                AstParserRuleReturnScope<CommonTree, IToken> odgovor = parser.start();
                CommonTree drevo = (CommonTree)odgovor.Tree;

                string izpis = String.Empty;
                Dictionary<string, float[]> CINDEKS = new Dictionary<string, float[]>();

                for (int i = 0; i < drevo.ChildCount; i++)
                {
                    ITree token = drevo.GetChild(i);
                    switch (token.Type)
                    {
                        case -1: break; //EOF
                        //case 0: richTextBox2.Text += GetTimestamp(DateTime.Now) + "Napaka!! Vrstica: " + token.Text + Environment.NewLine; return -1;
                        case 4: break; //ATTR
                        case 5: break; //COMM
                        case 6: break; //DATE
                        case 7: break; //DIN
                        case 8: break; //DOUT
                        case 9: break; //FRAME
                        case 10: break; //GROUP
                        case 11:
                            try
                            {
                                string key = token.Text; i++;
                                float[] value = new float[6];
                                for (int j = 0; j < 6; j++, i++)
                                {
                                    if (drevo.GetChild(i).Type == 15) { value[j] = -1f * (float)Convert.ToDouble(drevo.GetChild(i + 1).Text.Replace(".", ",")); i++; } //negativni predznak
                                    else { value[j] = (float)Convert.ToDouble(drevo.GetChild(i).Text.Replace(".", ",")); }
                                }
                                CINDEKS.Add(key, value); i--;

                                izpis += "\t" + token.Text + " (" + value[0].ToString() + " " + value[1].ToString() + " " + value[2].ToString() + " " + value[3].ToString() + " " + value[4].ToString() + " " + value[5].ToString() + ")" + Environment.NewLine;
                            }
                            catch
                            {
                                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Napaka!! Token" + "(" + i.ToString() + ")" + ": " + token.Text + Environment.NewLine; return -1;
                            }
                            break; //INDEKS
                        case 12: break; //INST
                        case 13: break; //INT
                        case 14: break; //JOB
                        case 15: break; //MINUS
                        case 16:
                            try
                            {
                                string usedKey = drevo.GetChild(i + 1).Text;
                                float delay = (float)Convert.ToDouble(drevo.GetChild(i + 3).Text.Replace(".", ",")); 
                                //MOVC(CINDEKS[usedKey], delay);
                                i += 3;
                                izpis += "\t" + token.Text + " (" + usedKey + ", " + delay.ToString() + ")" + Environment.NewLine;
                            }
                            catch
                            {
                                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Napaka!! Token" + "(" + i.ToString() + ")" + ": " + token.Text + Environment.NewLine; return -1;
                            }
                            break; //MOVC
                        case 17:
                            try
                            {
                                string usedKey = drevo.GetChild(i + 1).Text;
                                float delay = (float)Convert.ToDouble(drevo.GetChild(i + 3).Text.Replace(".", ",")); 
                                //MOVJ(CINDEKS[usedKey], delay);
                                i += 3;
                                izpis += "\t" + token.Text + " (" + usedKey + ", " + delay.ToString() + ")" + Environment.NewLine;
                            }
                            catch
                            {
                                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Napaka!! Token" + "(" + i.ToString() + ")" + ": " + token.Text + Environment.NewLine; return -1;
                            } 
                            break; //MOVJ
                        case 18:
                            try
                            {
                                string usedKey = drevo.GetChild(i + 1).Text;
                                float delay = (float)Convert.ToDouble(drevo.GetChild(i + 3).Text.Replace(".", ",")); 
                                //MOVL(CINDEKS[usedKey], delay);
                                i += 3;
                                izpis += "\t" + token.Text + " (" + usedKey + ", " + delay.ToString() + ")" + Environment.NewLine;
                            }
                            catch
                            {
                                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Napaka!! Token" + "(" + i.ToString() + ")" + ": " + token.Text + Environment.NewLine; return -1;
                            }
                            break; //MOVL
                        case 19:
                            try
                            {
                                string usedKey = drevo.GetChild(i + 1).Text;
                                float delay = (float)Convert.ToDouble(drevo.GetChild(i + 3).Text.Replace(".", ","));
                                //MOVS(CINDEKS[usedKey], delay);
                                i += 3;
                                izpis += "\t" + token.Text + " (" + usedKey + ", " + delay.ToString() + ")" + Environment.NewLine;
                            }
                            catch
                            {
                                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Napaka!! Token" + "(" + i.ToString() + ")" + ": " + token.Text + Environment.NewLine; return -1;
                            }
                            break; //MOVS
                        case 20: break; //NAME
                        case 21: break; //NEWLINE
                        case 22: break; //NIZ
                        case 23: break; //NPOS
                        case 24: break; //ONOFF
                        case 25: break; //POS
                        case 26: break; //POSTYPE
                        case 27: break; //RCONF
                        case 28: break; //REAL
                        case 29: break; //RECTAN
                        case 30: break; //TIME
                        case 31:
                            try
                            {
                                float timer = (float)Convert.ToDouble(drevo.GetChild(i + 1).Text.Replace(".", ","));
                                //izpis += "\t" + token.Text + "(" + timer.ToString() + ")" + Environment.NewLine;
                            }
                            catch
                            {
                                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Napaka!! Token" + "(" + i.ToString() + ")" + ": " + token.Text + Environment.NewLine; return -1;
                            }
                            break; //TIMER
                        case 32: break; //TOOL
                        case 33: break; //USER
                        case 34: break; //WS
                    }
                }

                richTextBox2.Text += GetTimestamp(DateTime.Now) + Environment.NewLine + izpis;
                return 0;
            /*}
            catch
            {
                richTextBox2.Text += GetTimestamp(DateTime.Now) + "Something went wrong..." + Environment.NewLine;
                return -1;
            }*/
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
 
    int lol = 6;

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

             robot.rotacija4 += 10;
             if (robot.rotacija4 > 180)
                 robot.rotacija4 = -180;

             if (robot.rotacija2 + lol < 100)
             {
                 robot.rotacija2 += lol;
             }
             else
             {
                 lol = -lol;
             }
             if (robot.rotacija2< -50)
             {
                 lol = -lol;
             }
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

        private void button4_Click(object sender, EventArgs e)
        {
            Vector3 vrh = robot.getVrhOrodja();
            MessageBox.Show("Vrh: " + vrh.ToString());
        }

        private void MOVC(float[] p, float delay)
        {
            richTextBox2.Text += "\t MOVC" + "(" + p[0].ToString() + " " + p[1].ToString() + " " + p[2].ToString() + " " + p[3].ToString() + " " + p[4].ToString() + " " + p[5].ToString() + ") & T(" + delay.ToString() + ")" + Environment.NewLine;
        }

        private void MOVJ(float[] p, float delay)
        {
            richTextBox2.Text += "\t MOVJ" + "(" + p[0].ToString() + " " + p[1].ToString() + " " + p[2].ToString() + " " + p[3].ToString() + " " + p[4].ToString() + " " + p[5].ToString() + ") & T(" + delay.ToString() + ")" + Environment.NewLine;
        }

        private void MOVL(float[] p, float delay)
        {
            richTextBox2.Text += "\t MOVL" + "(" + p[0].ToString() + " " + p[1].ToString() + " " + p[2].ToString() + " " + p[3].ToString() + " " + p[4].ToString() + " " + p[5].ToString() + ") & T(" + delay.ToString() + ")" + Environment.NewLine;
        }

        private void MOVS(float[] p, float delay)
        {
            richTextBox2.Text += "\t MOVS" + "(" + p[0].ToString() + " " + p[1].ToString() + " " + p[2].ToString() + " " + p[3].ToString() + " " + p[4].ToString() + " " + p[5].ToString() + ") & T(" + delay.ToString() + ")" + Environment.NewLine;
        }

        // ono za inverzno:D
        private void button5_Click(object sender, EventArgs e)
        {
            string[] vals= {"150", "150", "150"};
            Vector3 v=new Vector3(float.Parse(vals[0]), float.Parse(vals[1]), float.Parse(vals[2]));
            Vector3 curr = robot.getVrhOrodja();
            float distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            
            while (distance > 10)
            {
                robot.idi_tja(v);
                curr = robot.getVrhOrodja();
                distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
                glControl1.Invalidate();
            }
            
        }
        //konec inverzne
    }
}
