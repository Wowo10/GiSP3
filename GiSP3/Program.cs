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

        static void ButtonPress(object sender, EventArgs e)
        {
            if(Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                //Need to add checking for:
                //Vertices collision (probably some distance between center and click for all vertices
                //if user is clicking on existing vertex to click on it
                if(counter != 'Z' + 1)
                    vertices.Add(new Vertex(counter++, 
                        new Vector2f(Mouse.GetPosition(app).X, Mouse.GetPosition(app).Y))); //Cheat to convert V2i to V2f
            }
        }

        static void Main()
        {            
            app.Closed += new EventHandler(OnClose);
            app.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(ButtonPress);

            Color windowColor = new Color(50, 192, 255);

            // Start the main loop
            while (app.IsOpen)
            {
                // Process events
                app.DispatchEvents();

                // Clear screen
                app.Clear(windowColor);

                //Drawing edges
                foreach (var item in edges)
                {
                    item.Render(ref app);
                }

                //Drawing Vertices
                foreach (var item in vertices)
                {
                    item.Render(ref app);
                }

                // Update the window
                app.Display();
            } //End game loop
        } //End Main()

    }
}
