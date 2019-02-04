using OSharp.Beatmap.Configurable;
using OSharp.Beatmap.Sections.GamePlay;
using OSharp.Beatmap.Sections.Timing;

namespace OSharp.Beatmap.Sections
{
    public class General : KeyValueSection
    {
        public string AudioFilename { get; set; } = "audio.mp3";

        [ConfigBool(BoolParseType.ZeroOne)]
        public int AudioLeadIn { get; set; } = 0;

        public int PreviewTime { get; set; } = 0;

        [ConfigBool(BoolParseType.ZeroOne)]
        public bool Countdown { get; set; } = true;

        public TimingSampleset SampleSet { get; set; } = 0;
        public double StackLeniency { get; set; } = 0.7;

        [ConfigEnum(EnumParseType.Index)]
        public GameMode Mode { get; set; } = 0;

        [ConfigBool(BoolParseType.ZeroOne)]
        public bool LetterboxInBreaks { get; set; } = false;

        [ConfigBool(BoolParseType.ZeroOne)]
        public bool WidescreenStoryboard { get; set; } = true;

        [ConfigBool(BoolParseType.ZeroOne)]
        public bool EpilepsyWarning { get; set; } = false;
    }
}
