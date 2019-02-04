namespace OSharp.Beatmap.Sections
{
    public class Difficulty : KeyValueSection
    {
        public double HPDrainRate { get; set; } = 5;
        public double CircleSize { get; set; } = 5;
        public double OverallDifficulty { get; set; } = 5;
        public double ApproachRate { get; set; } = 5;
        public double SliderMultiplier { get; set; } = 1.0;
        public double SliderTickRate { get; set; } = 1.0;
    }
}
