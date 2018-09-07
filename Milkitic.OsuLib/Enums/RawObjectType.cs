using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkitic.OsuLib.Enums
{
    [Flags]
    public enum RawObjectType
    {
        Circle = 1,
        Slider = 2,
        NewCombo = 4,
        Spinner = 8,
        NewCbSkip2 = 16,
        NewCbSkip3 = 32,
        NewCbSkip4 = 48,
        NewCbSkip5 = 64,
        NewCbSkip6 = 80,
        NewCbSkip7 = 96,
        NewCbSkip8 = 112,
        Hold = 128
    }
}
