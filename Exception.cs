using System;

namespace OSharp.Beatmap
{
    public class MultiTimingSectionException : ApplicationException
    {
        public MultiTimingSectionException(string message) : base(message)
        {
        }
    }
    public class VersionNotSupportedException : ApplicationException
    {
        public VersionNotSupportedException(int version) : base($"尚不支持 osu file format v[{version}]。")
        {
        }
    }

    public class BadOsuFormatException : ApplicationException
    {
        public BadOsuFormatException(string message) : base(message)
        {
        }
    }
}
