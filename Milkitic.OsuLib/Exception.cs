using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Milkitic.OsuLib
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
