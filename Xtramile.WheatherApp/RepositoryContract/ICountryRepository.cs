using System.Collections.Generic;
using Xtramile.WheatherApp.Dto;

namespace Xtramile.WheatherApp.RepositoryContract
{
    public interface ICountryRepository
    {
        CountryDto Read(string id);
        List<CountryDto> GetAllCountry();
    }
}
