using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_General
    {
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
    }
}
