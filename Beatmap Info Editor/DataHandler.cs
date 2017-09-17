using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Editor.Object;

namespace Editor
{
    public class DataHandler
    {
        public List<obj_CompareInfo> InfoList { get; set; } = new List<obj_CompareInfo>();

        public void Compare(FileContainer fc)
        {
            var list = fc.FileList;
            bool same;
            if (list.Count <= 1) return;
            //LetterboxInBreaks
            same = true;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[0].General.LetterboxInBreaks!= list[0].General.LetterboxInBreaks)
            }
            if (same) InfoList.Add(new obj_CompareInfo
            {
                Name = "LetterboxInBreaks",
                Same = same
            })
        }
    }
}
