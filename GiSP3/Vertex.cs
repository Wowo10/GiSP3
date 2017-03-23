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

        CircleShape shape;
        Text labelchar;

        public Vertex(char label, Vector2f pos)
        {
            this.label = label;

            shape = new CircleShape(20);

            shape.FillColor = new Color(0,0,0,0); //invisible
            shape.OutlineThickness = 5;
            shape.OutlineColor = new Color(255,255,255); //white

            shape.Position = pos;

            Font font= new Font("resources/fonts/Font.otf");

            if(font == null)
            {
                System.Console.WriteLine("No Such Font");
            }

            labelchar = new Text();

            labelchar.Font = font;
            labelchar.CharacterSize = 30;
            labelchar.Color = new Color(255, 255, 255);
            labelchar.DisplayedString = label + ""; //one of reasons why c# isn`t cool language.
            labelchar.Position = pos + new Vector2f(5,3); //magic number for positioning

        }

        public void Render(ref RenderWindow window)
        {
            window.Draw(shape);
            window.Draw(labelchar);
        }
    }
}