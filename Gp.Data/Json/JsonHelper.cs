using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Json
{
    public static class JsonHelper
    {

        private static JsonSerializerSettings camelCaseSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        public static string SerializeCamel(object value)
        {
            return JsonConvert.SerializeObject(value, camelCaseSerializerSettings);
        }
    }
}
