using System;

namespace OSharp.Beatmap.Enums
{
    public static class EnumExtension
    {
        public static T ParseToEnum<T>(this string value)
        {
            if (typeof(T) == typeof(SliderTypeEnum))
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
