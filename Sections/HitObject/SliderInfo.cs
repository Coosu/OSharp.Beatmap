using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OSharp.Beatmap.Internal;

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
        public Point StartPoint { get; }
        public Point EndPoint => CurvePoints.Last();

        public SliderInfo(Point startPoint, int offset, double beatDuration, double sliderMultiplier)
        {
            StartPoint = startPoint;
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
        public override string ToString()
        {
            var sampleList = new List<(ObjectSamplesetType, ObjectSamplesetType)>();
            string edgeSampleStr;
            string edgeHitsoundStr;
            if (EdgeSamples != null)
            {
                for (var i = 0; i < EdgeSamples.Length; i++)
                {
                    var objectSamplesetType = EdgeSamples[i];
                    var objectAdditionType = EdgeAdditions[i];
                    sampleList.Add((objectSamplesetType, objectAdditionType));
                }

                edgeSampleStr = "," + string.Join("|", sampleList.Select(k => $"{(int)k.Item1}:{(int)k.Item2}"));
            }
            else
            {
                edgeSampleStr = "";
            }

            if (EdgeHitsounds != null)
            {
                edgeHitsoundStr = "," + string.Join("|", EdgeHitsounds.Select(k => $"{(int)k}"));
            }
            else
            {
                edgeHitsoundStr = "";
            }

            return string.Format("{0}|{1},{2},{3}{4}{5}",
                SliderType.ParseToCode(),
                string.Join("|", CurvePoints.Select(k => $"{k.X}:{k.Y}")),
                Repeat,
                PixelLength,
                edgeHitsoundStr,
                edgeSampleStr);
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
