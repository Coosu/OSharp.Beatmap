using System.Collections.Generic;
using System.IO;

namespace OSharp.Beatmap.Management
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
                FileList.Add(OsuFile.ReadFromFile(file.FullName));
        }

        public void LoadFromFile(string path) => FileList.Add(OsuFile.ReadFromFile(path));
    }
}
