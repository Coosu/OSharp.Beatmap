using System.Text;
using Milkitic.OsuLib.Interface;

namespace Milkitic.OsuLib.Model.Section
{
    public class Events : ISection
    {
        // 先鸽，忘记了某些参数意义
        private readonly StringBuilder _tmp = new StringBuilder();
        public void Match(string line)
        {
            _tmp.AppendLine(line);
        }

        public string ToSerializedString()
        {
            return $"[Events]\r\n{_tmp}\r\n";
        }
    }
}
