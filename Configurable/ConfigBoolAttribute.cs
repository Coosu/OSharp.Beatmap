using System;

namespace OSharp.Beatmap.Configurable
{
    public class ConfigBoolAttribute : Attribute
    {
        public BoolParseType Type { get; }

        public ConfigBoolAttribute(BoolParseType type)
        {
            Type = type;
        }
    }
}