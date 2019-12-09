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
        public Vector2[] CurvePoints { get; set; }
        public int Repeat { get; set; }
        public decimal PixelLength { get; set; }
        public HitsoundType[] EdgeHitsounds { get; set; }
        public ObjectSamplesetType[] EdgeSamples { get; set; }
        public ObjectSamplesetType[] EdgeAdditions { get; set; }

        //extension
        public Vector2 StartPoint { get; }
        public Vector2 EndPoint => CurvePoints.Last();

        private double _singleElapsedTime;
        private SliderEdge[] _edges;
        private SliderTick[] _ticks;

        public SliderInfo(Vector2 startPoint, int offset, double beatDuration, double sliderMultiplier)
        {
            StartPoint = startPoint;
            _offset = offset;
            _beatDuration = beatDuration;
            _sliderMultiplier = sliderMultiplier;
            _singleElapsedTime = (double)(PixelLength / (100 * (decimal)_sliderMultiplier) * (decimal)_beatDuration);
        }

        public SliderEdge[] Edges
        {
            get
            {
                if (_edges == null)
                {
                    var edges = new SliderEdge[Repeat + 1];

                    for (var i = 0; i < edges.Length; i++)
                    {
                        edges[i] = new SliderEdge
                        {
                            Offset = _offset + _singleElapsedTime * i,
                            Point = i % 2 == 0 ? StartPoint : EndPoint,
                            EdgeHitsound = EdgeHitsounds?[i] ?? HitsoundType.Normal,
                            EdgeSample = EdgeSamples?[i] ?? ObjectSamplesetType.Auto,
                            EdgeAddition = EdgeAdditions?[i] ?? ObjectSamplesetType.Auto
                        };
                    }

                    _edges = edges;
                }

                return _edges;
            }
        }

        public SliderTick[] Ticks
        {
            get
            {
                if (_ticks == null)
                {
                    List<List<Vector2>> value = GetGroupedBezier();
                    var lengths = GetBezierLengths(value);
                    var totalLength = lengths.Sum();
                    int i = 1;
                    var offset = i * _beatDuration;
                    while (offset < _singleElapsedTime * Repeat)
                    {
                        if (!Edges.Any(k => Math.Abs(k.Offset - offset) < 0.01))
                        {
                            var isNegative = (int)(offset / _singleElapsedTime) % 2 != 0;
                            var ratio = (offset % _singleElapsedTime) / _singleElapsedTime;
                            var relativeLen = totalLength * ratio;
                            if (isNegative)
                            {
                                for (int j = 0; j < lengths.Count; j++)
                                {
                                    var item = lengths[j];

                                }
                            }
                            else
                            {
                                for (int j = lengths.Count - 1; j >= 0; j--)
                                {
                                    var item = lengths[j];

                                }
                            }
                        }

                        i++;
                        offset = i * _beatDuration;
                    }
                }

                return _ticks;
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

        private static List<double> GetBezierLengths(List<List<Vector2>> value)
        {
            var list = new List<double>();
            foreach (var controlPoints in value)
            {
                var points = Bezier.GetBezierTrail(controlPoints, 0.05f);
                double dis = 0;
                if (points.Length <= 1)
                {
                }
                else
                {
                    for (int j = 0; j < points.Length - 1; j++)
                    {
                        dis += Math.Pow(
                            Math.Pow(points[j].X - points[j + 1].X, 2) +
                            Math.Pow(points[j].Y - points[j + 1].Y, 2),
                            0.5);
                    }
                }

                list.Add(dis);
            }

            return list;
        }

        private List<List<Vector2>> GetGroupedBezier()
        {
            var list = new List<List<Vector2>>();
            var current = new List<Vector2>();
            list.Add(current);
            for (int i = 0; i < CurvePoints.Length; i++)
            {
                var @this = CurvePoints[i];
                current.Add(@this);
                if (i == CurvePoints.Length - 1)
                {
                    break;
                }

                var next = CurvePoints[i + 1];

                if (Math.Abs(@this.X - next.X) < 0.01 && Math.Abs(@this.Y - next.Y) < 0.01)
                {
                    current = new List<Vector2>();
                    list.Add(current);
                }
            }

            return list;
        }
    }

    public struct SliderEdge
    {
        public double Offset { get; set; }
        public Vector2 Point { get; set; }
        public HitsoundType EdgeHitsound { get; set; }
        public ObjectSamplesetType EdgeSample { get; set; }
        public ObjectSamplesetType EdgeAddition { get; set; }
    }

    //public struct BezierGroup
    //{
    //    public double Length { get; set; }
    //    public IReadOnlyList<Vector2> Points { get; set; }
    //}

    public struct SliderTick
    {
        public double Offset { get; set; }
        public Vector2 Point { get; set; }
    }
}
