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
        static RenderWindow app = new RenderWindow(new VideoMode(800, 600), "Project 3 - FireWatch");
        static bool exit = false;

        public static List<Vertex> vertices = new List<Vertex>(); //List of Vertices
        public static char counter = 'A'; //Counter for vertices` labels
        public static char djikstracounter = 'A';

        public static List<Edge> edges = new List<Edge>();
        public static char? currentselection = null;
        public static uint currentlength = 1;

        public static bool searching = false;
        public static bool after = false;

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
                if (after)
                {
                    foreach (var vertex in vertices)
                    {
                        vertex.FireOff();
                    }
                }
                if (!searching)
                    CheckCollisions(); //returning value in case if someone needs to read it
            }
        }

        static void OnKeyPress(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                exit = true;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F1)) //Writing Edges
            {
                Console.WriteLine("Edges: ");
                foreach (var edge in edges)
                {
                    Console.WriteLine(edge.GetPair + " Length: " + edge.Lenght);
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F2)) //Writing Vertices
            {
                Console.WriteLine("Vertices: ");
                foreach (var vertex in vertices)
                {
                    Console.Write(vertex.Label + " ");
                }
                Console.Write("\n");
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.F3)) //Clearing all
            {
                vertices.Clear();
                edges.Clear();

                counter = 'A';

                Console.WriteLine("Cleared!");
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

            return dist;
        }

        static Collision CheckCollisions()
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

                        foreach (var edge in edges)
                        {
                            if (edge.GetPair.Contains(currentselection.Value) && edge.GetPair.Contains(item.Label))
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

        // Djikstras sheets

        static Vertex FindVertex(char label)
        {
            foreach (var vertex in vertices)
            {
                if (vertex.Label == label)
                    return vertex;
            }

            return null;
        }

        static char[] FindNeighbours(char vertexlabel)
        {
            List<char> container = new List<char>();

            foreach (var edge in edges)
            {
                if (edge.Contains(vertexlabel))
                {
                    container.Add(edge.GetSecond(vertexlabel));
                }
            }

            return container.ToArray();
        }

        static uint GetLength(char first, char second)
        {
            foreach (var edge in edges)
            {
                if (edge.GetPair.Contains(first) && edge.GetPair.Contains(second))
                {
                    return edge.Lenght;
                }
            }

            return 0;
        }

        static bool IsInArray(char[] tab, char findme)
        {
            foreach (var character in tab)
            {
                if (character == findme)
                {
                    return true;
                }
            }

            return false;
        }

        static char PopFirst(ref char[] tab)
        {
            char[] temp = new char[tab.Length - 1];

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = tab[i + 1];
            }

            char ret = tab[0];

            tab = temp;

            return ret;
        }

        static void SortQueue(ref char[] tab)
        {
            char[] temp = new char[tab.Length];

            for (int i = 0; i < tab.Length; i++)
            {
                uint mindist = uint.MaxValue;
                char counter = '0';

                for (int j = 0; j < tab.Length; j++)
                {
                    if (!IsInArray(temp, tab[j]) && FindVertex(tab[j]).Dist <= mindist)
                    {
                        mindist = FindVertex(tab[j]).Dist;
                        counter = FindVertex(tab[j]).Label;
                    }
                }
                temp[i] = counter;
            }

            tab = temp;
        }

        static void Djikstra(char start)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].Label == start)
                    vertices[i].Dist = 0;
                else
                    vertices[i].Dist = uint.MaxValue;

                vertices[i].Prev = '0';
            }

            char[] queue = new char[vertices.Count];

            for (int i = 0; i < queue.Length; i++)
            {
                queue[i] = (char)('A' + i);
            }

            SortQueue(ref queue);

            while (queue.Length != 0)
            {
                char u = PopFirst(ref queue);
                Vertex current = FindVertex(u);

                char[] neightbours = FindNeighbours(u);

                for (int i = 0; i < neightbours.Length; i++)
                {
                    if (current.Dist != uint.MaxValue && FindVertex(neightbours[i]).Dist > current.Dist + GetLength(neightbours[i], u))
                    {
                        FindVertex(neightbours[i]).Dist = current.Dist + GetLength(neightbours[i], u);
                        FindVertex(neightbours[i]).Prev = u;
                    }
                }

                SortQueue(ref queue);
            }
        }

        struct VertexData
        {
            char label, prev;
            uint distance;
            public uint Dist
            {
                get { return distance; }
            }
            public VertexData(char label, char prev, uint distance)
            {
                this.label = label;
                this.prev = prev;
                this.distance = distance;
            }

            public override string ToString()
            {
                string msg = label + ":(";

                if (distance != uint.MaxValue)
                    msg += distance;
                else
                    msg += "inf";

                msg += "/";

                if (prev != '0')
                    msg += prev + ")";
                else
                    msg += "nil)";

                return msg;
            }
        }

        static List<List<VertexData>> djikstradata = new List<List<VertexData>>();

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

                if (searching)
                {
                    if ('A' == djikstracounter)
                    {
                        foreach (var data in djikstradata)
                        {
                            data.Clear();
                        }
                        djikstradata.Clear();

                    }
                    else if (counter == djikstracounter)
                    {
                        uint[] amounts = new uint[vertices.Count];
                        searching = false;
                        djikstracounter = 'A';
                        Console.WriteLine("Searching Done!");
                        char counter = 'A';
                        foreach (var data in djikstradata)
                        {
                            uint sum = 0;
                            foreach (var vdata in data)
                            {
                                sum += vdata.Dist;
                            }
                            amounts[counter++ - 'A'] = sum;
                        }

                        uint min = uint.MaxValue;
                        for (int i = 0; i < vertices.Count; i++) //finding min of sums
                        {
                            if (amounts[i] < min)
                            {
                                min = amounts[i];
                            }
                        }

                        for (int i = 0; i < vertices.Count; i++) //if min then show it
                        {
                            if (amounts[i] == min)
                            {
                                vertices[i].FireOn();
                            }
                        }
                        after = true;

                        continue;
                    }

                    Djikstra(djikstracounter);
                    djikstradata.Add(new List<VertexData>());
                    foreach (var vertex in vertices)
                    {
                        djikstradata[djikstracounter - 'A'].Add(new VertexData(vertex.Label, vertex.Prev, vertex.Dist));
                    }
                    djikstracounter++;
                }

                app.Display();
            } 
        } 
    }
}