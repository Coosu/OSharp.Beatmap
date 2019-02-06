using System.IO;

namespace OSharp.Beatmap.Configurable
{
    public interface ISection : ISerializeWritable
    {
        void Match(string line);

    }
}
