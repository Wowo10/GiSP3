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
            public char GetSecond(char test)
            {
                if (test == start)
                    return stop;
                else if (test == stop)
                    return start;
                else //Theres none
                    return '0';
            }
            public override string ToString()
            {
                return "(" + start + ", " + stop + ")";
            }
        }

        Pair pair;
        public Pair GetPair
        {
            get { return pair; }
        }

        public bool Contains(char test)
        {
            return pair.Contains(test);
        }

        public char GetSecond(char test)
        {
            return pair.GetSecond(test);
        }

        uint length;
        public uint Lenght
        {
            get { return length; }
        }

        RectangleShape shape;

        VertexArray line;
        Text lengthchar;

        public void Look()
        {
            SFML.Graphics.Vertex[] temptab = new SFML.Graphics.Vertex[2];

            for (uint i = 0; i < line.VertexCount; i++)
            {
                temptab[i] = line[i];
            }

            for (int i = 0; i < temptab.Length; i++)
            {
                temptab[i].Color = new Color(255, 127, 0);
            }

            line.Clear();

            foreach (var vertex in temptab)
            {
                line.Append(vertex);
            }
        }

        public void UnLook()
        {
            SFML.Graphics.Vertex[] temptab = new SFML.Graphics.Vertex[2];

            for (uint i = 0; i < line.VertexCount; i++)
            {
                temptab[i] = line[i];
            }

            for (int i = 0; i < temptab.Length; i++)
            {
                temptab[i].Color = new Color(255, 255, 255);
            }

            line.Clear();

            foreach (var vertex in temptab)
            {
                line.Append(vertex);
            }
        }

        public Edge(Pair pair, Vector2f startpos, Vector2f stoppos, uint length = 1) //currently in use
        {
            this.pair = pair;

            line = new VertexArray();

            line.Append(new SFML.Graphics.Vertex(startpos, new Color(255, 255, 255)));
            line.Append(new SFML.Graphics.Vertex(stoppos, new Color(255, 255, 255)));
            /*line.Append(new SFML.Graphics.Vertex(startpos + new Vector2f(5, 5), new Color(255, 255, 255)));
            line.Append(new SFML.Graphics.Vertex(stoppos + new Vector2f(5, 5), new Color(255, 255, 255)));*/

            line.PrimitiveType = PrimitiveType.Lines;

            this.length = length;

            Font font = new Font("resources/fonts/Font1.ttf");

            lengthchar = new Text();

            lengthchar.Font = font;
            lengthchar.CharacterSize = 25;
            lengthchar.Color = new Color(0, 0, 0);
            lengthchar.DisplayedString = length + "";
            lengthchar.Position = new Vector2f((startpos.X + stoppos.X) / 2, (startpos.Y + stoppos.Y) / 2);

            lengthchar.Origin = new Vector2f(lengthchar.GetGlobalBounds().Width / 2, lengthchar.GetGlobalBounds().Height / 2);
        }

        public void Render(ref RenderWindow window)
        {
            //window.Draw(shape);

            window.Draw(line);
            window.Draw(lengthchar);
        }

    }
}
