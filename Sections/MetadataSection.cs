using OSharp.Beatmap.Configurable;
using System.Collections.Generic;
using System.Linq;

namespace OSharp.Beatmap.Sections
{
    [SectionProperty("Metadata")]
    public class MetadataSection : KeyValueSection
    {
        [SectionProperty("Title")]         public string Title { get; set; }
        [SectionProperty("TitleUnicode")]  public string TitleUnicode { get; set; }
        [SectionProperty("Artist")]        public string Artist { get; set; }
        [SectionProperty("ArtistUnicode")] public string ArtistUnicode { get; set; }
        [SectionProperty("Creator")]       public string Creator { get; set; }
        [SectionProperty("Version")]       public string Version { get; set; }
        [SectionProperty("Source")]        public string Source { get; set; }

        [SectionProperty("Tags")]
        [SectionConverter(typeof(SplitConverter), " ")]
        public List<string> TagList { get;  set; }

        [SectionProperty("BeatmapID")]     public int BeatmapId { get; set; }
        [SectionProperty("BeatmapSetID")]  public int BeatmapSetId { get; set; }

        [SectionIgnore]
        public MetaString TitleMeta => new MetaString(Title, TitleUnicode);
        [SectionIgnore]
        public MetaString ArtistMeta => new MetaString(Artist, ArtistUnicode);
    }
}
