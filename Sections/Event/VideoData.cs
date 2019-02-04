using System.Globalization;

namespace OSharp.Beatmap.Sections.Event
{
    public class VideoData
    {
        public double Offset { get; set; }
        public string Filename { get; set; }
        public override string ToString()
        {
            return $"Video,{Offset.ToString(CultureInfo.InvariantCulture)},\"{Filename}\"";
        }
    }
}