using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FoursquareOAuth2Client
{
    //Field containing JSON data that has not be deserialized yet
    public class UnparsedJSONField
    {
        [JsonExtensionData]
        public IDictionary<string, JToken> fieldData;
    }
}
