namespace OSharp.Beatmap.Interface
{
    public interface ISection
    {
        void Match(string line);
        string ToSerializedString();
    }
}
