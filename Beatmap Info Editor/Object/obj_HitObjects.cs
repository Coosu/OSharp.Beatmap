using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_HitObjects
    {
        public List<_HitObjects> HitObjectList { get; set; }
        public string TheRestText { get; set; }

        private StringBuilder sb = new StringBuilder();
        public override string ToString()
        {
            sb.Clear();
            sb.AppendLine("[HitObjects]");
            for (int i = 0; i < HitObjectList.Count; i++)
            {
                sb.AppendLine(HitObjectList[i].ToString());
            }
            sb.Append(TheRestText == null ? "" : TheRestText.ToString());
            return sb.ToString();
        }
    }
}
