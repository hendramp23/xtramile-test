using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.Model.Response;
using Xtramile.WheatherApp.ServiceContract;

namespace Xtramile.WheatherApp.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : BaseController
    {
        #region Fields

        private readonly IWeatherService weatherService;
        private readonly ICityService cityService;


        #endregion

        #region Constructor

        public DataController(IWeatherService weatherService, ICityService cityService)
        {
            this.weatherService = weatherService;
            this.cityService = cityService;
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
        [Route("/v{version:apiversion}/weather/cities/{id}/data")]
        public IActionResult GetByCityId([FromRoute][Required] string id)
        {
            if (!IsValidRequest(id))
            {
                return this.GetApiError("Invalid Request", (int)HttpStatusCode.BadRequest);
            }

            var cityReadResponse = this.cityService.Read(id);
            if (cityReadResponse.IsError())
            {
                return this.GetApiError(cityReadResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var weatherResponse = this.weatherService.GetCityWeatherByCityName(cityReadResponse.Data.Name);
            if (weatherResponse.IsError())
            {
                return this.GetApiError(weatherResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var response = GetWeatherViewModel(weatherResponse.Data);

            return new OkObjectResult(response);

        }

        #endregion

        #region Private Methods

        private bool IsValidRequest(string id)
        {
            return !string.IsNullOrEmpty(id);
        }

        private WeatherViewModel GetWeatherViewModel(WeatherDataDto data)
        {
            return new WeatherViewModel()
            {
                City = data.City,
                Country = data.Country,
                DewPoint = data.DewPoint,
                Humidity = data.Humidity,
                Location = data.Location,
                Pressure = data.Pressure,
                SkyCondition = data.SkyCondition,
                TemperatureCelcius = String.Format("{0:0.00}", data.TemperatureCelcius),
                TemperatureFahrenheit = String.Format("{0:0.00}", data.TemperatureFahrenheit),
                Time = data.Time,
                Visibility = data.Visibility,
                Wind = data.Wind,
                WindDirection = data.WindDirection,
            };
        }

        #endregion
    }
}
