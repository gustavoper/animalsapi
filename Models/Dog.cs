using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace AnimalsApi.Models
{

    public class BreedImage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Dog
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("image_id")]
        public string ImageId { get; set; }

        [JsonProperty("sub_id")]
        public string SubId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("url")]
        public string ImageUrl { get; set; }


        [JsonProperty("image")]
        public BreedImage BreedImage { get; set; }


    }


    public class DogList
    {
        public List<Dog> Dog { get; set; }
    }




}