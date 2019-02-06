using System.Drawing;
using System.Linq;

namespace OSharp.Beatmap.Sections.HitObject
{
    public class SliderInfo
    {
        private readonly int _offset;
        private readonly double _beatDuration;
        private readonly double _sliderMultiplier;

        public SliderType SliderType { get; set; }
        public Point[] CurvePoints { get; set; }
        public int Repeat { get; set; }
        public decimal PixelLength { get; set; }
        public HitsoundType[] EdgeHitsounds { get; set; }
        public ObjectSamplesetType[] EdgeSamples { get; set; }
        public ObjectSamplesetType[] EdgeAdditions { get; set; }

        //extension
        public Point StartPoint => CurvePoints.First();
        public Point EndPoint => CurvePoints.Last();

        public SliderInfo(int offset, double beatDuration, double sliderMultiplier)
        {
            _offset = offset;
            _beatDuration = beatDuration;
            _sliderMultiplier = sliderMultiplier;
        }

        public SliderEdge[] Edges
        {
            get
            {
                SliderEdge[] edges = new SliderEdge[Repeat + 1];

                for (var i = 0; i < edges.Length; i++)
                {
                    var time = PixelLength / (100 * (decimal)_sliderMultiplier) * (decimal)_beatDuration;
                    edges[i] = new SliderEdge
                    {
                        Offset = (double)(_offset + time * i),
                        Point = i % 2 == 0 ? StartPoint : EndPoint,
                        EdgeHitsound = EdgeHitsounds?[i] ?? HitsoundType.Normal,
                        EdgeSample = EdgeSamples?[i] ?? ObjectSamplesetType.Auto,
                        EdgeAddition = EdgeAdditions?[i] ?? ObjectSamplesetType.Auto
                    };
                }

                return edges;
            }
        }
    }

    public struct SliderEdge
    {
        public double Offset { get; set; }
        public Point Point { get; set; }
        public HitsoundType EdgeHitsound { get; set; }
        public ObjectSamplesetType EdgeSample { get; set; }
        public ObjectSamplesetType EdgeAddition { get; set; }
    }
}
