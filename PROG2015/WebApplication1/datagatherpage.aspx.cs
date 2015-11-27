using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.model;

namespace WebApplication1
{
    public partial class datagather : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BusModel s1 = new BusModel("3627", "706", "http://192.168.0.12",80.0, 1772);

            DataGather s1Gather = new DataGather(s1);

            if(s1Gather.gatherFromArduino())
            {
                //Complete
            }
            else
            {
                //Error Log
            }

            BusModel s2 = new BusModel("3612", "706", "http://192.168.0.12", 80.0, 1772);

            DataGather s2Gather = new DataGather(s2);

            if (s2Gather.gatherFromArduino())
            {
                //Complete
            }
            else
            {
                //Error Log
            }

        }
    }
}