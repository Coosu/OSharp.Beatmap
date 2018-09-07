using Milkitic.OsuLib.Enums;
using Milkitic.OsuLib.Interface;

namespace Milkitic.OsuLib.Model.Section
{
    public class General : KeyValueSection
    {
        public string AudioFilename { get; set; } = "audio.mp3";

        [ConfigBool(BoolParseType.ZeroOne)]
        public int AudioLeadIn { get; set; } = 0;

        public int PreviewTime { get; set; } = 0;

        [ConfigBool(BoolParseType.ZeroOne)]
        public bool Countdown { get; set; } = true;

        public SamplesetEnum SampleSet { get; set; } = 0;
        public double StackLeniency { get; set; } = 0.7;

        [ConfigEnum(EnumParseType.Index)]
        public GameModeEnum Mode { get; set; } = 0;

        [ConfigBool(BoolParseType.ZeroOne)]
        public bool LetterboxInBreaks { get; set; } = false;

        [ConfigBool(BoolParseType.ZeroOne)]
        public bool WidescreenStoryboard { get; set; } = true;

        [ConfigBool(BoolParseType.ZeroOne)]
        public bool EpilepsyWarning { get; set; } = false;
    }
}
