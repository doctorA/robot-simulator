using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Robot_simulator
{
    class Kvadrat : Lik
    {
        public Kvadrat()
        {
            tip = 3;
        }

        public override void risi(Conf_rezkar conf)
        {
            if (this.tocke.Count > 1)
            {
                GL.LineWidth(5f);
                GL.Begin(BeginMode.Lines);
                //GL.LineWidth(200f);
                GL.Color3(Color.White);
                    GL.Vertex2(tocke[0].X, tocke[0].Y); //x1y1
                    GL.Vertex2(tocke[1].X, tocke[0].Y); //x2x1

                    GL.Vertex2(tocke[1].X, tocke[0].Y); //x2y1
                    GL.Vertex2(tocke[1].X, tocke[1].Y); //x2y2

                    GL.Vertex2(tocke[1].X, tocke[1].Y); //x2y2
                    GL.Vertex2(tocke[0].X, tocke[1].Y); //x1y2

                    GL.Vertex2(tocke[0].X, tocke[1].Y); //x1y2
                    GL.Vertex2(tocke[0].X, tocke[0].Y); //x1y1

                GL.End();

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

        public override void toJBI(Conf_rezkar conf)
        {
            throw new NotImplementedException();
        }
    }
}
