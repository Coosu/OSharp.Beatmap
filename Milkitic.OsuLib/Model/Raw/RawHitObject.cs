namespace Milkitic.OsuLib.Model.Raw
{
    public class RawHitObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Offset { get; set; }
        
        // gugu
        public string NotImplementedInfo { get; set; }

        public override string ToString() => $"{X},{Y},{Offset},{NotImplementedInfo}";
    }
}
