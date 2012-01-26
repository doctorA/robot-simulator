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

        public abstract void risi(Conf_rezkar conf);
        public abstract void toJBI(Conf_rezkar conf);
    }
}
