using System;
using Octokit.Internal;
using Newtonsoft.Json;

namespace Brohub.Console
{
    public class JsonNetSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object item)
        {
            throw new NotImplementedException();
        }
    }
}