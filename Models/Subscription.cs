using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace AnimalsApi.Models
{
    public class Subscription
    { 
        [JsonProperty("image_id")]
        public string image_id {get; set; }
        
        [JsonProperty("sub_id")]
        public string sub_id { get; set; }

    }

}