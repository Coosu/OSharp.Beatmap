using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_CompareInfo
    {
        public string Name { get; set; }
        public bool Same { get; set; }
        public List<obj_DifferentInfo> DifferentInfo { get; set; } = new List<obj_DifferentInfo>();
    }
    public class obj_DifferentInfo
    {
        public List<string> Difficulty { get; set; } = new List<string>();
        public string Information { get; set; }
    }
}
