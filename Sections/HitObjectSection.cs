using OSharp.Beatmap.Configurable;
using OSharp.Beatmap.Internal;
using OSharp.Beatmap.Sections.HitObject;
using OSharp.Beatmap.Sections.Timing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace OSharp.Beatmap.Sections
{
    [SectionProperty("HitObjects")]
    public class HitObjectSection : Section
    {
        private readonly TimingSection _timingPoints;
        private readonly DifficultySection _difficulty;
        private readonly GeneralSection _general;
        public List<RawHitObject> HitObjectList { get; set; } = new List<RawHitObject>();

        public double MinTime => HitObjectList.Count == 0 ? 0 : HitObjectList.Min(t => t.Offset);
        public double MaxTime => HitObjectList.Count == 0 ? 0 : HitObjectList.Max(t => t.Offset);

        public HitObjectSection(OsuFile osuFile)
        {
            _timingPoints = osuFile.TimingPoints;
            _difficulty = osuFile.Difficulty;
            _general = osuFile.General;
        }

        public override void Match(string line)
        {
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

            if (type.HasFlag(RawObjectType.Circle))
            {
                ConvertToCircle(hitObject, notImplementedInfo);
            }
            else if (type.HasFlag(RawObjectType.Slider))
            {
                ConvertToSlider(hitObject, notImplementedInfo);
            }
            else if (type.HasFlag(RawObjectType.Spinner))
            {
                ConvertToSpinner(hitObject, notImplementedInfo);
            }
            else if (type.HasFlag(RawObjectType.Hold))
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
            ObjectSamplesetType[] edgeSamples;
            ObjectSamplesetType[] edgeAdditions;
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
                edgeSamples = new ObjectSamplesetType[repeat + 1];
                edgeAdditions = new ObjectSamplesetType[repeat + 1];
                for (int i = 0; i < edgeAdditionsStr.Length; i++)
                {
                    var sampAdd = edgeAdditionsStr[i].Split(':');
                    edgeSamples[i] = sampAdd[0].ParseToEnum<ObjectSamplesetType>();
                    edgeAdditions[i] = sampAdd[1].ParseToEnum<ObjectSamplesetType>();
                }
            }

            TimingPoint[] lastRedLinesIfExsist = _timingPoints.TimingList.Where(t => !t.Inherit)
                .Where(t => t.Offset <= hitObject.Offset).ToArray();
            TimingPoint lastRedLine;

            // hitobjects before lines is allowed
            if (lastRedLinesIfExsist.Length == 0)
                lastRedLine = _timingPoints.TimingList.First(t => !t.Inherit);
            else
            {
                double lastRedLineOffset = lastRedLinesIfExsist.Max(t => t.Offset);
                lastRedLine = _timingPoints.TimingList.First(t => t.Offset == lastRedLineOffset && !t.Inherit);
            }

            TimingPoint[] lastLinesIfExist = _timingPoints.TimingList.Where(t => t.Offset <= hitObject.Offset).ToArray();
            TimingPoint[] lastLines; // 1 red + 1 green is allowed
            TimingPoint lastLine;

            // hitobjects before lines is allowed
            if (lastLinesIfExist.Length == 0)
                lastLines = new[] { _timingPoints.TimingList.First(t => !t.Inherit) };
            else
            {
                double lastLineOffset = lastLinesIfExist.Max(t => t.Offset);
                // 1 red + 1 green is allowed, so maybe here are two results
                lastLines = _timingPoints.TimingList.Where(t => t.Offset == lastLineOffset).ToArray();
            }

            if (lastLines.Length > 1)
            {
                if (lastLines.Length == 2)
                {
                    if (lastLines[0].Inherit != lastLines[1].Inherit)
                    {
                        lastLine = lastLines.First(t => t.Inherit);
                    }
                    else
                        throw new RepeatTimingSectionException("存在同一时刻两条相同类型的Timing Section。");
                }
                else
                    throw new RepeatTimingSectionException("存在同一时刻多条Timing Section。");
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
                SliderType = sliderType.ParseToEnum<SliderType>()
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

        public override void AppendSerializedString(TextWriter textWriter)
        {
            textWriter.WriteLine($"[{SectionName}]");
            foreach (var hitObject in HitObjectList)
            {
                hitObject.AppendSerializedString(textWriter);
            }
        }
    }
}
