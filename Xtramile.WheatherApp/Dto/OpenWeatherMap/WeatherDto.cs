﻿using Newtonsoft.Json;

namespace Xtramile.WheatherApp.Dto.OpenWeatherMap
{
    public class WeatherDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
