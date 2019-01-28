using System.Drawing;
using System.Linq;
using OSharp.Beatmap.Enums;

namespace OSharp.Beatmap.Model
{
    public class SliderInfo
    {
        private readonly int _offset;
        private readonly double _beatDuration;
        private readonly double _sliderMultiplier;

        public SliderTypeEnum SliderType { get; set; }
        public Point[] CurvePoints { get; set; }
        public int Repeat { get; set; }
        public decimal PixelLength { get; set; }
        public HitsoundType[] EdgeHitsounds { get; set; }
        public SampleAdditonEnum[] EdgeSamples { get; set; }
        public SampleAdditonEnum[] EdgeAdditions { get; set; }

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
                        EdgeSample = EdgeSamples?[i] ?? SampleAdditonEnum.Auto,
                        EdgeAddition = EdgeAdditions?[i] ?? SampleAdditonEnum.Auto
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
        public SampleAdditonEnum EdgeSample { get; set; }
        public SampleAdditonEnum EdgeAddition { get; set; }
    }
}
