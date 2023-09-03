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
    public class CountryController : BaseController
    {
        #region Fields

        private readonly ICountryService countryService;


        #endregion

        #region Constructor

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <remarks>Get List of country.</remarks>
        /// <response code="200">Success Response.</response>
        /// <response code="400">Invalid Request.</response>
        /// <response code="401">Api Key not provided or incorrect.</response>
        /// <response code="500">Exception thrown.</response>
        [HttpGet]
        [Route("/v{version:apiversion}/weather/countries")]
        public IActionResult GetAll()
        {
            var countryListResponse = this.countryService.GetAllCountry();
            if (countryListResponse.IsError())
            {
                return this.GetApiError(countryListResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var countryViewModels = countryListResponse.DtoCollection.Select(c => GetCountryViewModel(c)).ToList();

            var response = new CountryListViewModel()
            {
                Results = countryViewModels,
            };

            return new OkObjectResult(response);

        }

        #endregion

        #region Private Methods

        private CountryViewModel GetCountryViewModel(CountryDto dto)
        {
            return new CountryViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        #endregion
    }
}
