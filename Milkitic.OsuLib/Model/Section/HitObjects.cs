using Milkitic.OsuLib.Enums;
using Milkitic.OsuLib.Interface;
using Milkitic.OsuLib.Model.Raw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Milkitic.OsuLib.Model.Section
{
    public class HitObjects : ISection
    {
        private readonly TimingPoints _timingPoints;
        private readonly Difficulty _difficulty;
        private readonly General _general;
        public List<RawHitObject> HitObjectList { get; set; }

        public double MinTime => HitObjectList.Count == 0 ? 0 : HitObjectList.Min(t => t.Offset);
        public double MaxTime => HitObjectList.Count == 0 ? 0 : HitObjectList.Max(t => t.Offset);

        public HitObjects(OsuFile osuFile)
        {
            _timingPoints = osuFile.TimingPoints;
            _difficulty = osuFile.Difficulty;
            _general = osuFile.General;
        }

        public void Match(string line)
        {
            if (HitObjectList == null)
                HitObjectList = new List<RawHitObject>();

            string[] param = line.Split(',');

            var x = int.Parse(param[0]);
            var y = int.Parse(param[1]);
            var offset = int.Parse(param[2]);
            var type = (RawObjectType)Enum.Parse(typeof(RawObjectType), param[3]);
            var hitsound = (HitsoundType)Enum.Parse(typeof(HitsoundType), param[4]);

            var notImplementedInfo = string.Join(",", param.Skip(5));
            var hitObject = new RawHitObject
            {
                X = x,
                Y = y,
                Offset = offset,
                RawType = type,
                Hitsound = hitsound
            };

            if ((type & RawObjectType.Circle) == RawObjectType.Circle)
            {
                ConvertToCircle(hitObject, notImplementedInfo);
            }
            else if ((type & RawObjectType.Slider) == RawObjectType.Slider)
            {
                ConvertToSlider(hitObject, notImplementedInfo);
            }
            else if ((type & RawObjectType.Spinner) == RawObjectType.Spinner)
            {
                ConvertToSpinner(hitObject, notImplementedInfo);
            }
            else if ((type & RawObjectType.Hold) == RawObjectType.Hold)
            {
                ConvertToHold(hitObject, notImplementedInfo);
            }

            HitObjectList.Add(hitObject);
        }

        private void ConvertToCircle(RawHitObject hitObject, string notImplementedInfo)
        {
            bool isSupportExtra = notImplementedInfo.IndexOf(":", StringComparison.Ordinal) != -1;
            hitObject.Extras = isSupportExtra ? notImplementedInfo : null;
        }

        private void ConvertToSlider(RawHitObject hitObject, string notImplementedInfo)
        {
            string extra = notImplementedInfo.Split(',').Last();
            bool isSupportExtra = extra.IndexOf(":", StringComparison.Ordinal) != -1;
            var infos = notImplementedInfo.Split(',');

            var sliderType = infos[0].Split('|')[0];
            var curvePoints = infos[0].Split('|').Skip(1).ToArray();
            Point[] points = new Point[curvePoints.Length];
            for (var i = 0; i < curvePoints.Length; i++)
            {
                var point = curvePoints[i];
                var xy = point.Split(':').Select(int.Parse).ToArray();
                points[i] = new Point(xy[0], xy[1]);
            }

            int repeat = int.Parse(infos[1]);
            decimal pixelLength = decimal.Parse(infos[2]);

            HitsoundType[] edgeHitsounds;
            SampleAdditonEnum[] edgeSamples;
            SampleAdditonEnum[] edgeAdditions;
            if (infos.Length == 3)
            {
                edgeHitsounds = null;
                edgeSamples = null;
                edgeAdditions = null;
            }
            else if (infos.Length == 4)
            {
                edgeHitsounds = infos[3].Split('|').Select(t => t.ParseToEnum<HitsoundType>()).ToArray();
                edgeSamples = null;
                edgeAdditions = null;
            }
            else
            {
                edgeHitsounds = infos[3].Split('|').Select(t => t.ParseToEnum<HitsoundType>()).ToArray();
                string[] edgeAdditionsStr = infos[4].Split('|');
                edgeSamples = new SampleAdditonEnum[repeat + 1];
                edgeAdditions = new SampleAdditonEnum[repeat + 1];
                for (int i = 0; i < edgeAdditionsStr.Length; i++)
                {
                    var sampAdd = edgeAdditionsStr[i].Split(':');
                    edgeSamples[i] = sampAdd[0].ParseToEnum<SampleAdditonEnum>();
                    edgeAdditions[i] = sampAdd[1].ParseToEnum<SampleAdditonEnum>();
                }
            }
            double lastRedLineOffset = _timingPoints.TimingList.Where(t => !t.Inherit).Where(t => t.Offset <= hitObject.Offset)
                .Max(t => t.Offset);
            var lastRedLine = _timingPoints.TimingList.First(t => t.Offset == lastRedLineOffset && !t.Inherit);

            double lastLineOffset = _timingPoints.TimingList.Where(t => t.Offset <= hitObject.Offset)
                .Max(t => t.Offset);
            var lastLines = _timingPoints.TimingList.Where(t => t.Offset == lastLineOffset).ToArray();

            RawTimingPoint lastLine;
            if (lastLines.Length > 1)
            {
                if (lastLines.Length == 2)
                {
                    if (lastLines[0].Inherit != lastLines[1].Inherit)
                    {
                        lastLine = lastLines.First(t => t.Inherit);
                    }
                    else
                        throw new FormatException("Bad osu format.");
                }
                else
                    throw new FormatException("Bad osu format.");
            }
            else
                lastLine = lastLines[0];
            hitObject.SliderInfo = new SliderInfo(hitObject.Offset, lastRedLine.Factor, _difficulty.SliderMultiplier * lastLine.Multiple)
            {
                CurvePoints = points,
                EdgeAdditions = edgeAdditions,
                EdgeHitsounds = edgeHitsounds,
                EdgeSamples = edgeSamples,
                PixelLength = pixelLength,
                Repeat = repeat,
                SliderType = sliderType.ParseToEnum<SliderTypeEnum>()
            };

            hitObject.Extras = isSupportExtra ? extra : null;
        }

        private void ConvertToSpinner(RawHitObject hitObject, string notImplementedInfo)
        {
            //throw new NotImplementedException();
        }

        private void ConvertToHold(RawHitObject hitObject, string notImplementedInfo)
        {
            //throw new NotImplementedException();
        }


        public string ToSerializedString() => "[HitObjects]\r\n" + string.Join("\r\n", HitObjectList) + "\r\n";
    }
}
