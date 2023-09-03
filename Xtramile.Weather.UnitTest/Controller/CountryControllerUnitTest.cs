using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xtramile.WheatherApp.Controllers.v1;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.Service;
using Xtramile.WheatherApp.ServiceContract;
using Xtramile.WheatherApp.ServiceContract.Response;
using Xunit;

namespace Xtramile.Weather.UnitTest.Controller
{
    public class CountryControllerUnitTest
    {
        #region Fields

        private readonly Mock<ICountryService> countryServiceMock;
        private readonly CountryController controller;

        #endregion

        public CountryControllerUnitTest()
        {
            this.countryServiceMock = new Mock<ICountryService>();
            this.controller = new CountryController(this.countryServiceMock.Object);
        }

        #region Test Methods

        [Fact]
        public void GetAll_Valid_ReturnListOfCountry()
        {
            this.PrepareCountryServiceGetAllCountryMock(true);

            var result = this.controller.GetAll();
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.GetAllCountry(), Times.Once());
        }

        [Fact]
        public void GetAll_Invalid_ReturnListOfCountry()
        {
            this.PrepareCountryServiceGetAllCountryMock(false);

            var result = this.controller.GetAll();
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.GetAllCountry(), Times.Once());
        }

        #endregion

        #region Preparation Methods

        private void PrepareCountryServiceGetAllCountryMock(bool isValid)
        {
            var response = new GenericGetDtoCollectionResponse<CountryDto>();

            if (!isValid)
            {
                response.AddErrorMessage("Error message");
                
            }
            else
            {
                var countryList = new List<CountryDto>()
            {
                new CountryDto()
                {
                    Id = "ID",
                    Name = "Indonesia",
                },
                new CountryDto()
                {
                    Id = "SI",
                    Name = "Singapore",
                },
                new CountryDto()
                {
                    Id = "AU",
                    Name = "Australia",
                },
            };

                foreach (var country in countryList)
                {
                    response.DtoCollection.Add(country);
                }
            }

            this.countryServiceMock.Setup(c => c.GetAllCountry())
                .Returns(response);
        }

        #endregion
    }
}
