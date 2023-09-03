using System;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.RepositoryContract;
using Xtramile.WheatherApp.ServiceContract;
using Xtramile.WheatherApp.ServiceContract.Response;

namespace Xtramile.WheatherApp.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public GenericResponse<CityDto> Read(string id)
        {
            var response = new GenericResponse<CityDto>();

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    response.AddErrorMessage("Parameter id cannot be null or empty string");
                    return response;
                }

                var city = this.cityRepository.Read(id);
                if (city == null)
                {
                    response.AddErrorMessage($"City for id {id} cannot be found.");
                    return response;
                }

                response.Data = city;
            }
            catch (Exception ex)
            {

                response.AddErrorMessage(ex.Message);
            }

            return response;
        }

        public GenericGetDtoCollectionResponse<CityDto> GetCityByCountryId(string countryId)
        {
            var response = new GenericGetDtoCollectionResponse<CityDto>();
            try
            {
                if (string.IsNullOrEmpty(countryId))
                {
                    response.AddErrorMessage("Parameter countryId cannot be null or empty string");
                    return response;
                }

                var cities = this.cityRepository.GetCityByCountryId(countryId);
                foreach (var city in cities)
                {
                    response.DtoCollection.Add(city);
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
