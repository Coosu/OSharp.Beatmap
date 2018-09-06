using Milkitic.OsuLib.Interface;
using Milkitic.OsuLib.Model.Raw;
using System.Collections.Generic;
using System.Linq;

namespace Milkitic.OsuLib.Model.Section
{
    public class HitObjects : ISection
    {
        public List<RawHitObject> HitObjectList { get; set; }

        public double MinTime => HitObjectList.Count == 0 ? 0 : HitObjectList.Min(t => t.Offset);
        public double MaxTime => HitObjectList.Count == 0 ? 0 : HitObjectList.Max(t => t.Offset);

        public void Match(string line)
        {
            if (HitObjectList == null)
                HitObjectList = new List<RawHitObject>();

            string[] param = line.Split(',');
            HitObjectList.Add(new RawHitObject
            {
                X = int.Parse(param[0]),
                Y = int.Parse(param[1]),
                Offset = int.Parse(param[2]),
                NotImplementedInfo = string.Join(",", param.Skip(3))
            });
        }

        public string ToSerializedString() => "[HitObjects]\r\n" + string.Join("\r\n", HitObjectList) + "\r\n";
    }
}
