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
                if (BookmarkList == null) return null;
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

        public override string ToString()
        {
            var list = GetType().GetProperties();

            sb.Clear();
            sb.AppendLine("[Editor]");
            for (int i = 0; i < list.Length - 1; i++)
            {
                if (list[i].PropertyType == typeof(bool))
                    sb.AppendLine(list[i].Name + ": " + Convert.ToInt32(list[i].GetValue(this)));
                else if (list[i].Name == "Bookmarks" && list[i].GetValue(this) == null)
                    continue;
                else if (list[i].Name == "BookmarkList")
                    continue;
                else
                    sb.AppendLine(list[i].Name + ": " + list[i].GetValue(this));
            }
            sb.Append(TheRestText.ToString());
            return sb.ToString();
        }
    }
}
