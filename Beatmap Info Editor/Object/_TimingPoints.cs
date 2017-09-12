using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Object
{
    public class _TimingPoints
    {
        public bool Positive { get; set; }
        public int Offset { get; set; }
        public double Factor { get; set; }
        public double BPM //计算属性
        {
            get
            {
                return Inherit ? -1 : Math.Round(60000d / Factor, 3);
            }
            set
            {
                if (!Inherit)
                {
                    Factor = 60000d / value;
                }
                else throw new Exception("You can not change BPM directly: The current timing point is inherited.");
            }
        }
        public double Multiple //计算属性
        {
            get
            {
                return Inherit ? Math.Round(100d / Math.Abs(Factor), 2) : -1;
            }
            set
            {
                if (Inherit)
                {
                    Factor = Positive ? 100d / value : -100d / value;
                }
                else throw new Exception("You can not change multiple directly: The current timing point is not inherited.");
            }
        }

        // 先鸽3个参数，忘了意义
        public int Rhythm
        {
            get => rhythm;
            set
            {
                //if (value < 1 || value > 7) value = 4; //此处待定
                rhythm = value;
            }
        }
        public int Track { get; set; }
        public _SampleSet SampleSet { get; set; }
        public int Volume { get; set; }
        public bool Inherit { get; set; }
        public bool Kiai { get; set; }
        private int rhythm;
    }
}
