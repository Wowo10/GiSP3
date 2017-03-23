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

        public static List<Vertex> Vertices = new List<Vertex>(); //List of Vertices
        public static char counter = 'A'; //Counter for vertices` labels

        static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        static void LeftButtonPress(object sender, EventArgs e)
        {
            if(Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if(counter != 'Z' + 1)
                    Vertices.Add(new Vertex(counter++, 
                        new Vector2f(Mouse.GetPosition(app).X, Mouse.GetPosition(app).Y))); //Cheat to convert V2i to V2f
            }
        }

        static void Main()
        {            
            app.Closed += new EventHandler(OnClose);
            app.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(LeftButtonPress);

            Color windowColor = new Color(50, 192, 255);

            Vertices.Add(new Vertex(counter++,new Vector2f(50,50)));

            // Start the main loop
            while (app.IsOpen)
            {
                // Process events
                app.DispatchEvents();

                // Clear screen
                app.Clear(windowColor);

                foreach (var item in Vertices)
                {
                    item.Render(ref app);
                }

                // Update the window
                app.Display();
            } //End game loop
        } //End Main()

    }
}
