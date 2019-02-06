using System;
using System.Collections.Generic;
using System.Text;

namespace OSharp.Beatmap.Configurable
{
    public class SectionConverterAttribute : Attribute
    {
        public Type ConverterType { get; }

        public SectionConverterAttribute(Type converterType)
        {
            if (!converterType.IsSubclassOf(typeof(ValueConverter)))
                throw new Exception($"Type {converterType} isn\'t a converter.");
            ConverterType = converterType;
        }
    }
}
