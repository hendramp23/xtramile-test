using System.Collections.Generic;
using System.Linq;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.RepositoryContract;

namespace Xtramile.WheatherApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly List<CountryDto> countries;

        public CountryRepository()
        {
            this.countries = new List<CountryDto>()
            {
                new CountryDto()
                {
                    Id = "IDN",
                    Name = "Indonesia",
                },
                new CountryDto()
                {
                    Id = "AUS",
                    Name = "Australia",
                },
                new CountryDto()
                {
                    Id = "SIN",
                    Name = "Singapore",
                },
            };
        }

        public CountryDto Read(string id)
        {
            return countries.FirstOrDefault(c => c.Id == id);
        }

        public List<CountryDto> GetAllCountry()
        {
            return this.countries;


        }
    }
}
