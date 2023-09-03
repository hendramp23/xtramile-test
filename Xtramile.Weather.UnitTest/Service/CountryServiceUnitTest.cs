using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xtramile.WheatherApp.Dto;
using Xtramile.WheatherApp.RepositoryContract;
using Xtramile.WheatherApp.Service;
using Xunit;

namespace Xtramile.Weather.UnitTest.Service
{
    public class CountryServiceUnitTest
    {
        #region Fields

        private readonly Mock<ICountryRepository> countryRepositoryMock;
        private readonly CountryService service;

        #endregion

        public CountryServiceUnitTest()
        {
            this.countryRepositoryMock = new Mock<ICountryRepository>();
            this.service = new CountryService(this.countryRepositoryMock.Object);
        }

        #region Test Methods

        #region Read

        [Fact]
        public void Read_GivenValidCountryId_ReturnCorrectValue()
        {
            this.PreparationCountryRepositoryRead(true);

            var result = this.service.Read("ID");

            Assert.NotNull(result.Data);
            Assert.Equal("Indonesia", result.Data.Name);
            this.countryRepositoryMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public void Read_GivenInvalidCountryId_ReturnParameterCannotEmptyMessage()
        {
            this.PreparationCountryRepositoryRead(false);

            var result = this.service.Read(string.Empty);

            Assert.Null(result.Data);
            Assert.Equal("Parameter id cannot be null or empty string", result.GetErrorMessage());
            this.countryRepositoryMock.Verify(c => c.Read(string.Empty), Times.Never());
        }

        [Fact]
        public void Read_GivenNotExistsCountryId_ReturnNull()
        {
            this.PreparationCountryRepositoryRead(false);

            var result = this.service.Read("UK");

            Assert.Null(result.Data);
            Assert.Equal("Country for id UK cannot be found.", result.GetErrorMessage());
            this.countryRepositoryMock.Verify(c => c.Read(It.IsAny<string>()), Times.Once());
        }

        #endregion

        #region GetAllCountry

        [Fact]
        public void GetAllCountry_NoParameter_ReturnListOfCountry()
        {
            this.PreparationCountryRepositoryGetAllCountry();

            var result = this.service.GetAllCountry();

            Assert.NotNull(result.DtoCollection);
            Assert.True(result.DtoCollection.Count > 1);
            this.countryRepositoryMock.Verify(c => c.GetAllCountry(), Times.Once());

        }

        #endregion

        #endregion

        #region Setup Methods

        private void PreparationCountryRepositoryRead(bool isValid)
        {
            if (isValid)
            {
                var countryDto = new CountryDto()
                {
                    Id = "ID",
                    Name = "Indonesia",
                };
                this.countryRepositoryMock.Setup(item => item.Read(It.IsAny<string>()))
                    .Returns(countryDto);
            }
            else
            {
                this.countryRepositoryMock.Setup(item => item.Read(It.IsAny<string>()))
                    .Returns((CountryDto)null);
            }
        }

        private void PreparationCountryRepositoryGetAllCountry()
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

            this.countryRepositoryMock.Setup(item => item.GetAllCountry())
                    .Returns(countryList);
        }


        #endregion
    }
}
