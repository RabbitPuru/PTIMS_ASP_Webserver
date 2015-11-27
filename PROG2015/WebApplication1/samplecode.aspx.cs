
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.model;

namespace WebApplication1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Arduino로 부터 데이터를 가져옴
            /*string uri = "http://192.168.0.12";
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(uri);
            string responseJSON = new StreamReader(stream).ReadToEnd();
            */

            string responseJSON = "{\"busno\":\"8182\",\"lineno\":\"937\",\"temperature\":\"30.00\",\"humidity\":\"33.00\",\"co2\":\"400 \"}";

            Response.Write("아두이노에서 가져온 데이터<br>");
            Response.Write(responseJSON);

            

            //JSON으로 데이터 추출.
            //클래스를 JSON에 맞게 만들어서 집어 넣어야함.
            DataFromArduino data = JsonConvert.DeserializeObject<DataFromArduino>(responseJSON);
            Response.Write("<br>현재 온도 : " + data.temperature + "°C");
            Response.Write("<br>현재 습도 : " + data.humidity + "%");
            Response.Write("<br>현재 CO2 농도 : ");
            if(data.co2 <= 400)
            {
                Response.Write(" 400 ppm 이하");
            }
            else
            {
                Response.Write(data.co2 + "ppm");
            }
            /*
            //서버 전송을 위하여 JSON형식으로 만들기
            TransPortEnvironmentData sendData = new TransPortEnvironmentData(data, DateTime.Now);
            string sendDataToJSON = JsonConvert.SerializeObject(sendData);

            Response.Write("<br><br>DB전송, 어플리케이션 활용을 위한 JSON<br>");
            Response.Write(sendDataToJSON);

            string connString = "mongodb://localhost";

            MongoClient cli = new MongoClient(connString);
            MongoDatabase testdb = cli.GetServer().GetDatabase("PROGProject2015");

            var testtable = testdb.GetCollection<TransPortEnvironmentData>("datafrombus");

            testtable.Insert(sendData);
            ObjectId id = sendData.Id;
            
            

            IMongoQuery query = Query.EQ("_id", id);
            var result = testtable.Find(query).SingleOrDefault();

            if (result != null)
            {
                Response.Write("<br>"+ result.ToString());
            }

            var min = testtable.FindAll().SetSortOrder(SortBy.Ascending("getTime")).SetLimit(1).FirstOrDefault();
            if (min != null)
            {
                Response.Write("<br>" + min.ToString());
            }
            */
            /*
            var min = collection.FindAll().SetSortOrder(SortBy.Ascending("val")).SetLimit(1).FirstOrDefault();
            var max = collection.FindAll().SetSortOrder(SortBy.Descending("val")).SetLimit(1).FirstOrDefault();
            */

        }
    }
}