using System;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.RepositoryContract;
using Xtramile.WheatherApp.ServiceContract;
using Xtramile.WheatherApp.ServiceContract.Response;

namespace Xtramile.WheatherApp.Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public GenericResponse<CountryDto> Read(string id)
        {
            var response = new GenericResponse<CountryDto>();

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    response.AddErrorMessage("Parameter id cannot be null or empty string");
                    return response;
                }

                var country = this.countryRepository.Read(id);
                if (country == null)
                {
                    response.AddErrorMessage($"Country for id {id} cannot be found.");
                    return response;
                }

                response.Data = country;
            }
            catch (Exception ex)
            {

                response.AddErrorMessage(ex.Message);
            }

            return response;
        }

        public GenericGetDtoCollectionResponse<CountryDto> GetAllCountry()
        {
            var response = new GenericGetDtoCollectionResponse<CountryDto>();
            try
            {
                var countries = this.countryRepository.GetAllCountry();
                foreach ( var country in countries )
                {
                    response.DtoCollection.Add( country );
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessage(ex.Message);
            }

            return response;
        }
    }
}
