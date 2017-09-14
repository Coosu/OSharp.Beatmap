using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class obj_Metadata
    {
        private StringBuilder sb = new StringBuilder();
        private StringBuilder sb2 = new StringBuilder();

        public string Title { get; set; }
        public string TitleUnicode { get; set; }
        public string Artist { get; set; }
        public string ArtistUnicode { get; set; }
        public string Creator { get; set; }
        public string Version { get; set; }
        public string Source { get; set; }
        public string Tags
        {
            get
            {
                if (tagList == null) return "";
                sb2.Clear();
                for (int i = 0; i < TagList.Count; i++)
                {
                    if (i != TagList.Count - 1) sb2.Append(TagList[i] + " ");
                    else sb2.Append(TagList[i]);
                }
                return sb2.ToString();
            }
            set
            {
                tagList = value.Split(' ').ToList();
            }
        }
        public List<string> TagList { get => tagList; }
        public int BeatmapID { get; set; }
        public int BeatmapSetID { get; set; }
        public string TheRestText { get; set; }

        private List<string> tagList;

        public override string ToString()
        {
            var list = GetType().GetProperties();

            sb.Clear();
            sb.AppendLine("[Metadata]");
            for (int i = 0; i < list.Length - 1; i++)
            {
                if (list[i].PropertyType == typeof(bool))
                    sb.AppendLine(list[i].Name + ":" + Convert.ToInt32(list[i].GetValue(this)));
                else if (list[i].Name == "TagList")
                    continue;
                else
                    sb.AppendLine(list[i].Name + ":" + list[i].GetValue(this));
            }
            sb.Append(TheRestText.ToString());
            return sb.ToString();
        }
    }
}
