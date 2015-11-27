using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.model
{
    public class TransPortEnvironmentData : DataFromArduino
    {
        public ObjectId Id { get; set; }

        public string busno { get; set; }

        public string lineno { get; set; }
        
        public double thi { get; set; }//불쾌지수

        public double co2_density { get; set; } //Co2농도

        public DateTime getTime { get; set; }

        public TransPortEnvironmentData(string busno, string lineno, double temperature, double humidity, double co2, DateTime getTime, double thi, double co2_density)
            : base(temperature, humidity, co2)
        {
            this.busno = busno;
            this.lineno = lineno;
            this.getTime = getTime;
            this.thi = thi;
            this.co2_density = co2_density;
        }

        public TransPortEnvironmentData(BusModel model, DataFromArduino data,DateTime getTime)
            : base(data.temperature, data.humidity, data.co2)
        {

            busno = model.busno;
            lineno = model.lineno;
            thi = model.thi;
            co2_density = model.co2;
            this.getTime = getTime;
        }

        override
        public String ToString()
        {
            string result = JsonConvert.SerializeObject(this);

            return result;
        }
    }
}