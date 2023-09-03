using System.Collections.Generic;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.RepositoryContract;
using Xtramile.WheatherApp.Service;
using Moq;
using Xunit;

namespace Xtramile.Weather.UnitTest.Service
{
    public class CityServiceUnitTest
    {
        #region Fields

        private readonly Mock<ICityRepository> cityRepositoryMock;
        private readonly CityService service;

        #endregion

        public CityServiceUnitTest()
        {
            this.cityRepositoryMock = new Mock<ICityRepository>();
            this.service = new CityService(this.cityRepositoryMock.Object);
        }

        #region Test Methods

        #region Read

        [Fact]
        public void Read_GivenValidCityId_ReturnCorrectValue()
        {
            this.PreparationCityRepositoryRead(true);

            var result = this.service.Read("DPS");

            Assert.NotNull(result.Data);
            Assert.Equal("Denpasar", result.Data.Name);
            this.cityRepositoryMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public void Read_GivenInvalidCityId_ReturnParameterCannotEmptyMessage()
        {
            this.PreparationCityRepositoryRead(false);

            var result = this.service.Read(string.Empty);

            Assert.Null(result.Data);
            Assert.Equal("Parameter id cannot be null or empty string", result.GetErrorMessage());
            this.cityRepositoryMock.Verify(c => c.Read(string.Empty), Times.Never());
        }

        [Fact]
        public void Read_GivenNotExistsCityId_ReturnNull()
        {
            this.PreparationCityRepositoryRead(false);

            var result = this.service.Read("CGK");

            Assert.Null(result.Data);
            Assert.Equal("City for id CGK cannot be found.", result.GetErrorMessage());
            this.cityRepositoryMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());
        }

        #endregion

        #region GetCityByCountryId

        [Fact]
        public void GetCityByCountryId_GivenValidCountryId_ReturnListOfCountry()
        {
            this.PreparationCityRepositoryGetCityByCountryId(true);

            var result = this.service.GetCityByCountryId("ID");

            Assert.NotNull(result.DtoCollection);
            Assert.True(result.DtoCollection.Count > 1);
            this.cityRepositoryMock.Verify(c => c.GetCityByCountryId(It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public void GetCityByCountryId_GivenInvalidCountryId_ReturnParameterCannotEmptyMessage()
        {
            this.PreparationCityRepositoryRead(false);

            var result = this.service.GetCityByCountryId(string.Empty);

            Assert.True(result.DtoCollection.Count == 0);
            Assert.Equal("Parameter countryId cannot be null or empty string", result.GetErrorMessage());
            this.cityRepositoryMock.Verify(c => c.GetCityByCountryId(string.Empty), Times.Never());
        }

        [Fact]
        public void GetCityByCountryId_GivenNotExistsCountryId_ReturnEmptyListOfCountry()
        {
            this.PreparationCityRepositoryGetCityByCountryId(false);

            var result = this.service.GetCityByCountryId("UK");

            Assert.NotNull(result.DtoCollection);
            Assert.True(result.DtoCollection.Count == 0);
            this.cityRepositoryMock.Verify(c => c.GetCityByCountryId(It.IsAny<string>()), Times.Once());

        }

        #endregion

        #endregion

        #region Setup Methods

        private void PreparationCityRepositoryRead(bool isValid)
        {
            if (isValid)
            {
                var cityDto = new CityDto()
                {
                    Id = "DPS",
                    Name = "Denpasar",
                    CountryId = "ID",
                };

                this.cityRepositoryMock.Setup(item => item.Read(It.IsAny<string>()))
                    .Returns(cityDto);
            }
            else
            {
                this.cityRepositoryMock.Setup(item => item.Read(It.IsAny<string>()))
                    .Returns((CityDto)null);
            }
        }

        private void PreparationCityRepositoryGetCityByCountryId(bool isValid)
        {

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

                this.cityRepositoryMock.Setup(item => item.GetCityByCountryId(It.IsAny<string>()))
                    .Returns(cityList);

            }
            else
            {
                this.cityRepositoryMock.Setup(item => item.GetCityByCountryId(It.IsAny<string>()))
                    .Returns(new List<CityDto>());
            }
        }


        #endregion
    }
}
