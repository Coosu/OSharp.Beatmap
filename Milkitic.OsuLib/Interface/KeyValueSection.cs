using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Milkitic.OsuLib.Model;

namespace Milkitic.OsuLib.Interface
{
    public abstract class KeyValueSection : ISection
    {
        [ConfigIgnore]
        public static string KeyValueFlag { get; set; } = ":";

        [ConfigIgnore]
        public Dictionary<string, string> UndefinedPairs { get; set; }

        public void Match(string line)
        {
            var splitted = line.Split(new[] { KeyValueFlag }, StringSplitOptions.None);
            if (splitted.Length < 2)
                throw new Exception("Unknown Key-Value: " + line);
            var key = splitted[0];
            var value = string.Join(KeyValueFlag, splitted.Skip(1)).Trim();

            var prop = GetType().GetProperty(key, BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
            {
                if (UndefinedPairs == null) UndefinedPairs = new Dictionary<string, string>();
                UndefinedPairs.Add(key, value);
            }
            else
            {
                var propType = prop.GetMethod.ReturnType;

                if (propType.BaseType == typeof(Enum))
                    prop.SetValue(this, Enum.Parse(propType, value));
                else
                {
                    object sb = ConvertValue(value, propType);
                    prop.SetValue(this, sb);
                }
            }
        }

        public string ToSerializedString()
        {
            var props = GetType().GetProperties().Where(p => p.GetCustomAttribute(typeof(ConfigIgnoreAttribute)) == null);
            StringBuilder sb = new StringBuilder($"[{GetType().Name}]\r\n");

            foreach (var prop in props)
            {
                object key = prop.Name, value = prop.GetValue(this);

                if (prop.GetMethod.ReturnType.BaseType == typeof(Enum))
                {
                    var attrs = prop.GetCustomAttributes(false);

                    foreach (var info in attrs)
                    {
                        switch (info)
                        {
                            case ConfigEnumAttribute configEnum:
                                if (configEnum.Type == EnumParseType.Index)
                                    value = (int)value;
                                break;
                        }
                    }
                }
                else if (prop.GetMethod.ReturnType == typeof(bool))
                {
                    var attrs = prop.GetCustomAttributes(false);

                    foreach (var info in attrs)
                    {
                        switch (info)
                        {
                            case ConfigBoolAttribute configBool:
                                if (configBool.Type == BoolParseType.ZeroOne)
                                    value = Convert.ToInt32(value);
                                break;
                        }
                    }
                }

                sb.AppendLine($"{key}: {value}");
            }

            return sb + "\r\n";
        }

        private static object ConvertValue(string value, Type propType)
        {
            object arg;
            if (propType == typeof(bool) && int.TryParse(value, out var parsed))
                arg = parsed;
            else
                arg = value;

            var type = typeof(Convert);
            var methodName = $"To{propType.Name}";
            var method = type.GetMethods().Where(t => t.Name == methodName).Where(t => t.GetParameters().Length == 1)
                .FirstOrDefault(t => t.GetParameters().First().ParameterType == typeof(object));

            if (method == default)
                throw new MissingMethodException($"Can not find method: \"Convert.{methodName}(Object obj)\"");

            object[] p = { arg };
            return method.Invoke(null, p);
        }
    }
}
