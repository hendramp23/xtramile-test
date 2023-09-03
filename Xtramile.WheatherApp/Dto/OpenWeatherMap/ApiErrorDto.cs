using Newtonsoft.Json;

namespace Xtramile.WheatherApp.Dto.OpenWeatherMap
{
    public class ApiErrorDto
    {
        [JsonProperty("cod")]
        public int Cod { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
