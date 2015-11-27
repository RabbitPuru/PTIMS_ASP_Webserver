using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.model
{
    public class DataWithIntencity : TransPortEnvironmentData
    {
        public string judge { get; set; }

        public DataWithIntencity(string busno, string lineno, double temperature, double humidity, double co2, DateTime getTime, double thi, double co2_density)
            : base(busno, lineno, temperature, humidity, co2, getTime, thi, co2_density)
        {
            judge = "normal";
        }

        public DataWithIntencity(TransPortEnvironmentData data)
            : base(data.busno, data.lineno, data.temperature, data.humidity, data.co2, data.getTime, data.thi, data.co2_density)
        {
            this.Id = data.Id;
            judge = "normal";
        }

        public string getJudge()
        {
            return judge;
        }

        public void setJudge(string judge)
        {
            this.judge = judge;
        }

        override
        public string ToString()
        {
            string result = JsonConvert.SerializeObject(this);

            return result;
        }

    }
}