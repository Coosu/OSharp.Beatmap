using System;
using System.Diagnostics;
using Milkitic.OsbLib;
using Milkitic.OsbLib.Extension;
using Milkitic.OsuLib;

namespace LibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //OsuFileManager manager =
            //    new OsuFileManager(
            //        @"D:\Program Files (x86)\osu!\Songs\699819 nao - Towa naru Kizuna to Omoi no Kiseki (1)");
            //Console.WriteLine("Folder:" + sw.ElapsedMilliseconds);
            //sw.Reset();
            var oks = ElementGroup.Parse(
                @"D:\Program Files (x86)\osu!\Songs\245160 Kawada Mami - Wings of Courage -Sora o Koete-\Mami Kawada - Wings of Courage -Sora o Koete- (examination).osb");
            oks.Expand();

            OsuFile file =
                new OsuFile(
                    @"D:\Program Files (x86)\osu!\Songs\404658 Giga - -BWW SCREAM-\Giga - -BWW SCREAM- (Lavender) [yf's sb Extreme].osu");// manager.FileList.First(k => k.Metadata.Version == "yf's Insane");
            Console.WriteLine("Select:" + sw.ElapsedMilliseconds);
            sw.Reset();
            var g = file.TimingPoints.GetInterval(3);
            Console.WriteLine("Get Interval:" + sw.ElapsedMilliseconds);
            sw.Reset(); foreach (var d in g)
            {
                Console.WriteLine(d.Value);
            }

            var ok = file.TimingPoints.GetTimingBars();
            var ok2 = file.TimingPoints.GetTimingKiais();
            file.GenerateFile(file.FileName);
            Console.ReadLine();
        }

     
    }
}
