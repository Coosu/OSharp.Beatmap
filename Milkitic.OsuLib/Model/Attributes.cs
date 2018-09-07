using System;

namespace Milkitic.OsuLib.Model
{
    public class ConfigNameAttribute : Attribute
    {
        public string Name { get; }

        public ConfigNameAttribute(string name)
        {
            Name = name;
        }
    }
    public class ConfigEnumAttribute : Attribute
    {
        public EnumParseType Type { get; }

        public ConfigEnumAttribute(EnumParseType type)
        {
            Type = type;
        }
    }

    public class ConfigBoolAttribute : Attribute
    {
        public BoolParseType Type { get; }

        public ConfigBoolAttribute(BoolParseType type)
        {
            Type = type;
        }
    }


    public class ConfigIgnoreAttribute : Attribute
    {
    }

    public enum BoolParseType
    {
        String, ZeroOne
    }

    public enum EnumParseType
    {
        String, Index
    }
}
