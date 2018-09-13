using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkitic.OsuLib.Model
{
    public class SbSampleInfo
    {
        public int Offset { get; set; }
        public int MagicalInt { get; set; }
        public string Filename { get; set; }
        public int Volume { get; set; }

        public override string ToString()
        {
            return string.Join(",", "Sample", Offset, MagicalInt, Filename, Volume);
        }
    }
}
