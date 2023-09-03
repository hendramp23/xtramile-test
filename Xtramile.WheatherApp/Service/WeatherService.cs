using System.Threading.Tasks;
using Newtonsoft.Json;
using Xtramile.WheatherApp.Core.Constant;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.Dto.OpenWeatherMap;
using Xtramile.WheatherApp.ServiceContract;
using Xtramile.WheatherApp.ServiceContract.Response;

namespace Xtramile.WheatherApp.Service
{
    public class WeatherService : IWeatherService
    {
        #region Fields

        private readonly IApiManager apiManager;

        #endregion

        #region Constructor

        public WeatherService(IApiManager apiManager)
        {
            this.apiManager = apiManager;
        }

        #endregion

        #region Public Methods

        public GenericResponse<WeatherDataDto> GetCityWeatherByCityName(string cityName)
        {
            var response = new GenericResponse<WeatherDataDto>();
            var url = string.Format(ApiConstant.OpenWeatherApiUrl, cityName, ApiConstant.OpenWeatherMapApiKey);
            var weatherApiResponse = Task.Run(() => this.apiManager.SendRequestAsync(url, "GET")).GetAwaiter().GetResult();
            
            if (weatherApiResponse.IsError())
            {
                response.AddErrorMessage(weatherApiResponse.GetErrorMessage());
                return response;
            }

            var result = JsonConvert.DeserializeObject<OpenWeatherMapDto>(weatherApiResponse.Data);
            response.Data = GetWeatherDataDto(result);
            return response;
        }


        #endregion

        #region Private Methods

        private WeatherDataDto GetWeatherDataDto(OpenWeatherMapDto apiData)
        {
            var weatherData = new WeatherDataDto()
            {
                Country = apiData.Sys.Country,
                City = apiData.Name,
                Time = apiData.TimeZone,
                DewPoint = 0,
                Humidity = apiData.Main.Humidity,
                Location = $"lon: {apiData.Coordinate.Longitude}, lat: {apiData.Coordinate.Latitude}",
                SkyCondition = apiData.Weather[0].Main,
                TemperatureFahrenheit = apiData.Main.Temperature,
                TemperatureCelcius = GetFahrenheitInCelcious(apiData.Main.Temperature),
                Visibility = apiData.Visibility,
                Wind = apiData.Wind.Speed,
                WindDirection = apiData.Wind.Deg,
                Pressure = apiData.Main.Preasure,
            };

            return weatherData;
        }

        #endregion

        #region Private Methods

        private decimal GetFahrenheitInCelcious(decimal fahrenheitTemperature)
        {
            return (fahrenheitTemperature - 32) * 5 / 9;
        }


        #endregion
    }
}
