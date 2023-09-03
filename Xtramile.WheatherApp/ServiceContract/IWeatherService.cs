using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.ServiceContract.Response;

namespace Xtramile.WheatherApp.ServiceContract
{
    public interface IWeatherService
    {
        GenericResponse<WeatherDataDto> GetCityWeatherByCityName(string cityName);
    }
}
