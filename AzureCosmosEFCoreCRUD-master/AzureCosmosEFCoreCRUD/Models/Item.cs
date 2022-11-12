using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AzureCosmosEFCoreCRUD.Models
{
    public class Item
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "orderstatus")]
        public string OrderStatus { get; set; }

        [JsonProperty(PropertyName = "orderprovince")]
        public string OrderProvince { get; set; }

        [JsonProperty(PropertyName = "locationname")]
        public string LocationName { get; set; }
    }
}
