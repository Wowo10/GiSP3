﻿using SFML.Graphics;
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

        int length;
        public int Lenght
        {
            get { return length; }
        }

        RectangleShape shape;

        VertexArray line;

        public void Look()
        {
            SFML.Graphics.Vertex[] temptab = new SFML.Graphics.Vertex[2];

            for (uint i = 0; i < line.VertexCount; i++)
            {
                temptab[i] = line[i];
            }

            for (int i = 0; i < temptab.Length; i++)
            {
                temptab[i].Color = new Color(255, 127,0);
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

        public Edge(Pair pair, Vector2f pos, double width, double rotationdegrees)
        {
            this.pair = pair;

            shape = new RectangleShape(new Vector2f(5, (float)width));

            shape.FillColor = new Color(255, 255, 255);

            shape.Position = pos;

            shape.Origin = new Vector2f(shape.GetGlobalBounds().Width / 2, shape.GetGlobalBounds().Height / 2);

            shape.Rotation = (float)rotationdegrees;

            //////////////////////////////////////////////////////////////

            /*
            temp = new VertexArray();
            temp.Append(new SFML.Graphics.Vertex(new Vector2f(0, 0), new Color(255, 255, 0)));
            temp.Append(new SFML.Graphics.Vertex(new Vector2f(400, 400), new Color(255, 255, 0)));

            temp.PrimitiveType = PrimitiveType.Lines;
            */
        }

        public Edge(Pair pair, Vector2f startpos, Vector2f stoppos, int length = 1) //currently in use
        {
            this.pair = pair;

            line = new VertexArray();

            line.Append(new SFML.Graphics.Vertex(startpos, new Color(255, 255, 255)));
            line.Append(new SFML.Graphics.Vertex(stoppos, new Color(255, 255, 255)));
            /*line.Append(new SFML.Graphics.Vertex(startpos + new Vector2f(5, 5), new Color(255, 255, 255)));
            line.Append(new SFML.Graphics.Vertex(stoppos + new Vector2f(5, 5), new Color(255, 255, 255)));*/

            line.PrimitiveType = PrimitiveType.Lines;

            this.length = length;
        }

        public void Render(ref RenderWindow window)
        {
            //window.Draw(shape);

            window.Draw(line);
        }

    }
}
