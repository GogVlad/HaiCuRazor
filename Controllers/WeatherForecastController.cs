﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace RazorMVC.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly double lat;
        private readonly double lon;
        private readonly string apiKey;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.lat = double.Parse(configuration["WeatherForecast:Latitude"], CultureInfo.InvariantCulture);
            this.lon = double.Parse(configuration["WeatherForecast:Longitude"], CultureInfo.InvariantCulture);
            this.apiKey = configuration["WeatherForecast:ApiKey"];
        }

        /// <summary>
        /// Getting weather forecast for five days.
        /// </summary>
        /// <returns>Enumerable of weatherForecast objects.</returns>
        [HttpGet]
        public List<WeatherForecast> Get()
        {
            var weatherForecasts = Get(lat,lon);

            return weatherForecasts.GetRange(1, 5);
        }

        [HttpGet("/forecast")]

        public List<WeatherForecast> Get(double lat, double lon)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return ConvertResponseContentToWeatherForecasts(response.Content);
        }

        [NonAction]

        public List<WeatherForecast> ConvertResponseContentToWeatherForecasts(string content)
        {
            JToken root = JObject.Parse(content);
            JToken testToken = root["daily"];
            List<WeatherForecast> forecasts = new List<WeatherForecast>();
            foreach (var token in testToken)
            {
                forecasts.Add(new WeatherForecast
                {
                    Date = RazorMvc.Utilities.DateTimeConverter.ConvertEpochToDateTime(long.Parse(token["dt"].ToString())),
                    TemperatureK = double.Parse(token["temp"]["day"].ToString()),
                    Summary = token["weather"][0]["description"].ToString(),
                });
            }

            return forecasts;
        }
    }
}
