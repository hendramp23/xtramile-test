using Newtonsoft.Json;
using System;

namespace Xtramile.WheatherApp.ServiceContract.Response
{
    public class ErrorApiResponse
    {
        [JsonProperty(PropertyName = "errorMessages")]

        public string[] ErrorMessages { get; set; } = Array.Empty<string>();
    }
}
