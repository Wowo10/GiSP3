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
        public static int currentlength = 1;

        public static bool searching = false;

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
                if(!searching)
                    CheckCollisions(); //returning value in case if someone needs to read it
            }
        }

        static void OnKeyPress(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                exit = true;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F1)) //Debug Mode
            {
                Vertex.debug = !Vertex.debug;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F2)) //Writing Edges
            {
                Console.WriteLine("Edges: ");
                foreach (var edge in edges)
                {
                    Console.WriteLine(edge.GetPair + " Length: "+ edge.Lenght);
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F3)) //Writing Vertices
            {
                Console.WriteLine("Vertices: ");
                foreach (var vertex in vertices)
                {
                    Console.Write(vertex.Label+" ");
                }
                Console.Write("\n");
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F4)) //Writing mousepos
            {
                Console.WriteLine("Mousepos: " + Mouse.GetPosition().X + ", " + Mouse.GetPosition().Y);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F5)) //Writing currents
            {
                Console.WriteLine("Current selection: " + currentselection +
                    ", Current lenght: " + currentlength + ", Searching: " + searching);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F6)) //Clearing all
            {
                vertices.Clear();
                edges.Clear();

                counter = 'A';

                Console.WriteLine("Cleared!");
            }            
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F7)) //Look all 
            {
                foreach (var edge in edges)
                {
                    edge.Look();
                }

                foreach (var vertex in vertices)
                {
                    vertex.Look();
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F8)) //UnLook all
            {
                foreach (var edge in edges)
                {
                    edge.UnLook();
                }

                foreach (var vertex in vertices)
                {
                    vertex.UnLook();
                }
            }


            else if (Keyboard.IsKeyPressed(Keyboard.Key.Return)) //starting search
            {
                Console.WriteLine("Running search!");
                searching = true;
            }


            //number handler
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num1) && currentselection != null)
            {
                currentlength = 1;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num2) && currentselection != null)
            {
                currentlength = 2;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num3) && currentselection != null)
            {
                currentlength = 3;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num4) && currentselection != null)
            {
                currentlength = 4;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num5) && currentselection != null)
            {
                currentlength = 5;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num6) && currentselection != null)
            {
                currentlength = 6;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num7) && currentselection != null)
            {
                currentlength = 7;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num8) && currentselection != null)
            {
                currentlength = 8;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num9) && currentselection != null)
            {
                currentlength = 9;
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
                        foreach (var vertex in vertices) //deselecting all
                        {
                            vertex.Deselect();
                        }

                        bool addedge = true;

                        foreach(var edge in edges)
                        {
                            if(edge.GetPair.Contains(currentselection.Value) && edge.GetPair.Contains(item.Label))
                            {
                                addedge = false;
                                break;
                            }
                        }

                        if (addedge && item.Label != currentselection)
                        {
                            Vector2f startpos = new Vector2f(0, 0);

                            foreach (var vertex in vertices) //finding posistion of currently selected vertex
                            {
                                if (vertex.Label == currentselection)
                                {
                                    startpos = vertex.Position;
                                    break;
                                }
                            }

                            /* Crappy code, uncommenting on own risk

                            edges.Add(new Edge(new Edge.Pair(currentselection.Value, item.Label),
                                new Vector2f((startpos.X + item.Position.X) / 2, (startpos.Y + item.Position.Y) / 2),
                                Math.Sqrt(Distance(startpos, item.Position)),
                                Math.Atan2(startpos.Y - item.Position.Y, startpos.X - item.Position.X))); //ArcTangens!

                            Console.WriteLine("Start: (" + startpos.X + ", " + startpos.Y + ") Stop: (" + item.Position.X + ", " + item.Position.Y + ")");
                            Console.WriteLine("a: " + (startpos.Y - item.Position.Y) + " b: " + (startpos.X - item.Position.X) + " atg: " + Math.Atan2(startpos.Y - item.Position.Y, startpos.X - item.Position.X));
                            */




                            edges.Add(new Edge(new Edge.Pair(currentselection.Value, item.Label), startpos, item.Position, currentlength));

                        }

                        currentselection = null; //setting back default values
                        currentlength = 1;
                    }
                    return Collision.SELECT;
                }
                else if (distance < 4000) //if colliding with any
                {
                    return Collision.CONTACT;
                }
            }

            if (counter != 'Z' + 1)
            {
                vertices.Add(new Vertex(counter++,
                new Vector2f(Mouse.GetPosition(app).X,
                    Mouse.GetPosition(app).Y))); //Cheat to convert V2i to V2f
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