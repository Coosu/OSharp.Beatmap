namespace OSharp.Beatmap.Sections
{
    public interface ISection
    {
        void Match(string line);
        string ToSerializedString();
    }
}
