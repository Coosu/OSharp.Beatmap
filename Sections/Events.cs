using OSharp.Beatmap.Sections.Event;
using OSharp.Storyboard.Management;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using OSharp.Common.Mathematics;

namespace OSharp.Beatmap.Sections
{
    public class Events : ISection
    {
        public BackgroundData BackgroundInfo { get; set; }
        public VideoData VideoInfo { get; set; }
        public List<StoryboardSampleData> SampleInfo { get; set; } = new List<StoryboardSampleData>();
        public List<RangeValue<double>> Breaks { get; set; } = new List<RangeValue<double>>();
        public ElementGroup ElementGroup { get; set; }

        private readonly StringBuilder _sbInfo = new StringBuilder();
        private readonly Dictionary<string, StringBuilder> _unknownSection = new Dictionary<string, StringBuilder>();
        private string _currentSection;

        private const string SectionBgVideo = "//Background and Video events";
        private const string SectionBreak = "//Break Periods";
        private const string SectionStoryboard = "//Storyboard";
        private const string SectionSbSamples = "//Storyboard Sound Samples";

        public void Match(string line)
        {
            if (line.StartsWith("//"))
            {
                var section = line.Trim();
                switch (section)
                {
                    case SectionBgVideo:
                        _currentSection = SectionBgVideo;
                        break;
                    case SectionBreak:
                        _currentSection = SectionBreak;
                        break;
                    case SectionSbSamples:
                        ElementGroup = ElementGroup.ParseFromText(_sbInfo.ToString().Trim('\r', '\n'));
                        _currentSection = SectionSbSamples;
                        break;
                    default:
                        if (section.StartsWith(SectionStoryboard))
                        {
                            _currentSection = SectionStoryboard;
                            _sbInfo.AppendLine(line);
                        }
                        else
                        {
                            _currentSection = section;
                            _unknownSection.Add(section, new StringBuilder());
                        }
                        break;
                }
            }
            else
            {
                switch (_currentSection)
                {
                    case SectionBgVideo:
                        if (line.StartsWith("Video,"))
                        {
                            var infos = line.Split(',');
                            VideoInfo = new VideoData { Offset = double.Parse(infos[1]), Filename = infos[2].Trim('"') };
                        }
                        else
                        {
                            var infos = line.Split(',');
                            double x = 0, y = 0;
                            if (infos.Length > 3)
                            {
                                x = double.Parse(infos[3]);
                                y = double.Parse(infos[4]);
                            }

                            BackgroundInfo = new BackgroundData
                            {
                                Unknown1 = infos[0],
                                Unknown2 = infos[1],
                                Filename = infos[2].Trim('"'),
                                X = x,
                                Y = y
                            };
                        }
                        break;
                    case SectionBreak:
                        {
                            var infos = line.Split(',');
                            Breaks.Add(new RangeValue<double>(double.Parse(infos[1]), double.Parse(infos[2])));
                        }
                        break;
                    case SectionSbSamples:
                        if (line.StartsWith("Sample,"))
                        {
                            var infos = line.Split(',');
                            SampleInfo.Add(new StoryboardSampleData
                            {
                                Offset = int.Parse(infos[1]),
                                MagicalInt = int.Parse(infos[2]),
                                Filename = infos[3].Trim('"'),
                                Volume = int.Parse(infos[4]),
                            });
                        }
                        break;
                    case SectionStoryboard:
                        _sbInfo.AppendLine(line);
                        break;
                    default:
                        _unknownSection[_currentSection].AppendLine(line);
                        break;
                }
            }
        }

        public string ToSerializedString()
        {
            return string.Join("\r\n",
                       "[Events]",
                       SectionBgVideo,
                       VideoInfo,
                       BackgroundInfo,
                       SectionBreak,
                       string.Join("\r\n", Breaks.Select(q => string.Join(",", "2",
                           q.StartTime.ToString(CultureInfo.InvariantCulture),
                           q.EndTime.ToString(CultureInfo.InvariantCulture)))),
                       _sbInfo.ToString().TrimEnd('\r', '\n'),
                       SectionSbSamples,
                       string.Join("\r\n", SampleInfo),
                       string.Join("\r\n", _unknownSection.Select(k => $"{k.Key}\r\n{k.Value.ToString().TrimEnd('\r', '\n')}"))
                   ) + "\r\n";
        }
    }
}
