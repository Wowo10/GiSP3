using SFML.Graphics;
using SFML.System;

namespace GiSP3
{
    class Edge
    {
        public class Pair
        {
            public char start, stop; //In this project we do not care which is start or stop
            public Pair(char start, char stop)
            {                
                this.start = start;
                this.stop = stop;
            }
            public bool Contains(char test)
            {
                return start == test || stop == test;
            }
        }

        Pair pair;
        public Pair GetPair
        {
            get { return pair; }
        }

        RectangleShape shape;

        public Edge(Pair pair, Vector2f pos)
        {
            this.pair = pair;

            shape = new RectangleShape();

            //hard math here! to do
        }

        public void Render(ref RenderWindow window)
        {
            window.Draw(shape);
        }

    }
}
