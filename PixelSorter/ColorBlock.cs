using System;
using System.IO;
using System.Reflection;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace PixelSorter
{
    public class ColorBlock
    {
        public RectangleShape shape { get; private set; }
        public Vector2f position;
        public Vector2f size;
        public SFML.Graphics.Color color;
        public Text text = new Text();
        public Font font;
        private Color colorData;

        public bool isHovered = false;
        
        public ColorBlock(Vector2f position, Vector2f size, Color color)
        {
            shape = new RectangleShape();
            
            // Get font from EmbeddedResources and set it.
            Stream fontStream = this.GetType().Assembly.GetManifestResourceStream("PixelSorter.font.ttf");
            byte[] fontData = new byte[fontStream.Length];
            fontStream.Read(fontData, 0, (int) fontStream.Length);
            fontStream.Close();

            font = new Font(fontData);


            this.position = position;
            this.size = size;
            this.color = new SFML.Graphics.Color(color.r, color.g, color.b);
            colorData = color;

            switch (Program.outputType)
            {
                case Program.OutputTypes.HEX:
                { 
                    this.text.DisplayedString = colorData.getHex(Program.rawFormat);
                    break;
                }
                case Program.OutputTypes.RGB:
                {
                    this.text.DisplayedString = colorData.getRGB(Program.rawFormat);
                    break;
                }
                
            }
            
            this.text.CharacterSize = (uint) (200/text.DisplayedString.Length)-2;
            this.text.FillColor = SFML.Graphics.Color.White;
            this.text.OutlineColor = SFML.Graphics.Color.Black;
            this.text.OutlineThickness = 1;
            this.text.Position = new Vector2f(text.CharacterSize/2+position.X+size.X/2, 12);
            this.text.Rotation = (float) (90);
            this.text.Font = this.font;

            shape.Position = this.position;
            shape.Size = this.size;
            shape.FillColor = this.color;
            
        }

        public void draw(RenderWindow window)
        {
            window.Draw(shape);
        }
        
        public void update(RenderWindow window)
        {
            if (isHovered)
            {
                window.Draw(text);
            }
        }

        public string copyToClipboard()
        {
            switch (Program.outputType)
            {
                case Program.OutputTypes.HEX:
                {
                    TextCopy.ClipboardService.SetText($"{colorData.getHex(Program.rawFormat)}");
                    return colorData.getHex(Program.rawFormat);
                    break;
                }
                case Program.OutputTypes.RGB:
                {
                    TextCopy.ClipboardService.SetText($"{colorData.getRGB(Program.rawFormat)}");
                    return colorData.getRGB(Program.rawFormat);
                    break;
                }
            }
            
            return colorData.getHex(Program.rawFormat);
        }
    }
}