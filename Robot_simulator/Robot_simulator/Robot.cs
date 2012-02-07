using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using GLU = OpenTK.Graphics.Glu;

using Lightwave;


namespace Robot_simulator
{
    class Robot
    {

        public int rotacija1 = 0;
        public int rotacija2 = 0;
        public int rotacija3 = 0;
        public int rotacija4 = 0;
        public int rotacija5 = 0;
        public bool modelOK = false;
        public LightwaveObject[] robot_model;  //objekt za modele robota

        public Robot(int rot1, int rot2, int rot3, int rot4, int rot5)
        {
            rotacija1 = rot1;
            rotacija2 = rot2;
            rotacija3 = rot3;
            rotacija4 = rot4;
            rotacija5 = rot5;
            string dir = Environment.CurrentDirectory;
            dir = dir.Remove(dir.IndexOf("bin")) + "ModelsLWO\\";  //pot do modelov
            robot_model = new LightwaveObject[7];
            // od tu pa do konca catch zakomentiraj če ne dela model robota :)
            //try
            //{
            //    robot_model[0] = LightwaveObject.LoadObject(dir + "MH6_base.lwo");  //poizkusi naložit modele
            //    robot_model[1] = LightwaveObject.LoadObject(dir + "MH6_baxis.lwo");
            //    robot_model[2] = LightwaveObject.LoadObject(dir + "MH6_laxis.lwo");
            //    robot_model[3] = LightwaveObject.LoadObject(dir + "MH6_raxis.lwo");
            //    robot_model[4] = LightwaveObject.LoadObject(dir + "MH6_saxis.lwo");
            //    robot_model[5] = LightwaveObject.LoadObject(dir + "MH6_taxis.lwo");
            //    robot_model[6] = LightwaveObject.LoadObject(dir + "MH6_uaxis.lwo");
            //    modelOK = true;
            //}
            //catch (Exception e)
            //{
            //    modelOK = false;  //če so napake to definiraj s to spremenljivko
            //}
        }

        public void narisi()
        {
            #region simple model
            if (!modelOK)  //če so bile napake pri nalaganju modela, potem nariši osnovno obliko robota
            {
                GL.Begin(BeginMode.Quads);

                GL.PushMatrix();
                GL.LoadIdentity();


                //spordaj osnova
                GL.Color3(Color.Blue);
                GL.Translate(0.0f, 0.0f, -6.0f);
                IntPtr quadric = GLU.NewQuadric();
                GL.Rotate(-90, 1.0f, 0.0f, 0.0f);
                GLU.Cylinder(quadric, 2.0, 2.0, 1.0, 32, 32);

                GL.Translate(0.0f, 0.0f, 1.0f);
                quadric = GLU.NewQuadric();
                GLU.PartialDisk(quadric, 0.1, 2.0, 32, 32, 0, 360);

                // 1 del zgoraj
                GL.Rotate(rotacija1 + 90, 0.0f, 0.0f, 1.0f);
                quadric = GLU.NewQuadric();
                GLU.Cylinder(quadric, 0.5, 0.5, 2.0, 32, 32);
                GL.Translate(0.0f, 0.0f, 2.0f);

                GL.Color3(Color.SkyBlue);
                quadric = GLU.NewQuadric();
                GLU.Sphere(quadric, 0.5, 32, 32);

                GL.Color3(Color.Blue);
                GL.Rotate(-90, 1.0f, 0.0f, 0.0f);
                GL.Translate(0.0f, 0.0f, 0.0f);
                quadric = GLU.NewQuadric();
                GLU.Cylinder(quadric, 0.5f, 0.5f, 1.0f, 32, 32);

                GL.Color3(Color.SkyBlue);
                GL.Translate(0.0f, 0.0f, 1.0f);
                quadric = GLU.NewQuadric();
                GLU.Sphere(quadric, 0.5, 32, 32);

                //2 del
                GL.Color3(Color.Blue);
                quadric = GLU.NewQuadric();
                GL.Rotate(-90, 0.0f, 1.0f, 0.0f);
                GL.Rotate(rotacija2, 1.0f, 0.0f, 0.0f);
                GLU.Cylinder(quadric, 0.5f, 0.5f, 7.0f, 32, 32);

                GL.Color3(Color.SkyBlue);
                GL.Translate(0.0f, 0.0f, 7.0f);
                quadric = GLU.NewQuadric();
                GLU.Sphere(quadric, 0.5, 32, 32);

                GL.Color3(Color.Blue);
                GL.Rotate(-90, 0.0f, 1.0f, 0.0f);
                GL.Translate(0.0f, 0.0f, 0.0f);
                quadric = GLU.NewQuadric();
                GLU.Cylinder(quadric, 0.5f, 0.5f, 1.0f, 32, 32);

                //3del
                GL.Color3(Color.SkyBlue);
                GL.Translate(0.0f, 0.0f, 1.0f);
                quadric = GLU.NewQuadric();
                GLU.Sphere(quadric, 0.5, 32, 32);

                GL.Color3(Color.Blue);
                quadric = GLU.NewQuadric();
                GL.Rotate(-90, 0.0f, 1.0f, 0.0f);
                GL.Rotate((rotacija3 * -1), 1.0f, 0.0f, 0.0f);
                GLU.Cylinder(quadric, 0.5f, 0.5f, 2.3f, 32, 32);

                GL.Color3(Color.SkyBlue);
                quadric = GLU.NewQuadric();
                GL.Translate(0.0f, 0.0f, 2.3f);
                GLU.Cylinder(quadric, 0.7f, 0.7f, 1.0f, 32, 32);


                quadric = GLU.NewQuadric();
                GLU.PartialDisk(quadric, 0.1, 0.7f, 32, 32, 0, 360);

                GL.Translate(0.0f, 0.0f, 1.0f);
                quadric = GLU.NewQuadric();
                GLU.PartialDisk(quadric, 0.1, 0.7f, 32, 32, 0, 360);

                //4 del

                GL.Color3(0.0f, 0.0f, 1.0f);
                quadric = GLU.NewQuadric();
                GL.Rotate(rotacija4, 0.0f, 0.0f, 1.0f);
                GLU.Cylinder(quadric, 0.4f, 0.4f, 2.5f, 32, 32);


                GL.Translate(0.0f, 0.3f, 2.7f);
                GL.Color3(1.0f, 0.0f, 0.0f);
                quadric = GLU.NewQuadric();
                GL.Rotate(90, 1.0f, 0.0f, 0.0f);
                GLU.Cylinder(quadric, 0.1f, 0.1f, 0.6f, 32, 32);



                GL.Translate(0.0f, 0.0f, 0.3f);
                GL.Color3(1.0f, 0.0f, 0.0f);
                quadric = GLU.NewQuadric();
                GL.Rotate(-90, 1.0f, 0.0f, 0.0f);
                GL.Rotate(rotacija5, 0.0f, 1.0f, 0.0f);
                GLU.Cylinder(quadric, 0.01f, 0.01f, 1.0f, 32, 32);

                GL.End();
            }
            #endregion
            else  //če pa ni bilo napak pa nariši naložen LWO objekt
            {
                GL.PushMatrix();
                //GL.LoadIdentity();
                for(int m=0;m<7;m++){
                LightwaveObject model = robot_model[m];

                for(int l=0;l<model.Layers.Count;l++)
                {
                    Layer layer=model.Layers[l];
                    for (int i = 0; i < layer.Polygons.Count; i++)
                    {
                        Polygon poly = layer.Polygons[i];
                        GL.Begin(BeginMode.Polygon);
                        //GL.Color3(Color.Blue);
                        GL.Color3(model.Surfaces[0].color.Red, model.Surfaces[0].color.Green, model.Surfaces[0].color.Blue);
                        for (int j = 0; j < poly.Vertices.Count; j++)
                        {
                            Lightwave.Point p = layer.Points[(int)poly.Vertices[j].Index];
                            GL.Normal3(poly.Vertices[j].normal_x, poly.Vertices[j].normal_y, poly.Vertices[j].normal_z);
                            GL.Vertex3(p.position_x, p.position_y, p.position_z);
                        }
                        GL.End();
                    }
                }

                GL.PopMatrix();
                }
            }
        }


    }
}
