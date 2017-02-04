using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MessageService.Common.Utils
{
    public static class MiscExtensions
    {
        public static T ToConsole<T>(this T arg)
        {
            Console.WriteLine(arg?.ToString() ?? "## arg is null ##");
            return arg;
        }

        public static bool IsNull(this object arg)
        {
            return arg == null;
        }

        public static bool IsNotNull(this object arg)
        {
            return arg.IsNull().Not();
        }
        public static bool Not(this bool arg)
        {
            return !arg;
        }

        public static bool Is<T>(this object arg)
        {
            return arg is T;
        }
        public static T To<T>(this object arg)
        {
            return (T) arg;
        }
        public static string StringDump(this object value)
        {
            if (value.IsNull())
            {
                return "##null##";
            }
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(value, Formatting.Indented, serializerSettings);
        }

    }
}