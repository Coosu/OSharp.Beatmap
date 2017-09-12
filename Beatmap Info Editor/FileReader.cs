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
            }
            else if (status != _Status.Version)
            {
                if (status == _Status.General) MatchedGeneral(of, line);
                else if (status == _Status.Editor) MatchedEditor(of, line);
                else if (status == _Status.Metadata) MatchedMetadata(of, line);
                else if (status == _Status.Difficulty) MatchedDifficulty(of, line);
                // 先鸽到这里
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
            name = line.Substring(0, pos);
            value = line.Substring(name.Length + 2, line.Length - 1 - name.Length - 1);

            var nameProp = of.General.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

            if (nameProp == null)
            {
                Console.WriteLine(name);
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
            name = line.Substring(0, pos);
            value = line.Substring(name.Length + 2, line.Length - 1 - name.Length - 1);

            var nameProp = of.Editor.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

            if (nameProp == null)
            {
                Console.WriteLine(name);
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
            name = line.Substring(0, pos);
            value = line.Substring(name.Length + 1, line.Length - name.Length - 1);

            var nameProp = of.Metadata.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

            if (nameProp == null)
            {
                Console.WriteLine(name);
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
            name = line.Substring(0, pos);
            value = line.Substring(name.Length + 1, line.Length - name.Length - 1);

            var nameProp = of.Difficulty.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

            if (nameProp == null)
            {
                Console.WriteLine(name);
                return;
            }
            if (nameProp.GetMethod.ReturnType == typeof(double))
                nameProp.SetValue(of.Difficulty, double.Parse(value));
            else if (nameProp.GetMethod.ReturnType == typeof(int))
                nameProp.SetValue(of.Difficulty, int.Parse(value));
            else throw new Exception("鸽爆");
        }
    }
}
