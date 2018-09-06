using Milkitic.OsuLib.Enums;
using Milkitic.OsuLib.Interface;
using Milkitic.OsuLib.Model.Raw;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Milkitic.OsuLib.Model.Section
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
                SampleAdditonEnum = (SampleAdditonEnum)(int.Parse(param[3]) - 1),
                Track = int.Parse(param[4]),
                Volume = int.Parse(param[5]),
                Inherit = !Convert.ToBoolean(int.Parse(param[6])),
                Kiai = Convert.ToBoolean(int.Parse(param[7])),
                Positive = double.Parse(param[1]) >= 0
            });
        }

        public string ToSerializedString() => "[TimingPoints]\r\n" + string.Join("\r\n", TimingList) + "\r\n\r\n";
    }
}
