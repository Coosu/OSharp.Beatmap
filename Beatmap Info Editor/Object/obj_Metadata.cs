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
                sb.Clear();
                for (int i = 0; i < TagList.Count; i++)
                {
                    if (i != TagList.Count - 1) sb.Append(TagList[i] + " ");
                    else sb.Append(TagList[i]);
                }
                return sb.ToString();
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

    }
}
