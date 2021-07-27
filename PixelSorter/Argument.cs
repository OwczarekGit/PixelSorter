using System.Runtime.InteropServices;

namespace PixelSorter
{
    public class Argument
    {
        private string flag;
        private string value;
        
        public Argument(string flag, string value)
        {
            this.flag = flag;
            this.value = value;
        }

        public string toString()
        {
            return $"{flag}: {value}";
        }

        public string getKey()
        {
            return flag;
        }

        public string getValue()
        {
            return value;
        }

        public bool isValueTrue()
        {
            if (value.ToLower().Contains("true") || value.Contains("1"))
                return true;

            return false;
        }
    }
}