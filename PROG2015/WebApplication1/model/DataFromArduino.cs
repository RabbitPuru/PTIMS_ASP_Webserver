using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.model
{


    public class DataFromArduino
    {
       
        public double temperature { get; set; } //온도
        public double humidity { get; set; } // 습도
        public double co2 { get; set; }// CO2농도
        
        public DataFromArduino(double temperature, double humidity, double co2)
        {
            this.temperature = temperature;
            this.humidity = humidity;
            this.co2 = co2;
        }
    }
}