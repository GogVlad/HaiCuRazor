using System;
using System.Collections.Generic;

namespace RazorMVC.WebAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public double TemperatureC { get; set; }
        //Acces violation
        /* {
             get { return TemperatureK - 273.15; }
             set { TemperatureC = value; }
         }*/

        public string Summary { get; set; }

        public double TemperatureK { get; set; }
    }
}
