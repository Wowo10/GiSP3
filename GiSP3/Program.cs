using System;
using SFML.Window;
using SFML.Graphics;

namespace GiSP3
{
    static class Program
    {
        static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        static void Main()
        {
            // Create the main window
            RenderWindow app = new RenderWindow(new VideoMode(800, 600), "Projekt 3");
            app.Closed += new EventHandler(OnClose);

            Color windowColor = new Color(0, 192, 255);

            // Start the main loop
            while (app.IsOpen)
            {
                // Process events
                app.DispatchEvents();

                // Clear screen
                app.Clear(windowColor);

                // Update the window
                app.Display();
            } //End game loop
        } //End Main()

    }
}
