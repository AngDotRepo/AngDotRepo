﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AzureCosmosEFCoreCRUD.Models
{
    public class ItemList
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "locationname")]
        public string LocationName { get; set; }
    }
}
