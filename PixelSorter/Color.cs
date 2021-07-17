using System;

namespace PixelSorter
{
    public class Color
    {
        public byte r;
        public byte g;
        public byte b;

        public Color(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public Color(SFML.Graphics.Color color)
        {
            this.r = color.R;
            this.g = color.G;
            this.b = color.B;
        }

        // Legacy thing when ImageMagick was used.
        /*public Color(IPixel<byte> pixel)
        {
            this.r = pixel.GetChannel(0);
            this.g = pixel.GetChannel(1);
            this.b = pixel.GetChannel(2);
        }*/

        public bool isSameGroup(Color other)
        {
            uint threshold = 16;
            
            uint diffR = (uint)Math.Abs(r - other.r);
            uint diffG = (uint)Math.Abs(g - other.g);
            uint diffB = (uint)Math.Abs(b - other.b);
            
            if (diffR <= threshold && diffG <= threshold && diffB <= threshold)
                return true;

            return false;
        }

        public string getHumanColors()
        {
            return $"R: {r}, G: {g}, B: {b}";
        }
        
        public string getRGB()
        {
            return $"({r}, {g}, {b})";
        }

        public string getHex(bool raw)
        {
            byte[] tmp = {r,g,b};
            string hex = BitConverter.ToString(tmp).Replace("-","");

            if (raw)
                return $"{hex}";
            else
                return $"#{hex}";
        }
    }
}