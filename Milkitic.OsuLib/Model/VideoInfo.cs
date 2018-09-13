using System.Globalization;

namespace Milkitic.OsuLib.Model
{
    public class VideoInfo
    {
        public double Offset { get; set; }
        public string Filename { get; set; }
        public override string ToString()
        {
            return $"Video,{Offset.ToString(CultureInfo.InvariantCulture)},\"{Filename}\"";
        }
    }
}