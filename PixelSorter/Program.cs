using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;

namespace PixelSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
            if (args.Length < 1)
            {
                Console.WriteLine("You need to specify the image path!");
                return; 
            }

            MagickImage tmpImage;
            MagickImage image;

            MagickGeometry geometry = new MagickGeometry();
            geometry.IgnoreAspectRatio = false;
            geometry.Width = 240;
            geometry.Height = 240;
            
            using (tmpImage = new MagickImage(args[0]))
            {
                tmpImage.Resize(geometry);
                image = new MagickImage(tmpImage);
            }
            
            List<ColorGroup> pixelFrequency = new List<ColorGroup>();

            var pixels = image.GetPixels();
            
            float target = pixels.Count();
            foreach (var pixel in pixels)
            {
                var tmp = new Color(pixel);

                bool hasMatch = false;
                int matchIndex = 0;
                foreach (var pixelData in pixelFrequency)
                {
                    if (pixelData.color.isTheSame(tmp))
                    {
                        hasMatch = true;
                        break;
                    }

                    matchIndex++;
                }

                if (!hasMatch)
                {
                    pixelFrequency.Add(new ColorGroup(tmp));
                }
                else
                {
                    pixelFrequency[matchIndex].count++;
                }
            }

            pixelFrequency.Sort();

            for (int i = 0; i < 16; i++)
            {
                //pixelFrequency[i].color.printHex(true);
            }

            DisplayWindow result = new DisplayWindow(pixelFrequency.GetRange(0,16));
        }
    }
}
