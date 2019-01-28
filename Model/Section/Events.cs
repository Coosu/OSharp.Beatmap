using OSharp.Beatmap.Interface;
using OSharp.Storyboard.Management;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OSharp.Beatmap.Model.Section
{
    public class Events : ISection
    {
        public BackgroundInfo BackgroundInfo { get; set; }
        public VideoInfo VideoInfo { get; set; }
        public List<SbSampleInfo> SampleInfo { get; set; } = new List<SbSampleInfo>();
        public List<OsuFile.TimeRange> Breaks { get; set; } = new List<OsuFile.TimeRange>();
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
                        ElementGroup = ElementGroup.Parse(_sbInfo.ToString().Trim('\r', '\n'), 0);
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
                            VideoInfo = new VideoInfo { Offset = double.Parse(infos[1]), Filename = infos[2].Trim('"') };
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

                            BackgroundInfo = new BackgroundInfo
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
                            Breaks.Add(new OsuFile.TimeRange(double.Parse(infos[1]), double.Parse(infos[2])));
                        }
                        break;
                    case SectionSbSamples:
                        if (line.StartsWith("Sample,"))
                        {
                            var infos = line.Split(',');
                            SampleInfo.Add(new SbSampleInfo
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
