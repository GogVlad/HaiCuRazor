using System;
using System.Collections.Generic;

namespace RazorMVC.WebAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public double TemperatureC
         {
             get { return Math.Round(TemperatureK - 273.15); }
         }

        public string Summary { get; set; }

        public double TemperatureK { get; set; }
    }
}
