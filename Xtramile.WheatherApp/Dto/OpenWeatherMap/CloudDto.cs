using Newtonsoft.Json;

namespace Xtramile.WheatherApp.Dto.OpenWeatherMap
{
    public class CloudDto
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}
