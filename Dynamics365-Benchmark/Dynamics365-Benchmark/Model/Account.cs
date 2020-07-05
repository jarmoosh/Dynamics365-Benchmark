using Newtonsoft.Json;

namespace Dynamics365_Benchmark.Model
{
    public class Account
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}
