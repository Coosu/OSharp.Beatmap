using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_Difficulty
    {
        private StringBuilder sb = new StringBuilder();

        public double HPDrainRate { get; set; }
        public double CircleSize { get; set; }
        public double OverallDifficulty { get; set; }
        public double ApproachRate { get; set; }
        public double SliderMultiplier { get; set; }
        public double SliderTickRate { get; set; }
        public string TheRestText { get; set; }

        public override string ToString()
        {
            var list = GetType().GetProperties();

            sb.Clear();
            sb.AppendLine("[Difficulty]");
            for (int i = 0; i < list.Length - 1; i++)
            {
                if (list[i].PropertyType == typeof(bool))
                    sb.AppendLine(list[i].Name + ":" + Convert.ToInt32(list[i].GetValue(this))); 
                else
                    sb.AppendLine(list[i].Name + ":" + list[i].GetValue(this));
            }
            sb.Append(TheRestText.ToString());
            return sb.ToString();
        }
    }
}
