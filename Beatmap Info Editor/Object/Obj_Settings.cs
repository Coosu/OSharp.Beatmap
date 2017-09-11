using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_Settings
    {
        public int Version { get; set; }
        public obj_General General { get; set; }
        public obj_Editor Editor { get; set; }
        public obj_Metadata Metadata { get; set; }
        public obj_Difficulty Difficulty { get; set; }
        public obj_Events Events { get; set; }
        public obj_TimingPoints TimingPoints { get; set; }
        public obj_Colours Colours { get; set; }
        public obj_HitObjects HitObjects { get; set; }
    }
}
