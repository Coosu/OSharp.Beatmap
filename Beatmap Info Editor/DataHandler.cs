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
            obj_CompareInfo oci = new obj_CompareInfo();
            var list = fc.FileList;
            bool same, flag2;
            if (list.Count <= 1) return;
            //LetterboxInBreaks
            same = true;
            flag2 = false;
            oci.Name = "LetterboxInBreaks";
            for (int i = 1; i < list.Count; i++)
            {
                if (list[0].General.LetterboxInBreaks != list[i].General.LetterboxInBreaks && !flag2)
                {
                    flag2 = true;
                    i = -1;
                    continue;
                }
                if (flag2)
                {
                    bool flag = false;
                    int j;
                    for (j = 0; j < oci.DifferentInfo.Count; j++)
                    {
                        if (oci.DifferentInfo[j].Information == list[i].General.LetterboxInBreaks.ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == true)
                    {
                        oci.DifferentInfo[j].Difficulty += "||" + list[i].Metadata.Version;
                    }
                    else
                    {
                        oci.DifferentInfo.Add(new obj_DifferentInfo
                        {
                            Information = list[i].General.LetterboxInBreaks.ToString(),
                            Difficulty = list[i].Metadata.Version
                        });
                    }
                    same = false;
                }
            }
            oci.Same = same;
        }
    }
}
