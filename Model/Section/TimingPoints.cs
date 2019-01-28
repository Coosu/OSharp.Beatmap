using System;
using System.Collections.Generic;
using System.Linq;
using OSharp.Beatmap.Enums;
using OSharp.Beatmap.Interface;
using OSharp.Beatmap.Model.Raw;

namespace OSharp.Beatmap.Model.Section
{
    public class TimingPoints : ISection
    {
        public List<RawTimingPoint> TimingList { get; set; }
        public double MinTime => TimingList.Count == 0 ? 0 : TimingList.Min(t => t.Offset);
        public double MaxTime => TimingList.Count == 0 ? 0 : TimingList.Max(t => t.Offset);

        public void Match(string line)
        {
            if (TimingList == null)
                TimingList = new List<RawTimingPoint>();

            string[] param = line.Split(',');
            TimingList.Add(new RawTimingPoint
            {
                Offset = double.Parse(param[0]),
                Factor = double.Parse(param[1]),
                Rhythm = int.Parse(param[2]),
                SamplesetEnum = (SamplesetEnum)(int.Parse(param[3]) - 1),
                Track = int.Parse(param[4]),
                Volume = int.Parse(param[5]),
                Inherit = !Convert.ToBoolean(int.Parse(param[6])),
                Kiai = Convert.ToBoolean(int.Parse(param[7])),
                Positive = double.Parse(param[1]) >= 0
            });
        }

        /// <summary>
        /// 获取当前bpm的节奏的间隔
        /// </summary>
        /// <param name="multiple">multiple: 1, 0.5, 1/3d, etc.</param>
        /// <returns></returns>
        public Dictionary<double, double> GetInterval(double multiple)
        {
            return TimingList.Where(t => !t.Inherit).OrderBy(t => t.Offset)
                .ToDictionary(k => k.Offset, v => 60000 / v.Bpm * multiple);
        }

        public double[] GetTimings(double multiple)
        {
            var array = TimingList.Where(t => !t.Inherit).OrderBy(t => t.Offset).ToArray();
            var list = new List<double>();

            for (int i = 0; i < array.Length; i++)
            {
                decimal nextTime = Convert.ToDecimal(i == array.Length - 1 ? MaxTime : array[i + 1].Offset);
                var t = array[i];
                decimal decBpm = Convert.ToDecimal(t.Bpm);
                decimal decMult = Convert.ToDecimal(multiple);
                decimal interval = 60000 / decBpm * decMult;
                decimal current = Convert.ToDecimal(t.Offset);
                while (current < nextTime)
                {
                    list.Add(Convert.ToDouble(current));
                    current += interval;
                }
            }

            return list.ToArray();
        }

        public RawTimingPoint GetRedLine(double offset)
        {
            RawTimingPoint[] points = TimingList.Where(t => !t.Inherit).Where(t => Math.Abs(t.Offset - offset) < 1).ToArray();
            return points.Length == 0 ? TimingList.First(t => !t.Inherit) : points.Last();
        }
        public RawTimingPoint GetLine(double offset)
        {
            var lines = TimingList.Where(t => t.Offset <= offset + 1/*tolerance*/).ToArray();
            if (lines.Length == 0)
                return TimingList.First();
            double timing = lines.Max(t => t.Offset);
            RawTimingPoint[] points = TimingList.Where(t => Math.Abs(t.Offset - timing) < 1).ToArray();
            RawTimingPoint point;
            if (points.Length > 1)
            {
                if (points.Length == 2)
                {
                    if (points[0].Inherit != points[1].Inherit)
                    {
                        point = points.First(t => t.Inherit);
                    }
                    else
                        throw new MultiTimingSectionException("存在同一时刻两条相同类型的Timing Section。");
                }
                else
                    throw new MultiTimingSectionException("存在同一时刻多条Timing Section。");
            }
            else
                point = points[0];

            return point;
        }

        public double[] GetTimingBars()
        {
            var array = TimingList.Where(t => !t.Inherit).OrderBy(t => t.Offset).ToArray();
            var list = new List<double>();

            for (int i = 0; i < array.Length; i++)
            {
                decimal nextTime = Convert.ToDecimal(i == array.Length - 1 ? MaxTime : array[i + 1].Offset);
                var t = array[i];
                decimal decBpm = Convert.ToDecimal(t.Bpm);
                decimal decMult = Convert.ToDecimal(t.Rhythm);
                decimal interval = 60000 / decBpm * decMult;
                decimal current = Convert.ToDecimal(t.Offset);
                while (current < nextTime)
                {
                    list.Add(Convert.ToDouble(current));
                    current += interval;
                }
            }

            return list.ToArray();
        }

        public OsuFile.TimeRange[] GetTimingKiais()
        {
            var array = TimingList;
            var list = new List<OsuFile.TimeRange>();
            double? tmpKiai = null;
            foreach (var t in array)
            {
                if (t.Kiai && tmpKiai == null)
                    tmpKiai = t.Offset;
                else if (!t.Kiai && tmpKiai != null)
                {
                    list.Add(new OsuFile.TimeRange(tmpKiai.Value, t.Offset));
                    tmpKiai = null;
                }
            }
            if (tmpKiai != null)
                list.Add(new OsuFile.TimeRange(tmpKiai.Value, MaxTime));
            return list.ToArray();
        }

        public string ToSerializedString() => "[TimingPoints]\r\n" + string.Join("\r\n", TimingList) + "\r\n\r\n";
    }
}
