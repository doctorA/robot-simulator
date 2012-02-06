using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using GLU = OpenTK.Graphics.Glu;


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

        public Robot(int rot1, int rot2, int rot3, int rot4, int rot5)
        {
            rotacija1 = rot1;
            rotacija2 = rot2;
            rotacija3 = rot3;
            rotacija4 = rot4;
            rotacija5 = rot5;
            LightwaveObject[] robot_model;
            string dir = Environment.CurrentDirectory;
            dir = dir.Remove(dir.IndexOf("bin")) + "ModelsLWO\\";
            robot_model = new LightwaveObject[6];
            robot_model[0] = LightwaveObject.LoadObject(dir + "MH6_base.lwo");
        }

        public void narisi()
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
    }
}
