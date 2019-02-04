using System;

namespace OSharp.Beatmap.Configurable
{
    public class ConfigEnumAttribute : Attribute
    {
        public EnumParseType Type { get; }

        public ConfigEnumAttribute(EnumParseType type)
        {
            Type = type;
        }
    }
}