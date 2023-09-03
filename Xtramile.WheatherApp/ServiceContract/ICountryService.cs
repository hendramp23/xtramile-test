using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.ServiceContract.Response;

namespace Xtramile.WheatherApp.ServiceContract
{
    public interface ICountryService
    {
        GenericResponse<CountryDto> Read(string id);
        GenericGetDtoCollectionResponse<CountryDto> GetAllCountry();
    }
}
