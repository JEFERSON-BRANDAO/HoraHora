using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;
using System.Net;

namespace HoraHora
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbTimer.Text = (int.Parse(lbTimer.Text) + 1).ToString();
            //
            if (int.Parse(lbTimer.Text) >= Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString()))
            {
                string linha = ConfigurationManager.AppSettings["linha"].ToString();
                {
                    Response.RedirectToRoute("desempenho_bot");
                }

            }
        }

    }
}