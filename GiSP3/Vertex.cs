using SFML.Graphics;
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

        public static bool debug = false;

        //Debug Variables
        CircleShape center;
        RectangleShape bounds;

        CircleShape shape;
        Text labelchar;

        public Vertex(char label, Vector2f pos)
        {
            this.label = label;

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

            Font font = new Font("resources/fonts/Font.otf");

            if (font == null)
            {
                System.Console.WriteLine("No Such Font");
            }

            labelchar = new Text();

            labelchar.Font = font;
            labelchar.CharacterSize = 30;
            labelchar.Color = new Color(255, 255, 255);
            labelchar.DisplayedString = label + ""; //one of reasons why c# isn`t cool language.
            labelchar.Position = pos + new Vector2f(-2, -8); //magic number for positioning

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

        public void Look()
        {
            shape.OutlineColor = new Color(255, 127, 0);
            labelchar.Color = new Color(255, 127, 0);
        }

        public void UnLook()
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
