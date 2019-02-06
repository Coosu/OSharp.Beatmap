using OSharp.Beatmap.Internal;
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
        public Dictionary<string, string> UndefinedPairs { get; set; }

        [SectionIgnore]
        public List<(PropertyInfo propInfo, string name)> PropertyInfos { get; set; }

        public KeyValueSection()
        {
            var type = GetType();
            PropertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(k => k.GetCustomAttribute<SectionIgnoreAttribute>() == null)
                .Select(k =>
                {
                    var attr = k.GetCustomAttribute<SectionPropertyAttribute>();
                    return (k, attr == null ? k.Name : attr.Name);
                })
                .ToList();
            PropertyInfos.AddRange(type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(k => k.GetCustomAttribute<SectionPropertyAttribute>() != null)
                .Select(k =>
                {
                    var attr = k.GetCustomAttribute<SectionPropertyAttribute>();
                    return (k, attr.Name);
                })
            );
        }

        public virtual void Match(string line)
        {
            if (!MatchKeyValue(line, out var key, out var value))
                throw new Exception("Unknown Key-Value: " + line);

            //var prop = GetType().GetProperty(key, BindingFlags.Public | BindingFlags.Instance);
            var prop = PropertyInfos.FirstOrDefault(k => k.name == key).propInfo;
            if (prop == null)
            {
                if (UndefinedPairs == null) UndefinedPairs = new Dictionary<string, string>();
                UndefinedPairs.Add(key, value);
            }
            else
            {
                var propType = prop.GetMethod.ReturnType;

                if (propType.BaseType == typeof(Enum))
                {
                    prop.SetValue(this, Enum.Parse(propType, value));
                }
                else
                {
                    var attr = prop.GetCustomAttribute<SectionConverterAttribute>();
                    if (attr != null)
                    {
                        var converter = (ValueConverter)Activator.CreateInstance(attr.ConverterType, true);
                        prop.SetValue(this, converter.ReadSection(value, propType));
                    }
                    else if (ValueConvert.ConvertValue(value, propType, out var converted))
                    {
                        prop.SetValue(this, converted);
                    }
                    else
                    {
                        throw new MissingMethodException($"Can not convert {{{value}}} to type {propType}.");
                    }
                }
            }
        }

        protected bool MatchKeyValue(string line, out string key, out string value)
        {
            int index = MatchFlag(line);
            if (index == -1)
            {
                key = null;
                value = null;
                return false;
            }

            key = line.Substring(0, index).Trim();
            value = line.Substring(index + 1).Trim();
            return true;
        }

        protected int MatchFlag(string line)
        {
            var index = line.IndexOf(KeyValueFlag, StringComparison.InvariantCulture);

            return index;
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

        protected virtual string KeyValueFlag { get; } = ":";
    }
}
