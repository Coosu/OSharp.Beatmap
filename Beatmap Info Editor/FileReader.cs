using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Editor.Object;
using System.Reflection;

namespace Editor
{
    public class OsuFileReader
    {
        private static _Status status;
        private static StringBuilder sb;
        public static OsuFile ReadFromFile(string root)
        {
            StreamReader sr = new StreamReader(root);
            OsuFile of = new OsuFile();
            string line = sr.ReadLine().Trim();
            while (line != null)
            {
                Matching(of, line);
                line = sr.ReadLine();
            }
            Console.WriteLine(of.ToString());
            sr.Close();
            return of;
        }

        private static void Matching(OsuFile of, string line)
        {
            if (line.Length > 17 && line.Substring(0, 17) == "osu file format v")
            {
                line = line.Replace("osu file format v", "");
                of.Version = int.Parse(line);
                status = _Status.Version;
            }
            else if (line.IndexOf("[") == 0 && line.LastIndexOf("]") == line.Length - 1)
            {
                line = line.Remove(line.Length - 1).Remove(0, 1);
                if (line == "General")
                {
                    of.General = new obj_General();
                    status = _Status.General;
                }
                else if (line == "Editor")
                {
                    of.Editor = new obj_Editor();
                    status = _Status.Editor;
                }
                else if (line == "Metadata")
                {
                    of.Metadata = new obj_Metadata();
                    status = _Status.Metadata;
                }
                else if (line == "Difficulty")
                {
                    of.Difficulty = new obj_Difficulty();
                    status = _Status.Difficulty;
                }
                else if (line == "Events")
                {
                    Console.WriteLine("To optimize your storyboard layer, visit: https://osu.ppy.sh/forum/t/532042");
                    sb = new StringBuilder();
                    of.Events = new obj_Events();
                    status = _Status.Events;
                }
                else if (line == "TimingPoints")
                {
                    sb.Clear();
                    sb = null;
                    of.TimingPoints = new obj_TimingPoints();
                    of.TimingPoints.TimingPointList = new List<_TimingPoints>();
                    status = _Status.TimingPoints;
                }
                else if (line == "Colours")
                {
                    of.Colours = new obj_Colours();
                    status = _Status.Colours;
                }
                else if (line == "HitObjects")
                {
                    of.HitObjects = new obj_HitObjects();
                    of.HitObjects.HitObjectList = new List<_HitObjects>();
                    status = _Status.HitObjects;
                }
            }
            else if (status != _Status.Version)
            {
                if (status == _Status.General) MatchedGeneral(of, line);
                else if (status == _Status.Editor) MatchedEditor(of, line);
                else if (status == _Status.Metadata) MatchedMetadata(of, line);
                else if (status == _Status.Difficulty) MatchedDifficulty(of, line);
                else if (status == _Status.Events) MatchedEvents(of, line);
                else if (status == _Status.TimingPoints) MatchedTimingPoints(of, line);
                else if (status == _Status.Colours) MatchedColours(of, line);
                else if (status == _Status.HitObjects) MatchedHitObjects(of, line);
            }
            else
            {
                if (status == _Status.Version)
                    of.TheRestText += Environment.NewLine; // 性能方面再说
            }
        }
        private static void MatchedGeneral(OsuFile of, string line)
        {
            if (line == "")
            {
                of.General.TheRestText += Environment.NewLine;
                return;
            }
            string name, value;
            int pos = line.IndexOf(":");
            if (pos == -1) throw new Exception("cnm");
            name = line.Substring(0, pos).Trim();
            value = line.Substring(name.Length + 2, line.Length - 1 - name.Length - 1).Trim();

            var nameProp = of.General.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

            if (nameProp == null)
            {
                Console.WriteLine("General." + name);
                return;
            }
            if (nameProp.GetMethod.ReturnType == typeof(string))
                nameProp.SetValue(of.General, value);
            else if (nameProp.GetMethod.ReturnType == typeof(int))
                nameProp.SetValue(of.General, int.Parse(value));
            else if (nameProp.GetMethod.ReturnType == typeof(double))
                nameProp.SetValue(of.General, double.Parse(value));
            else if (nameProp.GetMethod.ReturnType == typeof(bool))
                nameProp.SetValue(of.General, Convert.ToBoolean(int.Parse(value)));
            else if (nameProp.GetMethod.ReturnType == typeof(_SampleSet))
                nameProp.SetValue(of.General, Enum.Parse(typeof(_SampleSet), value));
            else if (nameProp.GetMethod.ReturnType == typeof(_Mode))
                nameProp.SetValue(of.General, int.Parse(value));
            else throw new Exception("鸽爆");
        }
        private static void MatchedEditor(OsuFile of, string line)
        {
            if (line == "")
            {
                of.Editor.TheRestText += Environment.NewLine;
                return;
            }
            string name, value;
            int pos = line.IndexOf(":");
            if (pos == -1) throw new Exception("cnm");
            name = line.Substring(0, pos).Trim();
            value = line.Substring(name.Length + 2, line.Length - 1 - name.Length - 1).Trim();

            var nameProp = of.Editor.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

            if (nameProp == null)
            {
                Console.WriteLine("Editor." + name);
                return;
            }

            if (nameProp.GetMethod.ReturnType == typeof(string))
                nameProp.SetValue(of.Editor, value);
            else if (nameProp.GetMethod.ReturnType == typeof(int))
                nameProp.SetValue(of.Editor, int.Parse(value));
            else if (nameProp.GetMethod.ReturnType == typeof(double))
                nameProp.SetValue(of.Editor, double.Parse(value));
            else throw new Exception("鸽爆");
        }
        private static void MatchedMetadata(OsuFile of, string line)
        {
            if (line == "")
            {
                of.Metadata.TheRestText += Environment.NewLine;
                return;
            }
            string name, value;
            int pos = line.IndexOf(":");
            if (pos == -1) throw new Exception("cnm");
            name = line.Substring(0, pos).Trim();
            value = line.Substring(name.Length + 1, line.Length - name.Length - 1).Trim();

            var nameProp = of.Metadata.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

            if (nameProp == null)
            {
                Console.WriteLine("Metadata." + name);
                return;
            }
            if (nameProp.GetMethod.ReturnType == typeof(string))
                nameProp.SetValue(of.Metadata, value);
            else if (nameProp.GetMethod.ReturnType == typeof(int))
                nameProp.SetValue(of.Metadata, int.Parse(value));
            else throw new Exception("鸽爆");
        }
        private static void MatchedDifficulty(OsuFile of, string line)
        {
            if (line == "")
            {
                of.Difficulty.TheRestText += Environment.NewLine;
                return;
            }
            string name, value;
            int pos = line.IndexOf(":");
            if (pos == -1) throw new Exception("cnm");
            name = line.Substring(0, pos).Trim();
            value = line.Substring(name.Length + 1, line.Length - name.Length - 1).Trim();

            var nameProp = of.Difficulty.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

            if (nameProp == null)
            {
                Console.WriteLine("Difficulty." + name);
                return;
            }
            if (nameProp.GetMethod.ReturnType == typeof(double))
                nameProp.SetValue(of.Difficulty, double.Parse(value));
            else if (nameProp.GetMethod.ReturnType == typeof(int))
                nameProp.SetValue(of.Difficulty, int.Parse(value));
            else throw new Exception("鸽爆");
        }
        private static void MatchedEvents(OsuFile of, string line)
        {
            sb.AppendLine(line);
            of.Events.TheRestText = sb.ToString();
        }
        private static void MatchedTimingPoints(OsuFile of, string line)
        {
            if (line == "")
            {
                of.TimingPoints.TheRestText += Environment.NewLine;
                return;
            }
            string[] param = line.Split(',');
            of.TimingPoints.TimingPointList.Add(new _TimingPoints()
            {
                Offset = int.Parse(param[0]),
                Factor = double.Parse(param[1]),
                Rhythm = int.Parse(param[2]),
                SampleSet = (_SampleSet)(int.Parse(param[3]) - 1),
                Track = int.Parse(param[4]),
                Volume = int.Parse(param[5]),
                Inherit = !Convert.ToBoolean(int.Parse(param[6])),
                Kiai = Convert.ToBoolean(int.Parse(param[7])),
                Positive = double.Parse(param[1]) < 0 ? false : true
            });
        }
        private static void MatchedColours(OsuFile of, string line)
        {
            if (line == "")
            {
                of.Colours.TheRestText += Environment.NewLine;
                return;
            }
            string name, value;
            int pos = line.IndexOf(":");
            if (pos == -1) throw new Exception("cnm");
            name = line.Substring(0, pos).Trim();
            value = line.Substring(name.Length + 2, line.Length - 1 - name.Length - 1).Trim();
            string[] colors = value.Split(',');
            var nameProp = of.Colours.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

            if (nameProp == null)
            {
                Console.WriteLine("Colours." + name);
                return;
            }
            if (nameProp.GetMethod.ReturnType == typeof(System.Drawing.Color)) {
                nameProp.SetValue(of.Colours, System.Drawing.Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2])));
                of.Colours.Count++;
            }
            else throw new Exception("鸽爆");
        }
        private static void MatchedHitObjects(OsuFile of, string line)
        {
            if (line == "")
            {
                of.HitObjects.TheRestText += Environment.NewLine;
                return;
            }
            string[] param = line.Split(',');
            of.HitObjects.HitObjectList.Add(new _HitObjects()
            {
                X = int.Parse(param[0]),
                Y = int.Parse(param[1]),
                Offset = int.Parse(param[2]),
                TheRestText = line.Replace(param[0] + "," + param[1] + "," + param[2] + ",", "")
        });
        }
}
}
