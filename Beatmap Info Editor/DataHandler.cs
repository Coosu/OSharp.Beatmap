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
            if (list.Count <= 1) return;
            Comp_Letter(list);
            Comp_Tag(list);
            // 先鸽
        }

        private void Comp_Letter(List<OsuFile> list)
        {
            //LetterboxInBreaks
            obj_CompareInfo oci = new obj_CompareInfo();
            bool same = true, flag2 = false;
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
                        oci.DifferentInfo[j].Difficulty.Add(list[i].Metadata.Version);
                    }
                    else
                    {
                        var od = new obj_DifferentInfo
                        {
                            Information = list[i].General.LetterboxInBreaks.ToString()
                        };
                        od.Difficulty.Add(list[i].Metadata.Version);
                        oci.DifferentInfo.Add(od);
                    }
                    same = false;
                }
            }
            oci.Same = same;
            InfoList.Add(oci);
        }
        private void Comp_Tag(List<OsuFile> list)
        {
            //LetterboxInBreaks
            obj_CompareInfo oci = new obj_CompareInfo();
            bool same = true, flag2 = false;
            oci.Name = "Tags";
            for (int i = 1; i < list.Count; i++)
            {
                if (list[0].Metadata.Tags != list[i].Metadata.Tags && !flag2)
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
                        if (oci.DifferentInfo[j].Information == list[i].Metadata.Tags)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == true)
                    {
                        oci.DifferentInfo[j].Difficulty.Add(list[i].Metadata.Version);
                    }
                    else
                    {
                        var od = new obj_DifferentInfo
                        {
                            Information = list[i].Metadata.Tags
                        };
                        od.Difficulty.Add(list[i].Metadata.Version);
                        oci.DifferentInfo.Add(od);
                    }
                    same = false;
                }
            }
            oci.Same = same;
            InfoList.Add(oci);
        }
    }
}
