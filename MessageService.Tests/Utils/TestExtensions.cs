using MessageService.Common.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MessageService.Tests.Utils
{
    public static class TestExtensions
    {
        public static void Dump(this object value)
        {
            value.StringDump().ToConsole();
        }
    }
}