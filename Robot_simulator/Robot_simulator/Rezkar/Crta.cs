using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Robot_simulator
{
    public class Crta : Lik
    {
        public Crta()
        {
            tip = 1;
        }

        public override void risi(Conf_rezkar conf)
        {
            if (this.tocke.Count > 1)
            {
                GL.Begin(BeginMode.Lines);
                GL.Color3(Color.White);

                for (int i = 0; i < tocke.Count-1; i++)
                {
                    GL.Vertex2(tocke[i]);
                    GL.Vertex2(tocke[i + 1]);
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
