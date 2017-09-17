using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Editor.Object;

namespace Editor
{
    public class FileContainer
    {
        public List<OsuFile> FileList { get; set; }

        public FileContainer(string directoryRoot)
        {
            FileList = new List<OsuFile>();
            DirectoryInfo di = new DirectoryInfo(directoryRoot);
            FileInfo[] files = di.GetFiles("*.osu");
            foreach (var file in files)
            {
                FileList.Add(OsuFileReader.ReadFromFile(file.FullName));
            }
        }
    }
}
