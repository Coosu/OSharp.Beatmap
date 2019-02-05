using OSharp.Beatmap.Configurable;
using System.Collections.Generic;
using System.Linq;

namespace OSharp.Beatmap.Sections
{
    [SectionProperty("Metadata")]
    public class Metadata : KeyValueSection
    {
        [SectionProperty("Title")]         public string Title { get; set; }
        [SectionProperty("TitleUnicode")]  public string TitleUnicode { get; set; }
        [SectionProperty("Artist")]        public string Artist { get; set; }
        [SectionProperty("ArtistUnicode")] public string ArtistUnicode { get; set; }
        [SectionProperty("Creator")]       public string Creator { get; set; }
        [SectionProperty("Version")]       public string Version { get; set; }
        [SectionProperty("Source")]        public string Source { get; set; }

        [SectionProperty("Tags")]
        public string Tags
        {
            get => TagList == null ? "" : string.Join(" ", TagList);
            set => TagList = value.Split(' ').ToList();
        }

        [SectionIgnore]                    public List<string> TagList { get; private set; }
        [SectionProperty("BeatmapID")]     public int BeatmapID { get; set; }
        [SectionProperty("BeatmapSetID")]  public int BeatmapSetID { get; set; }
    }
}
