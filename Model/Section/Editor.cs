using System.Collections.Generic;
using System.Linq;
using OSharp.Beatmap.Interface;

namespace OSharp.Beatmap.Model.Section
{
    public class Editor : KeyValueSection
    {
        public string Bookmarks
        {
            get => BookmarkList == null ? "" : string.Join(",", BookmarkList);
            set => BookmarkList = value.Split(',').Select(int.Parse).ToList();
        }
        [ConfigIgnore]
        public List<int> BookmarkList { get; private set; }
        public double DistanceSpacing { get; set; } = 1;
        public int BeatDivisor { get; set; } = 4;
        public int GridSize { get; set; } = 4;
        public double TimelineZoom { get; set; } = 1;
    }
}
