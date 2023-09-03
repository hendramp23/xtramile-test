using Newtonsoft.Json;

namespace Xtramile.WheatherApp.Dto.OpenWeatherMap
{
    public class WindDto
    {
        [JsonProperty("speed")]
        public decimal Speed { get; set; }

        [JsonProperty("deg")]
        public int Deg { get; set; }

        [JsonProperty("gust")]
        public decimal Gust { get; set; }
    }
}
