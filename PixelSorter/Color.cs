using System;
using ImageMagick;

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

        public Color(IPixel<byte> pixel)
        {
            this.r = pixel.GetChannel(0);
            this.g = pixel.GetChannel(1);
            this.b = pixel.GetChannel(2);
        }

        public bool isTheSame(Color other)
        {
            if (r == other.r && g == other.g && b == other.b)
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