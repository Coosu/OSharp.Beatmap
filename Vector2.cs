namespace OSharp.Beatmap
{
    public struct Vector2
    {
        public Vector2(float x, float y) : this()
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public override string ToString()
        {
            return X + "," + Y;
        }
    }
}