using Moq;
using Xtramile.WheatherApp.Service;
using Xtramile.WheatherApp.ServiceContract;
using Xtramile.WheatherApp.ServiceContract.Response;
using Xunit;

namespace Xtramile.Weather.UnitTest.Service
{
    public class WeatherServiceUnitTest
    {
        #region Fields

        private readonly Mock<IApiManager> apiManagerMock;
        private readonly WeatherService service;

        #endregion

        public WeatherServiceUnitTest()
        {
            this.apiManagerMock = new Mock<IApiManager>();
            this.service = new WeatherService(this.apiManagerMock.Object);
        }

        #region Test Methods

        [Fact]
        public void GetCityWeatherByCityName_GivenValidCityName_ReturnCorrectValue()
        {
            this.PrepareApiManagerSendRequestAsync(true);

            var result = this.service.GetCityWeatherByCityName("Jakarta");

            Assert.NotNull(result.Data);
            Assert.Equal("ID", result.Data.Country);
            this.apiManagerMock.Verify(c => c.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public void GetCityWeatherByCityName_GivenValidCityName_ReturnCorrectCelciusTemperature()
        {
            decimal fahrenheitTemperature = 212;
            decimal celciusTemperature = 100;
            this.PrepareApiManagerSendRequestAsync(true, fahrenheitTemperature);

            var result = this.service.GetCityWeatherByCityName("Jakarta");

            Assert.NotNull(result.Data);
            Assert.Equal(celciusTemperature, result.Data.TemperatureCelcius);
            this.apiManagerMock.Verify(c => c.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public void GetCityWeatherByCityName_GivenInValidCityName_ReturnErrorMessage()
        {
            this.PrepareApiManagerSendRequestAsync(false);

            var result = this.service.GetCityWeatherByCityName("XYZ");

            Assert.Null(result.Data);
            Assert.Equal("city not found", result.GetErrorMessage());
            this.apiManagerMock.Verify(c => c.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

        }

        #endregion

        #region Preparation Methods

        private void PrepareApiManagerSendRequestAsync(bool isValid, decimal temperatureInFahrenheit = 32)
        {
            if (isValid)
            {
                var validResponse = new GenericResponse<string>()
                {
                    Data = "{\"coord\":{\"lon\":106.8451,\"lat\":-6.2146},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04n\"}],\"base\":\"stations\",\"main\":{\"temp\":"+temperatureInFahrenheit+",\"feels_like\":81.72,\"temp_min\":76.89,\"temp_max\":82.29,\"pressure\":1010,\"humidity\":59,\"sea_level\":1010,\"grnd_level\":1008},\"visibility\":10000,\"wind\":{\"speed\":3,\"deg\":74,\"gust\":4.14},\"clouds\":{\"all\":86},\"dt\":1693690191,\"sys\":{\"type\":2,\"id\":2033644,\"country\":\"ID\",\"sunrise\":1693695149,\"sunset\":1693738344},\"timezone\":25200,\"id\":1642911,\"name\":\"Jakarta\",\"cod\":200}"
                };

                this.apiManagerMock.Setup(a => a.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(validResponse);
            }
            else
            {
                var responseWithError = new GenericResponse<string>();
                responseWithError.AddErrorMessage("city not found");

                this.apiManagerMock.Setup(a => a.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(responseWithError);
            }
        }

        #endregion
    }
}
