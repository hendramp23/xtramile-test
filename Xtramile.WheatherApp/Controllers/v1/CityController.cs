using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.Model.Response;
using Xtramile.WheatherApp.ServiceContract;

namespace Xtramile.WheatherApp.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        #region Fields

        private readonly ICityService cityService;
        private readonly ICountryService countryService;


        #endregion

        #region Constructor

        public CityController(ICityService cityService, ICountryService countryService)
        {
            this.cityService = cityService;
            this.countryService = countryService;
        }


        #endregion

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <remarks>Get list city by country id.</remarks>
        /// <param name="id">Country id.</param>
        /// <response code="200">Success Response.</response>
        /// <response code="400">Invalid Request.</response>
        /// <response code="401">Api Key not provided or incorrect.</response>
        /// <response code="500">Exception thrown.</response>
        [HttpGet]
        [Route("/v{version:apiversion}/weather/countries/{id}/cities")]
        public IActionResult GetAllByCountryId([FromRoute][Required] string id)
        {
            if (!IsValidRequest(id))
            {
                return this.GetApiError("Invalid Request", (int)HttpStatusCode.BadRequest);
            }

            var readCountryResponse = this.countryService.Read(id);
            if (readCountryResponse.IsError())
            {
                return this.GetApiError(readCountryResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var cityListResponse = this.cityService.GetCityByCountryId(id);
            if (cityListResponse.IsError())
            {
                return this.GetApiError(cityListResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var cityViewModels = cityListResponse.DtoCollection.Select(c => GetCityViewModel(c)).ToList();

            var response = new CityListViewModel()
            {
                Results = cityViewModels,
            };

            return new OkObjectResult(response);

        }

        #endregion

        #region Private Methods

        private bool IsValidRequest(string id)
        {
            return !string.IsNullOrEmpty(id);
        }

        private CityViewModel GetCityViewModel(CityDto dto)
        {
            return new CityViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                CountryId = dto.CountryId,
            };
        }

        #endregion
    }
}
