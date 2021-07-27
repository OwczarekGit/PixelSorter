using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security;
using SFML.Graphics;
using SFML.System;

namespace PixelSorter
{
    class Program
    {
        public enum OutputTypes
        {
            RGB,
            HEX,
        }
        
        public static bool createPickerWindow = true;
        public static bool rawFormat = true;
        public static OutputTypes outputType= OutputTypes.HEX;
        public static int outputColorsCount = 16;
        
        static void Main(string[] args)
        {
            var argParser = new ArgumentParser(args);
            int parserResult = argParser.process();

            if (parserResult != 0)
                return;

            SFML.Graphics.Image image;

            try
            {
                image = new SFML.Graphics.Image(args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to read image: {args[0]}. Invalid format?");
                return;
            }

            List<ColorGroup> slice;

            {
                var imageSize = image.Size;
                var imagePixelCount = imageSize.X * imageSize.Y;

                uint targetSamples = 200000;
                uint step = (uint) (imagePixelCount / targetSamples);

                if (step < 1)
                    step = 1;

                List<Color> pixels = new List<Color>();

                for (uint i = 0; i < imagePixelCount; i += step)
                {
                    uint x = i % imageSize.X;
                    uint y = i / imageSize.X;
                    pixels.Add(new Color(image.GetPixel(x, y)));
                }


                // Sort pixels by their amount in the image.
                List<ColorGroup> pixelFrequency = new List<ColorGroup>();

                foreach (var pixel in pixels)
                {
                    var tmp = pixel;

                    bool hasMatch = false;
                    int index = 0;
                    int foundIndex = 0;


                    foreach (var pixelData in pixelFrequency)
                    {
                        if (pixelData.color.isSameGroup(tmp))
                        {
                            hasMatch = true;
                            foundIndex = index;
                            //break;
                        }

                        index++;
                    }

                    if (!hasMatch)
                    {
                        pixelFrequency.Add(new ColorGroup(tmp));
                    }
                    else
                    {
                        pixelFrequency[foundIndex].count++;
                    }
                }

                pixelFrequency.Sort();

                int sliceSize = outputColorsCount;
                try
                {
                    slice = pixelFrequency.GetRange(pixelFrequency.Count - sliceSize, sliceSize);
                }
                catch
                {
                    slice = pixelFrequency.GetRange(0, pixelFrequency.Count);
                }
                
                // Force to do some cleaning.
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            if (createPickerWindow)
            {
                DisplayWindow result = new DisplayWindow(slice);
            }
            else
            {
                foreach (var color in slice)
                {
                    switch (outputType)
                    {
                        case OutputTypes.HEX:
                        {
                            Console.WriteLine(color.color.getHex(rawFormat));
                            break;
                        }
                        case OutputTypes.RGB:
                        {
                            Console.WriteLine(color.color.getRGB(rawFormat));
                            break;
                        }
                    }
                }
            }
        }
    }
}
