namespace OSharp.Beatmap.Stream
{
    public struct Vector2
    {
        public Vector2(float x, float y) : this()
        {
            X = x;
            Y = y;
        }

        public float X { get; }
        public float Y { get; }
    }
}