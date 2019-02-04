using System;

namespace OSharp.Beatmap.Configurable
{
    public class ConfigNameAttribute : Attribute
    {
        public string Name { get; }

        public ConfigNameAttribute(string name)
        {
            Name = name;
        }
    }
}