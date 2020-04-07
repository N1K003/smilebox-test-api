using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Smilebox.Common.Extensions
{
    public static class SerializationExtensions
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter()
            },
            Culture = CultureInfo.InvariantCulture
        };

        public static string SerializeToJson(this object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.None, Settings);
        }

        public static T DeserializeFromJson<T>(this string json)
        {
            return string.IsNullOrWhiteSpace(json) ? default : JsonConvert.DeserializeObject<T>(json, Settings);
        }
    }
}