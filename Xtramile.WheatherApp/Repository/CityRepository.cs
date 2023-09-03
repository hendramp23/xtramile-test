using System.Collections.Generic;
using System.Linq;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.RepositoryContract;

namespace Xtramile.WheatherApp.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly List<CityDto> cities = new List<CityDto>();


        public CityRepository()
        {
            this.cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = "DPS",
                    Name = "Denpasar",
                    CountryId = "IDN"
                },
                new CityDto()
                {
                    Id = "JKT",
                    Name = "Jakarta",
                    CountryId = "IDN"
                },
                new CityDto()
                {
                    Id = "CAN",
                    Name = "Canbera",
                    CountryId = "AUS"
                },
                new CityDto()
                {
                    Id = "MEL",
                    Name = "Melbourne",
                    CountryId = "AUS"
                },
                new CityDto()
                {
                    Id = "BRI",
                    Name = "Brisbane",
                    CountryId = "AUS"
                },
                new CityDto()
                {
                    Id = "SIN",
                    Name = "Singapore",
                    CountryId = "SIN"
                }
            };
        }

        public CityDto Read(string id)
        {
            return cities.FirstOrDefault(c => c.Id == id);
        }

        public List<CityDto> GetCityByCountryId(string countryId)
        {
            return this.cities.Where(c => c.CountryId == countryId).ToList();
        }
    }
}
