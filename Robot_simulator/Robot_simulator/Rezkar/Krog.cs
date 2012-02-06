using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Robot_simulator
{
    class Krog : Lik
    {
        public Krog()
        {
            tip = 2;
        }

        void glCircle3i(float x, float y, float radius)
        {
            float angle;
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Color3(Color.White);
            GL.LineWidth(5f);
            GL.Begin(BeginMode.LineLoop);
            for (int i = 0; i < 100; i++)
            {
                angle = i * 2f * (float)Math.PI / 100f;
                GL.Vertex2(x + ((float)Math.Cos(angle) * radius), y + ((float)Math.Sin(angle) * radius));
            }
            GL.End();
            GL.PopMatrix();
        }  



        public override void risi(Conf_rezkar conf)
        {
            if (this.tocke.Count > 1)
            {
                GL.Enable(EnableCap.LineSmooth);
                GL.ShadeModel(ShadingModel.Smooth);
                GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                glCircle3i(tocke[0].X, tocke[0].Y, (new Vector2(tocke[0].X - tocke[1].X, tocke[0].Y - tocke[1].Y)).Length);

               /* GL.LineWidth(5f);
                GL.Begin(BeginMode.Lines);
                //GL.LineWidth(200f);
                GL.Color3(Color.White);
                for (int i = 0; i < tocke.Count - 1; i++)
                {
                    GL.Vertex2(tocke[i]);
                    GL.Vertex2(tocke[i + 1]);
                }
                GL.End();

                GL.PointSize(10f);
                GL.Color3(Color.Red);
                GL.Begin(BeginMode.Points);
                for (int i = 0; i < tocke.Count; i++)
                {
                    GL.Vertex2(tocke[i]);
                }
                GL.End();*/
            }
        }

        public override void toJBI(Conf_rezkar conf)
        {
            throw new NotImplementedException();
        }
    }
}
