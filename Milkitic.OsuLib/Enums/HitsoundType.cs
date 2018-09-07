using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkitic.OsuLib.Enums
{
    [Flags]
    public enum HitsoundType
    {
        Normal = 1,
        Whistle = 2,
        Finish = 4,
        Clap = 8,
    }
}
