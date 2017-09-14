using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_General
    {
        StringBuilder sb = new StringBuilder();

        public string AudioFilename { get; set; }
        public int AudioLeadIn { get; set; }
        public int PreviewTime { get; set; }
        public bool Countdown { get; set; }
        public _SampleSet SampleSet { get; set; }
        public double StackLeniency { get; set; }
        public _Mode Mode { get; set; }
        public bool LetterboxInBreaks { get; set; }
        public bool EpilepsyWarning { get; set; } // 默认0
        public bool WidescreenStoryboard { get; set; }
        public string TheRestText { get; set; }
        public override string ToString()
        {
            var list = GetType().GetProperties();

            sb.Clear();
            sb.AppendLine("[General]");
            for (int i = 0; i < list.Length - 1; i++)
            {
                if (list[i].PropertyType == typeof(bool))
                    sb.AppendLine(list[i].Name + ": " + Convert.ToInt32(list[i].GetValue(this)));
                else if (list[i].PropertyType == typeof(_Mode))
                    sb.AppendLine(list[i].Name + ": " + (int)list[i].GetValue(this));
                else
                    sb.AppendLine(list[i].Name + ": " + list[i].GetValue(this));
            }
            sb.Append(TheRestText.ToString());
            return sb.ToString();
        }
    }
}
