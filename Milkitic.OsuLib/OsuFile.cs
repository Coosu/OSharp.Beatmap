using Milkitic.OsuLib.Interface;
using Milkitic.OsuLib.Model.Section;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Milkitic.OsuLib
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
                        var type = t.First(p => p.Name == sectionName).PropertyType;
                        object[] args = { };
                        if (type == typeof(HitObjects)) args = new object[] { this };
                        currentSecion = Activator.CreateInstance(type, args) as ISection;
                    }
                    else
                        throw new BadOsuFormatException("存在未知的节点: " + sectionName);
                }
                else
                {
                    switch (currentSecion)
                    {
                        case null when line.StartsWith(verFlag):
                            var str = line.Replace(verFlag, "");
                            if (!int.TryParse(str, out var verNum))
                                throw new BadOsuFormatException("未知的osu版本: " + str);
                            if (verNum < 7)
                                throw new VersionNotSupportedException(verNum);
                            Version = verNum;
                            break;
                        case null:
                            throw new BadOsuFormatException("存在问题头声明: " + line);
                        default:
                            currentSecion.Match(line);
                            break;
                    }
                }
            }

            if (currentSecion != null)
                t.First(p => p.Name == currentSecion.GetType().Name).SetValue(this, currentSecion);
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
        public string OsbFileName => Escape(string.Format("{0} - {1} ({2}).osb", Metadata.Artist, Metadata.Title,
            Metadata.Creator));

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
