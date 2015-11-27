using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.model
{
    public class ServerValues
    {
        public static string MongoDBServer = "mongodb://localhost";

        public static string MongoDBDataBase = "PROGProject2015";

        public static string MongoDBCollection = "datafrombusarduino";

        public static double GMT_KOR = 9.0;
        public static double Error_Hour = 0.5;//30min
    }
}