﻿using SFML.Graphics;
using SFML.System;

namespace GiSP3
{
    public class Vertex
    {
        char label;
        public char Label
        {
            get { return label; }
        }

        char previous;
        public char Prev
        {
            get { return previous; }
            set { previous = value; }
        }

        uint distance;
        public uint Dist
        {
            get { return distance; }
            set { distance = value; }
        }

        public static bool debug;

        public void FireOn()
        {
            shape.OutlineColor = new Color(255, 0, 0);
            labelchar.Color = new Color(255, 0, 0);
            labelchar.DisplayedString = firewatchlabel + "";
        }
        public void FireOff()
        {
            shape.OutlineColor = new Color(255, 255, 255);
            labelchar.Color = new Color(255, 255, 255);
            labelchar.DisplayedString = label + "";
        }

        public static Color firewatchcolor;
        public static char firewatchlabel;
        public static Text firetext;

        //Debug Variables
        CircleShape center;
        RectangleShape bounds;

        CircleShape shape;
        Text labelchar;

        static Vertex()
        {
            debug = false;

            firewatchcolor = new Color(255, 0, 0);
            firewatchlabel = 'S';
        }

        public Vertex(char label, Vector2f pos)
        {
            this.label = label;
            previous = '0';
            distance = uint.MaxValue;

            shape = new CircleShape(20);

            shape.FillColor = new Color(55, 200, 255); //invisible
            shape.OutlineThickness = 5;
            shape.OutlineColor = new Color(255, 255, 255); //white

            shape.Position = pos + new Vector2f(5, 5); //for outlinethickness

            shape.Origin = new Vector2f(shape.GetGlobalBounds().Width / 2, shape.GetGlobalBounds().Height / 2);

            center = new CircleShape(5);

            center.Origin = new Vector2f(center.GetGlobalBounds().Width / 2, center.GetGlobalBounds().Height / 2);

            center.FillColor = new Color(255, 0, 0);

            center.Position = pos;

            bounds = new RectangleShape(new Vector2f(shape.GetLocalBounds().Width, shape.GetLocalBounds().Height));

            bounds.Origin = new Vector2f(bounds.GetLocalBounds().Width / 2, bounds.GetLocalBounds().Height / 2);

            bounds.FillColor = new Color(0, 0, 0, 0);

            bounds.OutlineThickness = 2;
            bounds.OutlineColor = new Color(0, 0, 0); //white

            bounds.Position = pos;

            Font font = new Font("resources/fonts/Font1.ttf");

            if (font == null)
            {
                System.Console.WriteLine("No Such Font");
            }

            labelchar = new Text();

            labelchar.Font = font;
            labelchar.CharacterSize = 40;
            labelchar.Color = new Color(255, 255, 255);
            labelchar.DisplayedString = label + "";
            labelchar.Position = pos + new Vector2f(-2, -12); //font offset

            labelchar.Origin = new Vector2f(labelchar.GetGlobalBounds().Width / 2, labelchar.GetGlobalBounds().Height / 2);
        }

        public void Select()
        {
            shape.OutlineColor = new Color(0, 255, 0);
            labelchar.Color = new Color(0, 255, 0);
        }

        public void Deselect()
        {
            shape.OutlineColor = new Color(255, 255, 255);
            labelchar.Color = new Color(255, 255, 255);
        }

        public Vector2f Position
        {
            get { return center.Position; }
        }

        public void Render(ref RenderWindow window)
        {
            window.Draw(shape);
            window.Draw(labelchar);
            if (debug)
            {
                window.Draw(center);
                window.Draw(bounds);
            }
        }
    }
}
