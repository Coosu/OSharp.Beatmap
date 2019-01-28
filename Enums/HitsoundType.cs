using System;

namespace OSharp.Beatmap.Enums
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
