using System.Collections.Generic;
using Xtramile.WheatherApp.Dto;

namespace Xtramile.WheatherApp.RepositoryContract
{
    public interface ICityRepository
    {
        CityDto Read(string id);
        List<CityDto> GetCityByCountryId(string countryId);
    }
}
