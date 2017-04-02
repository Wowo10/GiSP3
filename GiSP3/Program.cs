using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace GiSP3
{
    static class Program
    {
        // Create the main window
        static RenderWindow app = new RenderWindow(new VideoMode(800, 600), "Projekt 3 - FireWatch");
        static bool exit = false;

        public static List<Vertex> vertices = new List<Vertex>(); //List of Vertices
        public static char counter = 'A'; //Counter for vertices` labels

        public static List<Edge> edges = new List<Edge>();
        public static char? currentselection = null;

        static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        enum Collision
        {
            NONE = 0,
            CONTACT,
            SELECT
        }

        static void OnButtonPress(object sender, EventArgs e)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (counter != 'Z' + 1)
                {
                    if (CheckCollisions() == Collision.NONE)
                    {
                        vertices.Add(new Vertex(counter++,
                            new Vector2f(Mouse.GetPosition(app).X,
                                Mouse.GetPosition(app).Y))); //Cheat to convert V2i to V2f
                    }
                }
            }
        }

        static void OnKeyPress(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                exit = true;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F1))
            {
                Vertex.debug = !Vertex.debug;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F2))
            {
                Console.WriteLine("Edges: ");
                foreach (var edge in edges)
                {
                    Console.WriteLine(edge.GetPair);
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F4))
            {
                Console.WriteLine("Mousepos: "+Mouse.GetPosition().X+", "+Mouse.GetPosition().Y);
            }
        }

        static double Distance(Vector2f start, Vector2f stop) //not rly, only squares, cause sqr is expensive
        {
            double dist = (start.X - stop.X) * (start.X - stop.X) + (start.Y - stop.Y) * (start.Y - stop.Y);
            //Console.WriteLine("(" + start.X + ", " + start.Y + "), (" + stop.X + ", " + stop.Y + "): " + dist);// debug checking distance

            return dist;
        }

        static Collision CheckCollisions() //true for no collisions detected
        {
            foreach (var item in vertices)
            {
                double distance = Distance(item.Position, new Vector2f(
                        Mouse.GetPosition(app).X, Mouse.GetPosition(app).Y));
                if (distance < 750)
                {
                    if (currentselection == null)
                    {
                        item.Select();
                        currentselection = item.Label;
                    }
                    else
                    {
                        foreach (var vertex in vertices)
                        {
                            vertex.Deselect();
                        }

                        Vector2f startpos = new Vector2f(0, 0);

                        foreach (var vertex in vertices)
                        {
                            if (vertex.Label == currentselection)
                            {
                                startpos = vertex.Position;
                                break;
                            }
                        }

                        /*
                        edges.Add(new Edge(new Edge.Pair(currentselection.Value, item.Label),
                            new Vector2f((startpos.X + item.Position.X) / 2, (startpos.Y + item.Position.Y) / 2),
                            Math.Sqrt(Distance(startpos, item.Position)),
                            Math.Atan2(startpos.Y - item.Position.Y, startpos.X - item.Position.X))); //ArcTangens!

                        Console.WriteLine("Start: (" + startpos.X + ", " + startpos.Y + ") Stop: (" + item.Position.X + ", " + item.Position.Y + ")");
                        Console.WriteLine("a: " + (startpos.Y - item.Position.Y) + " b: " + (startpos.X - item.Position.X) + " atg: " + Math.Atan2(startpos.Y - item.Position.Y, startpos.X - item.Position.X));
                        */

                        edges.Add(new Edge(new Edge.Pair(currentselection.Value, item.Label), startpos, item.Position));

                        currentselection = null;
                    }
                    return Collision.SELECT;
                }
                else if (distance < 4000) //if colliding with any
                {
                    return Collision.CONTACT;
                }
            }
            return Collision.NONE;
        }

        static void Main()
        {
            app.Closed += new EventHandler(OnClose);
            app.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnButtonPress);
            app.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPress);

            Color windowColor = new Color(50, 192, 255);

            // Start the main loop
            while (app.IsOpen && !exit)
            {
                // Process events
                app.DispatchEvents();

                // Clear screen
                app.Clear(windowColor);

                //Drawing edges
                foreach (var edge in edges)
                {
                    edge.Render(ref app);
                }

                //Drawing Vertices
                foreach (var vertex in vertices)
                {
                    vertex.Render(ref app);
                }

                // Update the window
                app.Display();
            } //End game loop
        } //End Main()

    }
}
