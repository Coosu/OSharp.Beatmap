using OSharp.Beatmap.Configurable;
using System.Collections.Generic;
using System.Linq;

namespace OSharp.Beatmap.Sections
{
    [SectionProperty("Editor")]
    public class Editor : KeyValueSection
    {
        [SectionProperty("Bookmarks")]
        public string Bookmarks
        {
            get => BookmarkList == null ? "" : string.Join(",", BookmarkList);
            set => BookmarkList = value.Split(',').Select(int.Parse).ToList();
        }

        [SectionIgnore]                      public List<int> BookmarkList { get; private set; }
        [SectionProperty("DistanceSpacing")] public double DistanceSpacing { get; set; } = 1;
        [SectionProperty("BeatDivisor")]     public int BeatDivisor { get; set; } = 4;
        [SectionProperty("GridSize")]        public int GridSize { get; set; } = 4;
        [SectionProperty("TimelineZoom")]    public double TimelineZoom { get; set; } = 1;
    }
}
