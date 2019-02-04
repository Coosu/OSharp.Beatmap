namespace OSharp.Beatmap.Sections.Event
{
    public class StoryboardSampleData
    {
        public int Offset { get; set; }
        public int MagicalInt { get; set; }
        public string Filename { get; set; }
        public int Volume { get; set; }

        public override string ToString()
        {
            return string.Join(",", "Sample", Offset, MagicalInt, Filename, Volume);
        }
    }
}
