using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.model;

namespace WebApplication1.controller
{
    public class GetDataSingleController
    {
        public string busno { get; set; }
        public GetDataSingleController(string busno)
        {
            this.busno = busno;
        }

        public string getJSONData()
        {
            string connString = ServerValues.MongoDBServer;

            if (busno == null || busno.Length < 1) // 버스 번호가 null이거나 없을 때
            {
                return("Error1");
            }
            else
            {
                MongoClient cli = new MongoClient(connString);
                MongoDatabase testdb = cli.GetServer().GetDatabase(ServerValues.MongoDBDataBase);

                var testtable = testdb.GetCollection<TransPortEnvironmentData>(ServerValues.MongoDBCollection);


                //해당 버스 번호의 데이터 중 최신의 것을 가져옴.
                IMongoQuery query = Query.EQ("busno", busno);
                var max = testtable.Find(query).SetSortOrder(SortBy.Descending("getTime")).SetLimit(1).FirstOrDefault();

                TimeSpan differHour = DateTime.Now -max.getTime ;
                
                if(differHour.TotalHours - ServerValues.GMT_KOR < ServerValues.Error_Hour) //30분내 오차
                {
                    if (max != null)
                    {
                        //return (differHour.TotalHours.ToString());
                        return (max.ToString());
                    }
                    else // 결과가 나오지 않을 때
                    {
                        return ("Error2");
                    }
                }
                else
                {
                    return ("Error3");
                }
                
                
            }
        }

        public double getTHI(double temperature, double humidity)
        {
            double result = 0;

            humidity /= 100;

            double T = temperature * 9 / 5;

            result = T - (0.55 * (1 - humidity) * (T - 26)) + 32;

            return result;
        }

        public string getJSONDataWithJudge()
        {
            string connString = ServerValues.MongoDBServer;

            if (busno == null || busno.Length < 1) // 버스 번호가 null이거나 없을 때
            {
                return ("Error1");
            }
            else
            {
                MongoClient cli = new MongoClient(connString);
                MongoDatabase testdb = cli.GetServer().GetDatabase(ServerValues.MongoDBDataBase);

                var testtable = testdb.GetCollection<TransPortEnvironmentData>(ServerValues.MongoDBCollection);


                //해당 버스 번호의 데이터 중 최신의 것을 가져옴.
                IMongoQuery query = Query.EQ("busno", busno);
                var max = testtable.Find(query).SetSortOrder(SortBy.Descending("getTime")).SetLimit(1).FirstOrDefault();

                if(max != null)
                {
                    DataWithIntencity result = new DataWithIntencity(max);

                    if (max.co2 > max.co2_density || max.thi < getTHI(max.temperature, max.humidity))
                    {
                        result.setJudge("congestion");
                    }
                    else
                    {
                        result.setJudge("normal");
                    }

                    TimeSpan differHour = DateTime.Now - max.getTime;

                    if (differHour.TotalHours - ServerValues.GMT_KOR < ServerValues.Error_Hour) //30분내 오차
                    {
                        return (result.ToString());
                    }
                    else
                    {
                        return ("Error3");
                    }
                }
                else // 결과가 나오지 않을 때
                {
                    return ("Error2");
                }
                


            }
        }
    }
}