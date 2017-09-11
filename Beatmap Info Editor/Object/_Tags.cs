using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class _Tags
    {
        public List<string> Item { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Item)
            {
                sb.Append(item + " ");
            }
            return sb.ToString().Trim(' ');
        }
    }
}
