﻿using System;
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
        public float rotacija1 = 0;
        public float rotacija2 = 0;
        public float rotacija3 = 0;
        public float rotacija4 = 0;
        public float rotacija5 = 0;
        public float rotacija6 = 0;
        public bool modelOK = false;
        public bool set_tool = true;
        public LightwaveObject[] robot_model;  //objekt za modele robota
        public LightwaveObject[] okolica; //objekti za okolico robota

        public Robot(int rot1, int rot2, int rot3, int rot4, int rot5, int rot6)
        {
            rotacija1 = rot1;
            rotacija2 = rot2;
            rotacija3 = rot3;
            rotacija4 = rot4;
            rotacija5 = rot5;
            rotacija6 = rot6;
            string dir = Environment.CurrentDirectory;
            dir = dir.Remove(dir.IndexOf("bin")) + "ModelsLWO\\";  //pot do modelov
            robot_model = new LightwaveObject[10];
            okolica = new LightwaveObject[10];
            // od tu pa do konca catch zakomentiraj če ne dela model robota :)
            try
            {
                robot_model[0] = LightwaveObject.LoadObject(dir + "MH6_base.lwo");  //poizkusi naložit modele
                robot_model[1] = LightwaveObject.LoadObject(dir + "MH6_saxis.lwo");
                robot_model[2] = LightwaveObject.LoadObject(dir + "MH6_laxis.lwo");
                robot_model[3] = LightwaveObject.LoadObject(dir + "MH6_uaxis.lwo");
                robot_model[4] = LightwaveObject.LoadObject(dir + "MH6_raxis.lwo");
                robot_model[5] = LightwaveObject.LoadObject(dir + "MH6_baxis.lwo");
                robot_model[6] = LightwaveObject.LoadObject(dir + "MH6_taxis.lwo");
                robot_model[7] = LightwaveObject.LoadObject(dir + "TOOL3.lwo");
                // okolica
                okolica[0] = LightwaveObject.LoadObject(dir + "Okolica\\Barrel_6.lwo");
                okolica[1] = LightwaveObject.LoadObject(dir + "Okolica\\Aframe.lwo");
                okolica[2] = LightwaveObject.LoadObject(dir + "Okolica\\floor.lwo");
                modelOK = true;
            }
            catch
            {
                modelOK = false;  //če so napake to definiraj s to spremenljivko
            }
        }

        public Vector3 getVrhOrodja()
        {
            Vector3 vrh;
            //dolžine posameznih objektov
            //podstavek: 0, -3.2, 0;
            //prvi zglob: 3.75, 8.3, -1.6; rot: z : rot1+90
            //prva roka: 0, 15.35, -0.4; rot: z
            //drugi zglob: 5.2, 3.9, 2.0; rot: x, pa -90 po x
            //druga roka: 10.8, 0, 0; rot: z
            //zadnji zglob: 2.2, 0, 0; rot: x
            //oni krogec za orodja montirat: 0.4, 0, 0
            // orodje ima svoje dolžine, rotacija je po x

            vrh = new Vector3(0.4f, 0.0f, 0.0f);
            vrh = rot_po_x(vrh, rotacija6);
            vrh += new Vector3(2.2f, 0f, 0f);
            vrh = rot_po_z(vrh, rotacija5);
            vrh += new Vector3(10.8f, 0f, 0f);
            vrh = rot_po_x(vrh, rotacija4);
            vrh += new Vector3(5.2f, 3.9f, 2.0f);
            vrh = rot_po_x(vrh, -90);
            vrh = rot_po_z(vrh, rotacija3);
            vrh += new Vector3(0f, 15.35f, -0.4f); 
            vrh = rot_po_z(vrh, rotacija2);
            vrh += new Vector3(3.75f, 8.3f, -1.6f);
            vrh = rot_po_y(vrh, rotacija1 + 90);

             return Vector3.Multiply(vrh, 10f);
        }

        public Vector3 rot_po_x(Vector3 state, float angle)
        {
            angle = angle / 180 * (float)Math.PI;
            Vector3 v = new Vector3();
            v.Y = state.Y * (float)Math.Cos(angle) - state.Z * (float)Math.Sin(angle);
            v.Z = state.Y * (float)Math.Sin(angle) + state.Z * (float)Math.Cos(angle);
            v.X = state.X;
            return v;
        }

        public Vector3 rot_po_y(Vector3 state, float angle)
        {
            angle = angle / 180 * (float)Math.PI;
            Vector3 v = new Vector3();
            v.Z = state.Z * (float)Math.Cos(angle) - state.X * (float)Math.Sin(angle);
            v.X = state.Z * (float)Math.Sin(angle) + state.X * (float)Math.Cos(angle);
            v.Y=state.Y;
            return v;
        }

        public Vector3 rot_po_z(Vector3 state, float angle)
        {
            angle=angle/180*(float)Math.PI;
            Vector3 v=new Vector3();
            v.X = state.X * (float)Math.Cos(angle) - state.Y * (float)Math.Sin(angle);
            v.Y = state.X * (float)Math.Sin(angle) + state.Y * (float)Math.Cos(angle);
            v.Z = state.Z;
            return v;
        }

        public void idi_tja(Vector3 v)
        {
            Vector3 curr = getVrhOrodja();
            float distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            float new_distance;


            rotacija6 += 0.1f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija6 -= 0.2f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija6 += 0.1f;

            rotacija5 += 0.1f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija5 -= 0.2f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija5 += 0.1f;
            distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));

            rotacija4 += 0.1f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija4 -= 0.2f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija4 += 0.1f;
            distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));

            rotacija3 += 0.1f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija3 -= 0.2f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija3 += 0.1f;
            distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));

            rotacija2 += 0.1f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija2 -= 0.2f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija2 += 0.1f;
            distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));


            rotacija1+=0.1f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija1 -= 0.2f;
            curr = getVrhOrodja();
            new_distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));
            if (new_distance > distance)
                rotacija1 += 0.1f;
            distance = (float)Math.Sqrt(Math.Pow(curr.X - v.X, 2) + Math.Pow(curr.Y - v.Y, 2) + Math.Pow(curr.Z - v.Z, 2));

  
                

                
                

                


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

                GL.Rotate(90, 1.0f, 0.0f, 0.0f);

                /* okolica robota  */
                GL.PushMatrix();
                GL.Translate(0.0f, -3.2f, 0.0f);
                risi_model(okolica[2]);
                GL.Translate(-5.0f, 0.0f, -50.0f);
                risi_model(okolica[0]);
                GL.Translate(7.0f, 0.0f, 0.0f);
                risi_model(okolica[0]);
                GL.Translate(-3.5f, 10.0f, 0.0f);
                risi_model(okolica[0]);
                GL.PopMatrix();
                GL.PushMatrix();
                GL.Translate(0.0f, -3.0f, 0.0f);
                risi_model(okolica[1]);
                GL.PopMatrix();

                // robot
                GL.PushMatrix();
                
                //dolžine posameznih objektov
                //podstavek: 0, -3.2, 0;
                //prvi zglob: 3.75, 8.3, -1.6; rot: z : rot1+90
                //prva roka: 0, 15.35, -0.4; rot: z
                //drugi zglob: 5.2, 3.9, 2.0; rot: x, pa -90 po x
                //druga roka: 10.8, 0, 0; rot: z
                //zadnji zglob: 2.2, 0, 0; rot: x
                //oni krogec za orodja montirat: 0.4, 0, 0


                risi_model(robot_model[0]);  //podstavek

                GL.Rotate(rotacija1 + 90, 0.0f, 1.0f, 0.0f);
                risi_model(robot_model[1]); // prvi motor
               

                GL.Translate(3.75f, 8.30f, -1.60f); // prva roka
                GL.Rotate(rotacija2, 0.0f, 0.0f, 1.0f);
                risi_model(robot_model[2]);

                GL.Translate(0.0f, 15.35f, -0.4f);
                GL.Rotate(rotacija3, 0.0f, 0.0f, 1.0f);
                risi_model(robot_model[3]);

                GL.Translate(5.2f, 3.9f, 2.0f);
                GL.Rotate(rotacija4, 1.0f, 0.0f, 0.0f);
                GL.Rotate(-90.0f, 1.0f, 0.0f, 0.0f);
                risi_model(robot_model[4]);

                GL.Translate(10.8f, 0.0f, 0.0f);
                GL.Rotate(rotacija5, 0.0f, 0.0f, 1.0f);
                risi_model(robot_model[5]);

                GL.Translate(2.2f, 0.0f, 0.0f);
                GL.Rotate(rotacija6, 1.0f, 0.0f, 0.0f);
                risi_model(robot_model[6]);

                if (set_tool)
                {
                    GL.Translate(0.4f, 0.0f, 0.0f);
                    risi_model(robot_model[7]);
                }

                GL.PopMatrix();


            }
        }


        public void risi_model(LightwaveObject model)
        {
            for (int l = 0; l < model.Layers.Count; l++)
            {
                Layer layer = model.Layers[l];
                Surface s = new Surface();
                for (int i = 0; i < layer.Polygons.Count; i++)
                {
                    Polygon poly = layer.Polygons[i];
                    GL.Begin(BeginMode.Polygon);
                    s = poly.SurfaceReference;
                    GL.Color3(s.color.Red, s.color.Green, s.color.Blue);
                    for (int j = 0; j < poly.Vertices.Count; j++)
                    {
                        Lightwave.Point p = layer.Points[(int)poly.Vertices[j].Index];
                        GL.Normal3(poly.Vertices[j].normal_x, poly.Vertices[j].normal_y, poly.Vertices[j].normal_z);
                        GL.Vertex3(p.position_x, p.position_y, p.position_z);
                    }
                    GL.End();
                }
            }
        }


    }
}
