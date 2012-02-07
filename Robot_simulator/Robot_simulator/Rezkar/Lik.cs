using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot_simulator
{
    using OpenTK;
    using OpenTK.Graphics.OpenGL;

    public abstract class Lik
    {
        public List<Vector2> tocke = new List<Vector2>();
        public List<string> ukazi = new List<string>();
        public int tip;

        public void zbrisiTocko(Vector2 t)
        {

        }

        public float razdalja(Vector2 p1)
        {
            float r=float.MaxValue;
            float tmp;
            foreach(Vector2 v in tocke)
            {
                tmp=(new Vector2(v.X - p1.X, v.Y - p1.Y)).Length;
                if (r > tmp)
                {
                    r = tmp;
                }
            }
            return r;
        }

        public float razdalja_ind(Vector2 p1, ref int index)
        {
            index = 0;
            float r = float.MaxValue;
            float tmp;
            for (int i = 0; i < tocke.Count; i++)
            {
                tmp = (new Vector2(tocke[i].X - p1.X, tocke[i].Y - p1.Y)).Length;
                if (r > tmp)
                {
                    r = tmp;
                    index = i;
                }
            }
            return r;
        }

        public abstract void risi(Conf_rezkar conf);
        public abstract void toJBI(Conf_rezkar conf);
    }
}
