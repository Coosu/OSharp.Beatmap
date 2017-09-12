using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_Editor
    {
        private StringBuilder sb = new StringBuilder();
        public string Bookmarks
        {
            get
            {
                sb.Clear();
                for (int i = 0; i < BookmarkList.Count; i++)
                {
                    if (i != BookmarkList.Count - 1) sb.Append(BookmarkList[i] + ",");
                    else sb.Append(BookmarkList[i]);
                }
                return sb.ToString();
            }
            set
            {
                bookmarkList = (Array.ConvertAll(
                    value.Split(','), new Converter<string, int>(int.Parse)))
                    .ToList();
            }
        }
        public List<int> BookmarkList { get => bookmarkList; }
        public double DistanceSpacing { get; set; }
        public int BeatDivisor { get; set; }
        public int GridSize { get; set; }
        public double TimelineZoom { get; set; }
        public string TheRestText { get; set; }
        private List<int> bookmarkList;
    }
}
