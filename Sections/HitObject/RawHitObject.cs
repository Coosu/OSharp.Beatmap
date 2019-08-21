using OSharp.Beatmap.Configurable;
using OSharp.Beatmap.Internal;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace OSharp.Beatmap.Sections.HitObject
{
    public class RawHitObject : SerializeWritableObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Offset { get; set; }
        public RawObjectType RawType { get; set; }
        public HitObjectType ObjectType
        {
            get
            {
                if ((RawType & RawObjectType.Circle) == RawObjectType.Circle)
                    return HitObjectType.Circle;
                if ((RawType & RawObjectType.Slider) == RawObjectType.Slider)
                    return HitObjectType.Slider;
                if ((RawType & RawObjectType.Spinner) == RawObjectType.Spinner)
                    return HitObjectType.Spinner;
                if ((RawType & RawObjectType.Hold) == RawObjectType.Hold)
                    return HitObjectType.Hold;
                return HitObjectType.Circle;
            }
        }
        public int NewComboCount
        {
            get
            {
                int ncBase = 0;
                if ((RawType & RawObjectType.NewCombo) == RawObjectType.NewCombo)
                    ncBase = 1;
                var newThing = 0b01110000 & (int)RawType;
                var ncCount = newThing >> 4;
                return ncCount + ncBase;
            }
        }
        public HitsoundType Hitsound { get; set; }
        public SliderInfo SliderInfo { get; set; }
        public int HoldEnd { get; set; }

        public string Extras
        {
            get => $"{(int)SampleSet}:{(int)AdditionSet}:{CustomIndex}:{SampleVolume}:{FileName}";
            set
            {
                var arr = value?.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr != null)
                {
                    if (arr.Length > 0)
                    {
                        SampleSet = arr[0].ParseToEnum<ObjectSamplesetType>();
                    }
                    if (arr.Length > 1)
                    {
                        AdditionSet = arr[1].ParseToEnum<ObjectSamplesetType>();
                    }
                    if (arr.Length > 2)
                    {
                        CustomIndex = int.Parse(arr[2]);
                    }
                    if (arr.Length > 3)
                    {
                        SampleVolume = int.Parse(arr[3]);
                    }
                    if (arr.Length > 4)
                    {
                        FileName = arr[4];
                    }
                }
            }
        }

        public ObjectSamplesetType SampleSet { get; set; }

        public ObjectSamplesetType AdditionSet { get; set; }

        public int CustomIndex { get; set; }

        public int SampleVolume { get; set; }

        public string FileName { get; set; }

        //public string NotImplementedInfo { get; set; }

        public override string ToString()
        {
            switch (ObjectType)
            {
                case HitObjectType.Circle:
                    return $"{X},{Y},{Offset},{(int)RawType},{(int)Hitsound}{(Extras == null ? "" : "," + Extras)}";
                case HitObjectType.Slider:
                    return $"{X},{Y},{Offset},{(int)RawType},{(int)Hitsound},{SliderInfo}{(Extras == null ? "" : "," + Extras)}";
                case HitObjectType.Spinner:
                    return $"{X},{Y},{Offset},{(int)RawType},{(int)Hitsound},{HoldEnd},{Extras ?? ""}";
                case HitObjectType.Hold:
                    return $"{X},{Y},{Offset},{(int)RawType},{(int)Hitsound},{HoldEnd}:{Extras ?? ""}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void AppendSerializedString(TextWriter textWriter)
        {
            textWriter.WriteLine(ToString());
        }
    }
}
