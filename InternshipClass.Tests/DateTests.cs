using Microsoft.Extensions.Logging.Abstractions;
using RazorMvc.Utilities;
using RazorMVC.WebAPI;
using RazorMVC.WebAPI.Controllers;
using System;
using Xunit;

namespace InternshipClass.Tests
{
    public class DateTests
    {
        [Fact]
        public void CheckEpochConversion()
        {
            // Assume
            long ticks = 1617184800;


            // Act
            DateTime dateTime = DateTimeConverter.ConvertEpochToDateTime(ticks);

            // Assert
            Assert.Equal(2021, dateTime.Year);
            Assert.Equal(03, dateTime.Month);
            Assert.Equal(31, dateTime.Day);
        }

        [Fact]
        public void ConvertOutputOfWeatherAPIToWeatherForecast()
        {
            // Assume
            //https://api.openweathermap.org/data/2.5/onecall?lat=45.75&lon=25.3333&exclude=hourly,minutely&appid=5e2f591282908129a5688c6af52aa490
            var lat = 45.75;
            var lon = 25.3333;
            var APIKey = "5e2f591282908129a5688c6af52aa490";
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger);

            // Act
            var weatherForecasts = weatherForecastController.FetchWeatherForecasts(lat,lon,APIKey);
            WeatherForecast weatherForcastForTomorrow = weatherForecasts[1];

            // Assert
            Assert.Equal(286.82, weatherForcastForTomorrow.TemperatureK);

        }

    }


}
