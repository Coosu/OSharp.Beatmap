using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_Colours
    {
        StringBuilder sb = new StringBuilder();
        public Color Combo1 { get; set; }
        public Color Combo2 { get; set; }
        public Color Combo3 { get; set; }
        public Color Combo4 { get; set; }
        public Color Combo5 { get; set; }
        public Color Combo6 { get; set; }
        public Color Combo7 { get; set; }
        public Color Combo8 { get; set; }
        public int Count { get; set; }
        public string TheRestText { get; set; }
        public override string ToString()
        {
            Color tmp;
            var list = GetType().GetProperties();

            sb.Clear();
            sb.AppendLine("[Colours]");
            for (int i = 0; i < Count; i++)
            {
                tmp = (Color)(list[i].GetValue(this));
                if (tmp.R != 0) sb.AppendLine(list[i].Name + ": " + tmp.R + "," + tmp.G + "," + tmp.B);
            }
            sb.Append(TheRestText.ToString());
            return sb.ToString();
        }
    }
}
