using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.controller;
using WebApplication1.model;

namespace WebApplication1
{
    public partial class getData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string busno = Request["busno"];

            string judge = Request["judge"];

            GetDataSingleController getData = new GetDataSingleController(busno);

            string result = "";

            if(judge == null || judge.Length < 1)
            {
                judge = "no";
            }
            if(judge.ToLower().Equals("yes"))
            {
                result = getData.getJSONDataWithJudge();
            }
            else
            {
                result = getData.getJSONData();
            }

            if(result.Equals("Error1"))
            {
                Response.Write("Enter The Bus Number!");
            }
            else if(result.Equals("Error2"))
            {
                Response.Write("Wrong Bus Number!");
            }
            else if(result.Equals("Error3"))
            {
                Response.Write("Sync Time With The Arduino");
            }
            else
            {
                Response.Write(result);
            }

        }
    }
}