using System;

namespace PixelSorter
{
    public class ColorGroup : IComparable
    {
        public int count = 0;
        public Color color;

        public ColorGroup(Color c)
        {
            color = c;
            count++;
        }

        public int CompareTo(object? other)
        {
            ColorGroup tmp = (ColorGroup) other;

            if (tmp.count > count)
                return -1;
            else
                return 1;
        }
    }
}