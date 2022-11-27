using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace ETLAPIFramework.ConsoleApp.Data.Common
{
    public static class JsonObject
    {
        public static bool IsValidJson(this string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return false;
            }

            if (stringValue=="[]")
            {
                return false;
            }

            if (stringValue == null)
            {
                return false;
            }

            var value = stringValue.Trim();

            if ((value.StartsWith("{") && value.EndsWith("}")) ||
                (value.StartsWith("[") && value.EndsWith("]")))
            {
                try
                {
                    var obj = JToken.Parse(value);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }

            return false;
        }


        public static string Getproperty(string inputjson)
        {

            if(inputjson==null)
            {
                return null;
            }

            string json = inputjson;
            var newResource = JsonConvert.DeserializeObject<List<Jsonparse>>(json);
            string value = newResource[0].Text;
            return value;
        }

        public static Stream GenerateStreamFromJson(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }

}