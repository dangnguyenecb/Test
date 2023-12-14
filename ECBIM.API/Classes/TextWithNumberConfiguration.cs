using ECBIM.API.Extensions;
using System.Text;

namespace ECBIM.API
{
    public class TextWithNumberConfiguration
    {
        private int _number;
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int Number { get => _number; set => _number = value; }
        public string Name { get { return (Prefix + Number + Suffix); } }

        public TextWithNumberConfiguration(string str)
        {
            StringBuilder sb_number = new StringBuilder();
            StringBuilder sb_suffix = new StringBuilder();
            bool numberFound = false;

            if (str.ContainsNumber())
            {
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    char c = str[i];

                    if (char.IsNumber(c))
                    {
                        sb_number.Insert(0, c);
                        numberFound = true;
                    }
                    else if (!char.IsNumber(c) && !numberFound)
                    {
                        sb_suffix.Insert(0, c);
                    }
                    else
                    {
                        Prefix = str.Substring(0, i + 1);
                        break;
                    }
                }
                Number = int.Parse(sb_number.ToString());
                Suffix = sb_suffix.ToString();
            }
            else
            {
                Prefix = str + " ";
            }
        }
    }
}

