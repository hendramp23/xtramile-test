using Microsoft.Extensions.DependencyInjection;
using Xtramile.WheatherApp.Repository;
using Xtramile.WheatherApp.RepositoryContract;
using Xtramile.WheatherApp.Service;
using Xtramile.WheatherApp.ServiceContract;

namespace Xtramile.WheatherApp
{
    public class Bootstrapper
    {
        public static void SetupRepository(IServiceCollection service)
        {
            service.AddTransient<ICountryRepository, CountryRepository>();
            service.AddTransient<ICityRepository, CityRepository>();
        }

        public static void SetupServices(IServiceCollection service)
        {
            service.AddTransient<IApiManager, ApiManager>();
            service.AddTransient<ICountryService, CountryService>();
            service.AddTransient<ICityService, CityService>();
            service.AddTransient<IWeatherService, WeatherService>();
        }
    }
}
