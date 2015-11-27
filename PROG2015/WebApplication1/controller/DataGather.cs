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

namespace WebApplication1.model
{
    public class DataGather
    {
        public BusModel bus { get; set; }

        public DataGather(BusModel bus)
        {
            this.bus = bus;
        }

        public Boolean gatherFromArduino()
        {
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(bus.busURL);
            string responseJSON = new StreamReader(stream).ReadToEnd();

            DataFromArduino data = JsonConvert.DeserializeObject<DataFromArduino>(responseJSON);
            //여기까지 아두이노에서 JSON데이터 받는 단계


            //데이터 베이스로 전송
            //받은 데이터에 현재 시간을 추가.
            TransPortEnvironmentData sendData = new TransPortEnvironmentData(bus, data, DateTime.Now);
            
            //MongoDB와 연결
            string connString = ServerValues.MongoDBServer;

            MongoClient cli = new MongoClient(connString);
            MongoDatabase testdb = cli.GetServer().GetDatabase(ServerValues.MongoDBDataBase);
            
            var testtable = testdb.GetCollection<TransPortEnvironmentData>(ServerValues.MongoDBCollection);

            //삽입
            testtable.Insert(sendData);
            ObjectId id = sendData.Id;

            IMongoQuery query = Query.EQ("_id", id);
            var result = testtable.Find(query).SingleOrDefault();
            stream.Close();

            //제대로 MongoDB에 올라갔다면
            if (result != null)
            {
                return true;
            }
            //제대로 MongoDB에 올라가지 않았다면
            else
            {
                return false;
            }

            
        }

        public Boolean MannualUpdateJSON(string json)
        {
           
            DataFromArduino data = JsonConvert.DeserializeObject<DataFromArduino>(json);
            //여기까지 아두이노에서 JSON데이터 받는 단계


            //데이터 베이스로 전송
            //받은 데이터에 현재 시간을 추가.
            TransPortEnvironmentData sendData = new TransPortEnvironmentData(bus, data, DateTime.Now);

            //MongoDB와 연결
            string connString = ServerValues.MongoDBServer;

            MongoClient cli = new MongoClient(connString);
            MongoDatabase testdb = cli.GetServer().GetDatabase("PROGProject2015");

            var testtable = testdb.GetCollection<TransPortEnvironmentData>("datafrombus");

            //삽입
            testtable.Insert(sendData);
            ObjectId id = sendData.Id;

            IMongoQuery query = Query.EQ("_id", id);
            var result = testtable.Find(query).SingleOrDefault();

            //제대로 MongoDB에 올라갔다면
            if (result != null)
            {
                return true;
            }
            //제대로 MongoDB에 올라가지 않았다면
            else
            {
                return false;
            }
        }

    }
}