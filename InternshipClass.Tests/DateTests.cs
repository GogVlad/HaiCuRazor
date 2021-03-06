using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using RazorMvc.Utilities;
using RazorMVC.WebAPI;
using RazorMVC.WebAPI.Controllers;
using System;
using System.IO;
using Xunit;

namespace InternshipClass.Tests
{
    public class DateTests
    {
        private IConfigurationRoot configuration;
        public DateTests()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
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
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.Get();
            WeatherForecast weatherForcastForTomorrow = weatherForecasts[1];

            // Assert
            Assert.Equal(5, weatherForecasts.Count);

        }

        private WeatherForecastController InstantiateWeatherForecastController()
        {
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger, configuration);
            return weatherForecastController;
        }

        [Fact]
        public void ConvertWeatherJSONToWeatherForecast()
        {
            // Assume
            //https://api.openweathermap.org/data/2.5/onecall?lat=45.75&lon=25.3333&exclude=hourly,minutely&appid=5e2f591282908129a5688c6af52aa490

            var content = GetStreamLines();
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();
            // Act
            var weatherForecasts = weatherForecastController.ConvertResponseContentToWeatherForecasts(content);
            WeatherForecast weatherForcastForTomorrow = weatherForecasts[1];

            // Assert
            Assert.Equal(285.39, weatherForcastForTomorrow.TemperatureK);

        }

        [Fact (Skip ="No exception")]
        public void ShouldHandleJsonErrorFromWeatherApi()
        {
            //Assume
            string content = GetStreamLines();
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            //Act

            //Assert
            Assert.Throws<Exception>(() => weatherForecastController.ConvertResponseContentToWeatherForecasts(content));
        }

        private string GetStreamLines()
        {
            var assembly = this.GetType().Assembly;
            using var stream = assembly.GetManifestResourceStream("InternshipClass.Tests.weatherForecast.json");
            StreamReader streamReader = new StreamReader(stream);
            var streamReaderLines = "";

            while (!streamReader.EndOfStream)
            {
                streamReaderLines += streamReader.ReadLine();
            }
            return streamReaderLines;
        }
    }


}
