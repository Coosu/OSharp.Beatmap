namespace OSharp.Beatmap.Configurable
{
    public interface ISection
    {
        void Match(string line);
        string ToSerializedString();
    }
}
