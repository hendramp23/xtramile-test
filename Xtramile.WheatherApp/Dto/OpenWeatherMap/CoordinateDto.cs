using Newtonsoft.Json;

namespace Xtramile.WheatherApp.Dto.OpenWeatherMap
{
    public class CoordinateDto
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}
