using Newtonsoft.Json;

namespace Xtramile.WheatherApp.Model.Response
{
    public class ApiErrorResponseModel
    {
        [JsonProperty("errorMessages")]
        public string[] ErrorMessages { get; set; }
    }
}
