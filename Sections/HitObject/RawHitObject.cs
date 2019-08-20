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
        public string Extras { get; set; }

        // extended
        public ObjectSamplesetType SampleSet
        {
            get { return Extras?.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0].ParseToEnum<ObjectSamplesetType>() ?? default; }
            set
            {
                var array = Extras?.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (array != null && array.Length > 0)
                {
                    array[0] = ((int)value).ToString();
                    Extras = string.Join(":", array) + ":";
                }
            }
        }

        public ObjectSamplesetType AdditionSet
        {
            get { return Extras?.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].ParseToEnum<ObjectSamplesetType>() ?? default; }
            set
            {
                var array = Extras?.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (array != null && array.Length > 1)
                {
                    array[1] = ((int)value).ToString();
                    Extras = string.Join(":", array) + ":";
                }
            }
        }

        public int CustomIndex
        {
            get => Extras == null ? 0 : int.Parse(Extras.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[2]);
            set
            {
                var array = Extras?.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (array != null && array.Length > 2)
                {
                    array[2] = value.ToString();
                    Extras = string.Join(":", array) + ":";
                }
            }
        }

        public int SampleVolume
        {
            get =>
                Extras == null
                    ? 0
                    : (Extras.Split(':').Length > 3
                        ? int.Parse(Extras.Split(':')[3])
                        : 0);
            set
            {
                var array = Extras?.Split(':');
                if (array != null && array.Length > 3)
                {
                    array[3] = value.ToString();
                    Extras = string.Join(":", array);
                }
            }
        }

        public string FileName
        {
            get =>
                Extras == null
                    ? ""
                    : (Extras.Split(':').Length > 4
                        ? Extras.Split(':')[4]
                        : "");
            set
            {
                var array = Extras?.Split(':');
                if (array != null && array.Length > 4)
                {
                    array[4] = value;
                    Extras = string.Join(":", array);
                }
            }
        }

        public string NotImplementedInfo { get; set; }

        public override string ToString()
        {
            switch (ObjectType)
            {
                case HitObjectType.Circle:
                    return $"{X},{Y},{Offset},{(int)RawType},{(int)Hitsound}{(Extras == null ? "" : "," + Extras)}";
                case HitObjectType.Slider:
                    return $"{X},{Y},{Offset},{(int)RawType},{(int)Hitsound},{SliderInfo}{(Extras == null ? "" : "," + Extras)}";
                case HitObjectType.Spinner:
                    throw new NotImplementedException();
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
