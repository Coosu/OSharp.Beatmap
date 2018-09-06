using System.Collections.Generic;
using System.IO;
using Milkitic.OsuLib.Model;

namespace Milkitic.OsuLib
{
    public class OsuFileManager
    {
        public List<OsuFile> FileList { get; } = new List<OsuFile>();

        public OsuFileManager()
        {

        }

        public OsuFileManager(string directoryPath)
        {
            LoadFromDirectory(directoryPath);
        }

        public void LoadFromDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.osu");
            foreach (var file in files)
                FileList.Add(new OsuFile(file.FullName));
        }

        public void LoadFromFile(string path) => FileList.Add(new OsuFile(path));
    }
}
