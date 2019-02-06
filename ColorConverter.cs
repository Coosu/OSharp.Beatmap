using OSharp.Beatmap.Configurable;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OSharp.Beatmap
{
    public class ColorConverter : ValueConverter<Color>
    {
        public override Color ReadSection(string value)
        {
            var colors = value.Split(',').Select(int.Parse).ToArray();
            return Color.FromArgb(colors[0], colors[1], colors[2]);
        }

        public override string WriteSection(Color value)
        {
            return $"{value.R},{value.G},{value.B}";
        }
    }
}
