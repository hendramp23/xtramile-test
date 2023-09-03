import React, { useEffect, useState } from "react";
import Select from 'react-select';
import WeatherService from '../api/weather-service';
import WeatherForm from '../components/WeatherForm';

export default function WeatherScreen(){
    const [countries, setCountries] = useState([]);
    const [cities, setCities] = useState([]);
    const [weatherData, setWeatherData] = useState({
        showData: false,
        data: {}
    });
    const [isLoading, setIsLoading] = useState(true);

    const weatherService = new WeatherService();

    const resetData = () => {
        setCities([]);
        setWeatherData({
            showData: false,
            data:{},
        });
    }

    const getCountryList = () => {
        weatherService.GetAllCountry()
        .then((response) => {

            const countrySelectList = response.data.results.map((item) => {
                return {
                    value: item.id,
                    label: item.name,
                }
            });

            setCountries(countrySelectList);
            setIsLoading(false);
        });
    }

    const getCityList = (countryId) => {
        weatherService.GetCityByCountryId(countryId)
        .then((response) => {

            const citySelectList = response.data.results.map((item) => {
                return {
                    value: item.id,
                    label: item.name,
                }
            });

            setCities(citySelectList);
        });
    }

    const getWeatherData = (cityId) => {
        weatherService.GetWeatherDataByCityId(cityId)
        .then((response) => {
            setWeatherData({
                showData: true,
                data: response.data,
            });
        });
    }

    const onCountrySelectListChanged = (e) => {
        resetData();
        getCityList(e.value);
    }

    const onCitySelectListChanged = (e) => {
        getWeatherData(e.value);
    }

    useEffect(() => {
        getCountryList();
    }, []);

    if(isLoading)
    {
        return <p>loading.... </p>
    }

    return(
        <>
            <h1>Xtramile Weather App</h1>
            <section>
                <Select
                    options={countries}
                    onChange={(r) => {
                        onCountrySelectListChanged(r);
                    }}
                />
                <Select
                    options={cities}
                    onChange={(r) => {
                        onCitySelectListChanged(r);
                    }}
                />
            </section>
            <section>
                <WeatherForm data={weatherData} />
            </section>
        </>
    );
}