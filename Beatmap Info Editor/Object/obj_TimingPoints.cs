using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_TimingPoints
    {
        public List<_TimingPoints> TimingPointList { get; set; }
        public string TheRestText { get; set; }

        private StringBuilder sb = new StringBuilder();
        public override string ToString()
        {
            sb.Clear();
            sb.AppendLine("[HitObjects]");
            for (int i = 0; i < TimingPointList.Count; i++)
            {
                sb.AppendLine(TimingPointList[i].ToString());
            }
            sb.Append(TheRestText == null ? "" : TheRestText.ToString());
            return sb.ToString();
        }
    }
}
