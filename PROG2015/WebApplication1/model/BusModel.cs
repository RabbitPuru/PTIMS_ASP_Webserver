using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.model
{
    public class BusModel
    {
        public ObjectId Id { get; set; }

        public string busno { get; set; } // 버스 차량 번호
        public string lineno { get; set; } // 버스 노선 번호
        public string busURL { get; set; } // 아두이노 단말기의 URL

        public double thi { get; set; } //기준 불쾌지수
        public double co2 { get; set; } //기준 CO2
        
        public BusModel(string busno, string lineno, string busURL, double thi, double co2)
        {
            this.busno = busno;
            this.lineno = lineno;
            this.busURL = busURL;
            this.thi = thi;
            this.co2 = co2;
        }

    }
}