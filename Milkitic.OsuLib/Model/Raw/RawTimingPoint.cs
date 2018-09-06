using Milkitic.OsuLib.Enums;
using System;
using System.Globalization;

namespace Milkitic.OsuLib.Model.Raw
{
    public class RawTimingPoint
    {
        public bool Positive { get; set; }
        public double Offset { get; set; }
        public double Factor { get; set; }
        public double Bpm //计算属性
        {
            get => Inherit ? -1 : Math.Round(60000d / Factor, 3);
            set
            {
                if (!Inherit)
                    Factor = 60000d / value;
                else throw new Exception("The current timing point is inherited.");
            }
        }
        public double Multiple //计算属性
        {
            get => Inherit ? Math.Round(100d / Math.Abs(Factor), 2) : -1;
            set
            {
                if (Inherit)
                    Factor = Positive ? 100d / value : -100d / value;
                else throw new Exception("The current timing point is not inherited.");
            }
        }

        // 先鸽3个参数，忘了意义
        public int Rhythm
        {
            get => _rhythm;
            set
            {
                if (value < 1 || value > 7) value = 4; //此处待定
                _rhythm = value;
            }
        }
        public SampleAdditonEnum SampleAdditonEnum { get; set; }
        public int Track { get; set; }
        public int Volume { get; set; }
        public bool Inherit { get; set; }
        public bool Kiai { get; set; }

        private int _rhythm;

        public override string ToString() => string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", Offset,
            Factor.ToString(CultureInfo.InvariantCulture), Rhythm,
            (int)SampleAdditonEnum + 1, Track, Volume, Convert.ToInt32(!Inherit), Convert.ToInt32(Kiai));
    }
}
