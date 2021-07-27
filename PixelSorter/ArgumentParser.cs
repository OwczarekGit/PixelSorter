using System;
using System.Collections.Generic;

namespace PixelSorter
{
    public class ArgumentParser
    {
        private string[] args;
        
        public ArgumentParser(string[] args)
        {
            this.args = args;
        }

        public int process()
        {
            if (args.Length < 1)
            {
                Console.WriteLine("You need to specify the image path!");
                return -1;
            }

            // Parsing argument list into List of pairs.
            List<Argument> arguments = new List<Argument>();

            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-") || args[i].StartsWith("--"))
                    {
                        var tmpArg = new Argument(args[i].Replace("-", ""), args[i + 1]);
                        arguments.Add(tmpArg);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error parsing args.");
                return -1;
            }

            // Processing argument pairs.
            foreach (var argument in arguments)
            {
                // Check if should output to stdout (guess that's useful for scripts?).
                if (argument.getKey().Equals("s") || argument.getKey().Equals("stdout"))
                {
                    if (argument.isValueTrue())
                    {
                        Program.createPickerWindow = false;
                    }
                }

                // Check for desired output format.
                if (argument.getKey().Equals("f") || argument.getKey().Equals("format"))
                {
                    switch (argument.getValue().ToUpper())
                    {
                        case "RGB":
                        {
                            Program.outputType = Program.OutputTypes.RGB;
                            break;
                        }
                        case "HEX":
                        {
                            Program.outputType = Program.OutputTypes.HEX;
                            break;
                        }
                        default:
                        {
                            Console.WriteLine($"Invalid format: {argument.getValue()}");
                            return -1;
                        }
                    }
                }

                // Check how many colors should be outputted.
                if (argument.getKey().Equals("c") || argument.getKey().Equals("colors"))
                {
                    try
                    {
                        Program.outputColorsCount = int.Parse(argument.getValue());
                    }
                    catch
                    {
                        Console.WriteLine($"Cannot set: {argument.getValue()} as the output color count.");
                        return -1;
                    }
                }

                // Check if should fancy format values or keep them in raw format.
                if (argument.getKey().Equals("r") || argument.getKey().Equals("raw"))
                {
                    if (argument.isValueTrue())
                    {
                        Program.rawFormat = true;
                    }
                    else
                    {
                        Program.rawFormat = false;
                    }
                }
            }

            return 0;
        }
    }
}