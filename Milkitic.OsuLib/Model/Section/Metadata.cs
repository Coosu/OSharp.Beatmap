using Milkitic.OsuLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Milkitic.OsuLib.Model.Section
{
    public class Metadata : KeyValueSection
    {
        public string Title { get; set; }
        public string TitleUnicode { get; set; }
        public string Artist { get; set; }
        public string ArtistUnicode { get; set; }
        public string Creator { get; set; }
        public string Version { get; set; }
        public string Source { get; set; }
        public string Tags
        {
            get => TagList == null ? "" : string.Join(" ", TagList);
            set => TagList = value.Split(' ').ToList();
        }
        [ConfigIgnore]
        public List<string> TagList { get; private set; }
        public int BeatmapID { get; set; }
        public int BeatmapSetID { get; set; }

        public string GetUnicodeTitle()
        {
            return string.IsNullOrEmpty(TitleUnicode)
                ? (string.IsNullOrEmpty(Title) ? "" : Title)
                : TitleUnicode;
        }

        public string GetUnicodeArtist()
        {
            return string.IsNullOrEmpty(ArtistUnicode)
                ? (string.IsNullOrEmpty(Artist) ? "" : Artist)
                : ArtistUnicode;
        }

        public string GetOriginalTitle()
        {
            return string.IsNullOrEmpty(Title)
                ? (string.IsNullOrEmpty(TitleUnicode) ? "" : TitleUnicode)
                : Title;
        }

        public string GetOriginalArtist()
        {
            return string.IsNullOrEmpty(Artist)
                ? (string.IsNullOrEmpty(ArtistUnicode) ? "" : ArtistUnicode)
                : Artist;
        }
    }
}
