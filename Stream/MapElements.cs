using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OSharp.Beatmap.Internal;
using OSharp.Beatmap.Sections.HitObject;
using OSharp.Beatmap.Sections.Timing;

namespace OSharp.Beatmap.Stream
{
    class HitObjects : IEnumerable<RawHitObjectSlim>
    {
        private readonly string _file;

        public HitObjects(string file)
        {
            _file = file;
        }

        public IEnumerator<RawHitObjectSlim> GetEnumerator()
        {
            var enumerator = new HitObjectsEnumerator(_file);
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class HitObjectsEnumerator : IEnumerator<RawHitObjectSlim>
        {
            private readonly string _file;
            private StreamReader _sr;
            private bool _isFinished = true;

            public HitObjectsEnumerator(string file)
            {
                _file = file;
                ReadyReader();
            }

            private void ReadyReader()
            {
                _isFinished = true;
                _sr = new StreamReader(_file);
                //int lineIndex = 0;
                while (!_sr.EndOfStream)
                {
                    var line = _sr.ReadLine();
                    //lineIndex++;
                    if (line == "[HitObjects]")
                    {
                        _isFinished = false;
                    }
                }
            }

            public bool MoveNext()
            {
                if (_sr.EndOfStream || _isFinished)
                {
                    _sr?.Dispose();
                    return false;
                }

                var line = _sr.ReadLine();
                while (line == null)
                {
                    if (_sr.EndOfStream)
                        return false;
                    line = _sr.ReadLine();
                }

                string[] param = line.SpanSplit(",");

                var x = int.Parse(param[0]);
                var y = int.Parse(param[1]);
                var offset = int.Parse(param[2]);
                var type = (RawObjectType)Enum.Parse(typeof(RawObjectType), param[3]);
                var hitsound = (HitsoundType)Enum.Parse(typeof(HitsoundType), param[4]);
                var others = string.Join(",", param.Skip(5));

                var hitObject = new RawHitObjectSlim
                {
                    X = x,
                    Y = y,
                    Offset = offset,
                    RawType = type,
                    Hitsound = hitsound
                };
                switch (hitObject.ObjectType)
                {
                    case HitObjectType.Circle:
                        ToCircle(ref hitObject, others);
                        break;
                    case HitObjectType.Slider:
                        ToSlider(ref hitObject, others);
                        break;
                    case HitObjectType.Spinner:
                        ToSpinner(ref hitObject, others);
                        break;
                    case HitObjectType.Hold:
                        ToHold(ref hitObject, others);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Current = hitObject;
                return true;
            }

            private void ToCircle(ref RawHitObjectSlim hitObject, string others)
            {
                // extra
                hitObject.Extras = others;
            }

            private void ToSlider(ref RawHitObjectSlim hitObject, string others)
            {
                var infos = others.SpanSplit(",");

                // extra
                string notSureExtra = infos[infos.Length - 1];
                bool supportExtra = notSureExtra.IndexOf(":", StringComparison.Ordinal) != -1;
                hitObject.Extras = supportExtra ? notSureExtra : null;

                // slider curve
                var curveInfo = infos[0].SpanSplit("|");
                var sliderType = curveInfo[0];

                var points = new Vector2[curveInfo.Length - 1]; // curvePoints skip 1
                for (var i = 1; i < curveInfo.Length; i++)
                {
                    var point = curveInfo[i];
                    var xy = point.SpanSplit(":");
                    points[i] = new Vector2(int.Parse(xy[0]), int.Parse(xy[1]));
                }

                // repeat
                int repeat = int.Parse(infos[1]);

                // length
                decimal pixelLength = decimal.Parse(infos[2]);

                // edge hitsounds
                HitsoundType[] edgeHitsounds;
                ObjectSamplesetType[] edgeSamples;
                ObjectSamplesetType[] edgeAdditions;
                if (infos.Length == 3)
                {
                    edgeHitsounds = null;
                    edgeSamples = null;
                    edgeAdditions = null;
                }
                else if (infos.Length == 4)
                {
                    edgeHitsounds = infos[3].SpanSplit("|").Select(t => t.ParseToEnum<HitsoundType>()).ToArray();
                    edgeSamples = null;
                    edgeAdditions = null;
                }
                else
                {
                    edgeHitsounds = infos[3].SpanSplit("|").Select(t => t.ParseToEnum<HitsoundType>()).ToArray();
                    string[] edgeAdditionsStrArr = infos[4].SpanSplit("|");
                    edgeSamples = new ObjectSamplesetType[repeat + 1];
                    edgeAdditions = new ObjectSamplesetType[repeat + 1];
                    for (int i = 0; i < edgeAdditionsStrArr.Length; i++)
                    {
                        var sampAdd = edgeAdditionsStrArr[i].SpanSplit(":");
                        edgeSamples[i] = sampAdd[0].ParseToEnum<ObjectSamplesetType>();
                        edgeAdditions[i] = sampAdd[1].ParseToEnum<ObjectSamplesetType>();
                    }
                }

                TimingPoint[] lastRedLinesIfExsist = _timingPoints.TimingList.Where(t => !t.Inherit)
                    .Where(t => t.Offset <= hitObject.Offset).ToArray();
                TimingPoint lastRedLine;
            }

            private void ToSpinner(ref RawHitObjectSlim hitObject, string others)
            {
                throw new NotImplementedException();
            }

            private void ToHold(ref RawHitObjectSlim hitObject, string others)
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                ReadyReader();
            }

            public RawHitObjectSlim Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _sr?.Dispose();
            }
        }
    }
}
