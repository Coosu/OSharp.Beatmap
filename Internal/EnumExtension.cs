using OSharp.Beatmap.Sections.HitObject;
using System;

namespace OSharp.Beatmap.Internal
{
    public static class EnumExtension
    {
        internal static T ParseToEnum<T>(this string value)
        {
            if (typeof(T) == typeof(SliderType))
            {
                if (value == "L")
                    value = "Linear";
                else if (value == "P")
                    value = "Perfect";
                else if (value == "B")
                    value = "Bezier";
                else if (value == "C")
                    value = "Catmull";
            }
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
