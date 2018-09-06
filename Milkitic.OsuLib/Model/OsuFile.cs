using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Milkitic.OsuLib.Interface;
using Milkitic.OsuLib.Model.Section;

namespace Milkitic.OsuLib.Model
{
    public class OsuFile
    {
        public int Version { get; set; }
        public General General { get; set; }
        public Editor Editor { get; set; }
        public Metadata Metadata { get; set; }
        public Difficulty Difficulty { get; set; }
        public Events Events { get; set; }
        public TimingPoints TimingPoints { get; set; }
        public Colours Colours { get; set; }
        public HitObjects HitObjects { get; set; }

        public double MinTime => Math.Min(HitObjects.MinTime, TimingPoints.MinTime);
        public double MaxTime => Math.Max(HitObjects.MaxTime, TimingPoints.MaxTime);

        public OsuFile(string filePath)
        {
            const string verFlag = "osu file format v";
            string[] lines = File.ReadAllLines(filePath).Where(l => l.Trim() != "").ToArray();
            PropertyInfo[] t = GetType().GetProperties().Where(p =>
                p.PropertyType.GetInterfaces().Contains(typeof(ISection)) ||
                p.PropertyType.BaseType == typeof(KeyValueSection)).ToArray();
            ISection currentSecion = null;
            foreach (var line in lines)
            {
                if (MatchedSection(line, out var sectionName))
                {
                    if (currentSecion != null)
                        t.First(p => p.Name == currentSecion.GetType().Name).SetValue(this, currentSecion);

                    if (t.Select(p => p.Name).Contains(sectionName))
                    {
                        currentSecion = Activator.CreateInstance(t.First(p => p.Name == sectionName).PropertyType) as ISection;
                    }
                    else
                        throw new NullReferenceException("Unknown section: " + sectionName);
                }
                else
                {
                    switch (currentSecion)
                    {
                        case null when line.StartsWith(verFlag):
                            var str = line.Replace(verFlag, "");
                            if (!int.TryParse(str, out var num))
                                throw new FormatException("Unknown osu file version: " + str);
                            if (num < 7)
                                throw new NotSupportedException("尚不支持 " + verFlag + num);
                            Version = num;
                            break;
                        case null:
                            throw new NullReferenceException("Line is not in any sections: " + line);
                        default:
                            currentSecion.Match(line);
                            break;
                    }
                }
            }

            if (currentSecion != null)
                t.First(p => p.Name == currentSecion.GetType().Name).SetValue(this, currentSecion);
        }

        /// <summary>
        /// 获取当前bpm的节奏的间隔
        /// </summary>
        /// <param name="multiple">multiple: 1, 0.5, 1/3d, etc.</param>
        /// <returns></returns>
        public Dictionary<double, double> GetInterval(double multiple)
        {
            return TimingPoints.TimingList.Where(t => !t.Inherit).OrderBy(t => t.Offset)
                .ToDictionary(k => k.Offset, v => 60000 / v.Bpm * multiple);
        }

        public double[] GetTimings(double multiple)
        {
            var array = TimingPoints.TimingList.Where(t => !t.Inherit).OrderBy(t => t.Offset).ToArray();
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

        public double[] GetTimingBars()
        {
            var array = TimingPoints.TimingList.Where(t => !t.Inherit).OrderBy(t => t.Offset).ToArray();
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

        public TimeRange[] GetTimingKiais()
        {
            var array = TimingPoints.TimingList;
            var list = new List<TimeRange>();
            double? tmpKiai = null;
            foreach (var t in array)
            {
                if (t.Kiai && tmpKiai == null)
                    tmpKiai = t.Offset;
                else if (!t.Kiai && tmpKiai != null)
                {
                    list.Add(new TimeRange(tmpKiai.Value, t.Offset));
                    tmpKiai = null;
                }
            }
            if (tmpKiai != null)
                list.Add(new TimeRange(tmpKiai.Value, MaxTime));
            return list.ToArray();
        }

        public struct TimeRange
        {
            public double StartTime { get; }
            public double EndTime { get; }

            public TimeRange(double startTime, double endTime)
            {
                StartTime = startTime;
                EndTime = endTime;
            }
        }

        private static bool MatchedSection(string line, out string sectionName)
        {
            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                sectionName = line.Substring(1, line.Length - 2);
                return true;
            }

            sectionName = null;
            return false;
        }

        public string FileName => Escape(string.Format("{0} - {1} ({2}){3}.osu", Metadata.Artist, Metadata.Title,
            Metadata.Creator, Metadata.Version != "" ? " [" + Metadata.Version + "]" : ""));

        private static string Escape(string source)
        {
            return source.Replace(@"\", "").Replace(@"/", "").Replace(@":", "").Replace(@"*", "").Replace(@"?", "")
                .Replace("\"", "").Replace(@"<", "").Replace(@">", "").Replace(@"|", "");
        }

        public override string ToString()
        {
            return Metadata.Version;
        }

        //todo: not optimized
        public void GenerateFile(string path)
        {
            File.WriteAllText(path,
                string.Format("osu file format v{0}\r\n\r\n{1}{2}{3}{4}{5}{6}{7}{8}", Version,
                    General?.ToSerializedString(), Editor?.ToSerializedString(), Metadata?.ToSerializedString(),
                    Difficulty?.ToSerializedString(), Events?.ToSerializedString(), TimingPoints?.ToSerializedString(),
                    Colours?.ToSerializedString(), HitObjects?.ToSerializedString()));
        }
    }
}
