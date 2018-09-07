using System;
using System.Diagnostics;
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
            OsuFile file =
                new OsuFile(
                    @"D:\Program Files (x86)\osu!\Songs\hey\DJ NAGAI feat. aru - Benibotan (yf_bmp) [Another].osu");// manager.FileList.First(k => k.Metadata.Version == "yf's Insane");
            Console.WriteLine("Select:" + sw.ElapsedMilliseconds);
            sw.Reset();
            var g = file.GetInterval(3);
            Console.WriteLine("Get Interval:" + sw.ElapsedMilliseconds);
            sw.Reset(); foreach (var d in g)
            {
                Console.WriteLine(d.Value);
            }

            var ok = file.GetTimingBars();
            var ok2 = file.GetTimingKiais();
            file.GenerateFile(file.FileName);
            Console.ReadLine();
        }
    }
}
