using OSharp.Beatmap.Configurable;
using OSharp.Beatmap.Sections;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OSharp.Beatmap
{
    public class OsuFile : Config
    {
        public int Version { get; set; }
        public GeneralSection General { get; set; }
        public EditorSection Editor { get; set; }
        public MetadataSection Metadata { get; set; }
        public DifficultySection Difficulty { get; set; }
        public EventSection Events { get; set; }
        public TimingSection TimingPoints { get; set; }
        public ColorSection Colours { get; set; }
        public HitObjectSection HitObjects { get; set; }

        public static async Task<OsuFile> ReadFromFileAsync(string path)
        {
            return await Task.Run(() =>
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return ConfigConvert.DeserializeObject<OsuFile>(sr);
                }
            });
        }

        public override string ToString() => Path;

        //todo: not optimized
        public void WriteOsuFile(string path, string newDiffName = null)
        {
            File.WriteAllText(path,
                string.Format("osu file format v{0}\r\n\r\n{1}{2}{3}{4}{5}{6}{7}{8}", Version,
                    General?.ToSerializedString(),
                    Editor?.ToSerializedString(),
                    Metadata?.ToSerializedString(newDiffName),
                    Difficulty?.ToSerializedString(),
                    Events?.ToSerializedString(),
                    TimingPoints?.ToSerializedString(),
                    Colours?.ToSerializedString(),
                    HitObjects?.ToSerializedString()));
        }

        internal override void HandleCustom(string line)
        {
            const string verFlag = "osu file format v";

            if (line.StartsWith(verFlag))
            {
                var str = line.Replace(verFlag, "");
                if (!int.TryParse(str, out var verNum))
                    throw new BadOsuFormatException("未知的osu版本: " + str);
                if (verNum < 7)
                    throw new VersionNotSupportedException(verNum);
                Version = verNum;
            }
            else
            {
                throw new BadOsuFormatException("存在问题头声明: " + line);
            }
        }

        private OsuFile() { }

        private string Path => Common.IO.File.EscapeFileName(string.Format("{0} - {1} ({2}){3}.osu",
            Metadata.Artist,
            Metadata.Title,
            Metadata.Creator,
            Metadata.Version != "" ? $" [{Metadata.Version}]" : ""));

        public string GetPath(string newDiffName)
        {
            return Common.IO.File.EscapeFileName(string.Format("{0} - {1} ({2}){3}.osu",
                Metadata.Artist,
                Metadata.Title,
                Metadata.Creator,
                Metadata.Version != "" ? $" [{newDiffName}]" : ""));
        }

    }
}
