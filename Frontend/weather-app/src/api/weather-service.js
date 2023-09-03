import BaseService from "./base-service";

export default class CountryService extends BaseService {
  constructor() {
    super("weather");
  }
  GetAllCountry(uuid, options, version) {
    return this._Get(
      `${this.baseApiUrl}/${version ?? this.defaultVersion}/${
        this.baseController
      }/countries`,
      options
    );
  }

  GetCityByCountryId(countryId, options, version) {
    return this._Get(
      `${this.baseApiUrl}/${version ?? this.defaultVersion}/${
        this.baseController
      }/countries/${countryId}/cities`,
      options
    );
  }

  GetWeatherDataByCityId(cityId, options, version) {
    return this._Get(
      `${this.baseApiUrl}/${version ?? this.defaultVersion}/${
        this.baseController
      }/cities/${cityId}/data`,
      options
    );
  }
}
