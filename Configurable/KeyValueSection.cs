using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OSharp.Beatmap.Configurable
{
    public abstract class KeyValueSection : ISection
    {
        [SectionIgnore]
        public static string KeyValueFlag { get; set; } = ":";

        [SectionIgnore]
        public Dictionary<string, string> UndefinedPairs { get; set; }

        public virtual void Match(string line)
        {
            var index = line.IndexOf(KeyValueFlag, StringComparison.InvariantCulture);
            if (index == -1)
                throw new Exception("Unknown Key-Value: " + line);

            var key = line.Substring(0, index);
            var value = line.Substring(index + 1);

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

        public virtual string ToSerializedString()
        {
            var props = GetType().GetProperties().Where(p => p.GetCustomAttribute(typeof(SectionIgnoreAttribute)) == null);
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
                            case SectionEnumAttribute configEnum:
                                if (configEnum.Option == EnumParseOption.Index)
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
                            case SectionBoolAttribute configBool:
                                if (configBool.Option == BoolParseOption.ZeroOne)
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
