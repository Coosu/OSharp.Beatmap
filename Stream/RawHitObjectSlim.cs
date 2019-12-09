using System;
using OSharp.Beatmap.Internal;
using OSharp.Beatmap.Sections.HitObject;

namespace OSharp.Beatmap.Stream
{
    public struct RawHitObjectSlim
    {
        private string _extras;
        private bool _extraInitial;
        private ObjectSamplesetType _sampleSet;
        private ObjectSamplesetType _additionSet;
        private int _customIndex;
        private int _sampleVolume;
        private string _fileName;

        public int X { get; set; }
        public int Y { get; set; }
        public int Offset { get; set; }
        public RawObjectType RawType { get; set; }

        public HitObjectType ObjectType
        {
            get
            {
                if (RawType.HasFlag(RawObjectType.Circle))
                    return HitObjectType.Circle;
                if (RawType.HasFlag(RawObjectType.Slider))
                    return HitObjectType.Slider;
                if (RawType.HasFlag(RawObjectType.Spinner))
                    return HitObjectType.Spinner;
                if (RawType.HasFlag(RawObjectType.Hold))
                    return HitObjectType.Hold;
                return HitObjectType.Circle;
            }
        }

        public int NewComboSkipCount
        {
            get
            {
                var @base = RawType.HasFlag(RawObjectType.NewCombo) ? 1 : 0;
                var skipBin = 0b01110000 & (int)RawType;
                var skip = skipBin >> 4;
                return skip + @base;
            }
        }

        public HitsoundType Hitsound { get; set; }
        public SliderInfo SliderInfo { get; set; }
        public int HoldEnd { get; set; }

        public string Extras
        {
            get => _extras;
            set
            {
                _extras = value;
                _extraInitial = false;
            }
        }

        #region Extras

        public ObjectSamplesetType SampleSet
        {
            get
            {
                if (_extraInitial) InitialExtra();
                return _sampleSet;
            }
            set => _sampleSet = value;
        }

        public ObjectSamplesetType AdditionSet
        {
            get
            {
                if (_extraInitial) InitialExtra();
                return _additionSet;
            }
            set => _additionSet = value;
        }

        public int CustomIndex
        {
            get
            {
                if (_extraInitial) InitialExtra();
                return _customIndex;
            }
            set => _customIndex = value;
        }

        public int SampleVolume
        {
            get
            {
                if (_extraInitial) InitialExtra();
                return _sampleVolume;
            }
            set => _sampleVolume = value;
        }

        public string FileName
        {
            get
            {
                if (_extraInitial) InitialExtra();
                return _fileName;
            }
            set => _fileName = value;
        }

        private void InitialExtra()
        {
            if (!string.IsNullOrWhiteSpace(Extras))
            {
                var arr = Extras.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0) SampleSet = arr[0].ParseToEnum<ObjectSamplesetType>();
                if (arr.Length > 1) AdditionSet = arr[1].ParseToEnum<ObjectSamplesetType>();
                if (arr.Length > 2) CustomIndex = int.Parse(arr[2]);
                if (arr.Length > 3) SampleVolume = int.Parse(arr[3]);
                if (arr.Length > 4) FileName = arr[4];
            }

            _extraInitial = true;
        }

        #endregion

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
    }
}