using System.Drawing;
using OSharp.Beatmap.Configurable;

namespace OSharp.Beatmap.Sections
{
    /// <summary>
    /// Not implemented
    /// </summary>
    [SectionProperty("Colours")]
    public class Colours : KeyValueSection
    {
        public Color Combo1 { get; set; }
        public Color Combo2 { get; set; }
        public Color Combo3 { get; set; }
        public Color Combo4 { get; set; }
        public Color Combo5 { get; set; }
        public Color Combo6 { get; set; }
        public Color Combo7 { get; set; }
        public Color Combo8 { get; set; }
    }
}
