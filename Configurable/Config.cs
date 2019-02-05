using System;
using System.Collections.Generic;
using System.Text;

namespace OSharp.Beatmap.Configurable
{
    public abstract class Config
    {
        internal abstract void HandleCustom(string line);
    }
}
