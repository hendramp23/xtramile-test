using System.Collections.Generic;
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
    public class CityControllerUnitTest
    {
        #region Fields

        private readonly Mock<ICountryService> countryServiceMock;
        private readonly Mock<ICityService> cityServiceMock;
        private readonly CityController controller;

        #endregion

        public CityControllerUnitTest()
        {
            this.countryServiceMock = new Mock<ICountryService>();
            this.cityServiceMock = new Mock<ICityService>();
            this.controller = new CityController(this.cityServiceMock.Object, this.countryServiceMock.Object);
        }

        #region Test Methods

        [Fact]
        public void GetAllByCountryId_ValidParameter_ReturnCorrectCityData()
        {
            this.PrepareCountryServiceReadMock(true);
            this.PreparationCityServiceGetAllByCountryId(true);

            var result = this.controller.GetAllByCountryId("ID");
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());
            this.cityServiceMock.Verify(c => c.GetCityByCountryId(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void GetAllByCountryId_GivenEmptyStringCountryId_ReturnBadRequestResponse()
        {
            this.PrepareCountryServiceReadMock(false);
            this.PreparationCityServiceGetAllByCountryId(false);

            var result = this.controller.GetAllByCountryId(string.Empty);
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.Read(It.IsAny<string>()), Times.Never());
            this.cityServiceMock.Verify(c => c.GetCityByCountryId(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void GetAllByCountryId_InvalidCountryId_ReturnBadRequestResponse()
        {
            this.PrepareCountryServiceReadMock(false);
            this.PreparationCityServiceGetAllByCountryId(true);

            var result = this.controller.GetAllByCountryId("XYZ");
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());
            this.cityServiceMock.Verify(c => c.GetCityByCountryId(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void GetAllByCountryId_GetCityErrorFoundG_ReturnBadRequestResponse()
        {
            this.PrepareCountryServiceReadMock(true);
            this.PreparationCityServiceGetAllByCountryId(false);

            var result = this.controller.GetAllByCountryId("ID");
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());
            this.cityServiceMock.Verify(c => c.GetCityByCountryId(It.IsAny<string>()), Times.Once());
        }

        #endregion

        #region Preparation Methods

        private void PrepareCountryServiceReadMock(bool isValid)
        {
            var response = new GenericResponse<CountryDto>();

            if (!isValid)
            {
                response.AddErrorMessage("Error message");

            }
            else
            {
                response.Data = new CountryDto()
                {
                    Id = "ID",
                    Name = "Indonesia",
                };

            }

            this.countryServiceMock.Setup(c => c.Read(It.IsAny<string>()))
                .Returns(response);
        }

        private void PreparationCityServiceGetAllByCountryId(bool isValid)
        {
            var response = new GenericGetDtoCollectionResponse<CityDto>();

            if (isValid)
            {
                var cityList = new List<CityDto>()
                {
                    new CityDto()
                    {
                        Id = "DPS",
                        Name = "Denpasar",
                        CountryId = "ID",
                    },
                    new CityDto()
                    {
                        Id = "JKT",
                        Name = "Jakarta",
                        CountryId = "ID",
                    },
                };

                foreach (var city in cityList)
                {
                    response.DtoCollection.Add(city);
                }

                this.cityServiceMock.Setup(item => item.GetCityByCountryId(It.IsAny<string>()))
                    .Returns(response);

            }
            else
            {
                response.AddErrorMessage("Error message");
                this.cityServiceMock.Setup(item => item.GetCityByCountryId(It.IsAny<string>()))
                    .Returns(response);
            }
        }

        #endregion
    }
}
