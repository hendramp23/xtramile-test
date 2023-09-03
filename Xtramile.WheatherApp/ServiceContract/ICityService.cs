using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.ServiceContract.Response;

namespace Xtramile.WheatherApp.ServiceContract
{
    public interface ICityService
    {
        GenericResponse<CityDto> Read(string id);
        GenericGetDtoCollectionResponse<CityDto> GetCityByCountryId(string countryId);
    }
}
