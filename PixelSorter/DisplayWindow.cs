using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PixelSorter
{
    public class DisplayWindow
    {
        public RenderWindow window = new RenderWindow(new VideoMode(800,200), "PixelSorter", Styles.Titlebar | Styles.Close);
        public List<ColorBlock> blocks = new List<ColorBlock>();
        public RectangleShape blinker = new RectangleShape();
        private byte blinkerDuration = 0;
        private byte blinkerStep = 24;

        public DisplayWindow(List<ColorGroup> colors)
        {
            window.KeyPressed += keyPressed;
            window.MouseButtonPressed += mousePressed;
            window.MouseMoved += mouseMoved;
            window.Closed += windowClosed;
            window.SetFramerateLimit(30);
            
            // Setup blinker
            blinker.Size = (Vector2f)window.Size;
            blinker.Position = new Vector2f(0, 0);
            blinker.FillColor = SFML.Graphics.Color.White;


            int currentX = 0;
            long step = window.Size.X / colors.Count;
            
            foreach (var color in colors)
            {
                blocks.Add(new ColorBlock(new Vector2f(currentX*step, 0), new Vector2f(step, window.Size.Y), color.color));
                currentX++;
            }
            
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();
                
                draw();
                update();
                
                window.Display();
            }
        }

        private void draw()
        {
            foreach (var block in blocks)
            {
                block.draw(window);
            }
            
            window.Draw(blinker);
        }

        private void update()
        {
            foreach (var block in blocks)
            {
                block.update(window);
            }

            blinker.FillColor = new SFML.Graphics.Color(blinker.FillColor.R,  blinker.FillColor.G, blinker.FillColor.B, blinkerDuration);

            blinkerDuration = (byte)(blinkerDuration - blinkerStep > 0 ? blinkerDuration - blinkerStep : 0);

        }

        private void mousePressed(object sender, MouseButtonEventArgs e)
        {
            foreach (var block in blocks)
            {
                if (block.isHovered && e.Button == Mouse.Button.Left)
                {
                    var result = block.copyToClipboard();
                    blinkerDuration = 255;
                    blinker.FillColor = block.color;
                    window.SetTitle($"PixelSorter | Copied {result}");
                }
            }
        }

        private void mouseMoved(object sender, MouseMoveEventArgs e)
        {
            var mouse = new Vector2f(e.X, e.Y);
            
            foreach (var block in blocks)
            {
                if(block.shape.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                    block.isHovered = true;
                else
                    block.isHovered = false;
            }
        }

        private void keyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Q)
                window.Close();
        }

        private void windowClosed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}