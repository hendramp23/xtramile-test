using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xtramile.WheatherApp.Controllers.v1;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.ServiceContract;
using Xtramile.WheatherApp.ServiceContract.Response;
using Xunit;

namespace Xtramile.Weather.UnitTest.Controller
{
    public class DataControllerUnitTest
    {
        #region Fields

        private readonly Mock<IWeatherService> weatherServiceMock;
        private readonly Mock<ICityService> cityServiceMock;
        private readonly DataController controller;

        #endregion

        public DataControllerUnitTest()
        {
            this.weatherServiceMock = new Mock<IWeatherService>();
            this.cityServiceMock = new Mock<ICityService>();
            this.controller = new DataController(this.weatherServiceMock.Object, this.cityServiceMock.Object);
        }

        #region Test Methods

        [Fact]
        public void GetCityWeatherByCityName_ValidParameter_ReturnCorrectWeatherData()
        {
            this.PrepareCityServiceReadMock(true);
            this.PreparationWeatherServiceGetCityWeatherByCityNameMock(true);

            var result = this.controller.GetByCityId("DPS");
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            this.cityServiceMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());
            this.weatherServiceMock.Verify(c => c.GetCityWeatherByCityName(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void GetCityWeatherByCityName_GivenEmptyStringCountryId_ReturnBadRequestResponse()
        {
            this.PrepareCityServiceReadMock(false);
            this.PreparationWeatherServiceGetCityWeatherByCityNameMock(false);

            var result = this.controller.GetByCityId(string.Empty);
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.cityServiceMock.Verify(c => c.Read(It.IsAny<string>()), Times.Never());
            this.weatherServiceMock.Verify(c => c.GetCityWeatherByCityName(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void GetCityWeatherByCityName_InvalidCityId_ReturnBadRequestResponse()
        {
            this.PrepareCityServiceReadMock(false);
            this.PreparationWeatherServiceGetCityWeatherByCityNameMock(true);

            var result = this.controller.GetByCityId("XYZ");
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.cityServiceMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());
            this.weatherServiceMock.Verify(c => c.GetCityWeatherByCityName(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void GetCityWeatherByCityName_GetWeatherErrorFound_ReturnBadRequestResponse()
        {
            this.PrepareCityServiceReadMock(true);
            this.PreparationWeatherServiceGetCityWeatherByCityNameMock(false);

            var result = this.controller.GetByCityId("ID");
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.cityServiceMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());
            this.weatherServiceMock.Verify(c => c.GetCityWeatherByCityName(It.IsAny<string>()), Times.Once());
        }

        #endregion

        #region Preparation Methods

        private void PrepareCityServiceReadMock(bool isValid)
        {
            var response = new GenericResponse<CityDto>();

            if (!isValid)
            {
                response.AddErrorMessage("Error message");

            }
            else
            {
                response.Data = new CityDto()
                {
                    Id = "DPS",
                    Name = "Denpasar",
                    CountryId = "ID",
                };

            }

            this.cityServiceMock.Setup(c => c.Read(It.IsAny<string>()))
                .Returns(response);
        }

        private void PreparationWeatherServiceGetCityWeatherByCityNameMock(bool isValid)
        {
            var response = new GenericResponse<WeatherDataDto>();

            if (isValid)
            {
                var weatherData = new WeatherDataDto()
                {
                    Humidity = 100,
                    SkyCondition = "clouds"
                };

                response.Data = weatherData;

                this.weatherServiceMock.Setup(item => item.GetCityWeatherByCityName(It.IsAny<string>()))
                    .Returns(response);

            }
            else
            {
                response.AddErrorMessage("Error message");
                this.weatherServiceMock.Setup(item => item.GetCityWeatherByCityName(It.IsAny<string>()))
                    .Returns(response);
            }
        }

        #endregion
    }
}
