using System.Collections.Generic;
using System.Linq;
using Milkitic.OsuLib.Interface;

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
    }
}
