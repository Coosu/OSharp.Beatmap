using OSharp.Beatmap.Enums;

namespace OSharp.Beatmap.Model.Raw
{
    public class RawHitObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Offset { get; set; }
        public RawObjectType RawType { get; set; }
        public ObjectType ObjectType
        {
            get
            {
                if ((RawType & RawObjectType.Circle) == RawObjectType.Circle)
                    return ObjectType.Circle;
                if ((RawType & RawObjectType.Slider) == RawObjectType.Slider)
                    return ObjectType.Slider;
                if ((RawType & RawObjectType.Spinner) == RawObjectType.Spinner)
                    return ObjectType.Spinner;
                if ((RawType & RawObjectType.Hold) == RawObjectType.Hold)
                    return ObjectType.Hold;
                return ObjectType.Circle;
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
        public string Extras { get; set; }

        // extended
        public SampleAdditonEnum SampleSet => Extras?.Split(':')[0].ParseToEnum<SampleAdditonEnum>() ?? default;
        public SampleAdditonEnum AdditionSet => Extras?.Split(':')[1].ParseToEnum<SampleAdditonEnum>() ?? default;
        public int CustomIndex => Extras == null ? 0 : int.Parse(Extras.Split(':')[2]);
        public int SampleVolume => Extras == null
            ? 0
            : (Extras.Split(':').Length > 3
                ? int.Parse(Extras.Split(':')[3])
                : 0);
        public string FileName => Extras == null
            ? ""
            : (Extras.Split(':').Length > 4
                ? Extras.Split(':')[4]
                : "");
        public string NotImplementedInfo { get; set; }

        public override string ToString() => $"{X},{Y},{Offset},{NotImplementedInfo}";
    }
}
