using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSharp.Beatmap
{
    public struct MetaString
    {
        public string Origin { get; }
        public string Unicode { get; }
        private readonly bool _preferUnicode;

        public bool IsWestern => !(_preferUnicode && Unicode != Origin && !string.IsNullOrEmpty(Unicode));

        public MetaString(string origin, string unicode) :
            this(origin, unicode, true)
        {
        }

        public MetaString(string origin, string unicode, bool preferUnicode)
        {
            Origin = new string(origin.Where(k => k <= 126 || k >= 32).ToArray());
            Unicode = unicode;
            _preferUnicode = preferUnicode;
        }

        public string ToUnicodeString()
        {
            return string.IsNullOrEmpty(Unicode)
                ? (string.IsNullOrEmpty(Origin) ? default : Origin)
                : Unicode;
        }

        public string ToOriginalString()
        {
            return string.IsNullOrEmpty(Origin)
                ? (string.IsNullOrEmpty(Unicode) ? "" : Unicode)
                : Origin;
        }

        public string ToPreferredString() => _preferUnicode ? string.IsNullOrEmpty(Unicode) ? Origin : Unicode : Origin;

        public override string ToString() => string.IsNullOrEmpty(Unicode) ? Origin : Unicode;
    }
}
