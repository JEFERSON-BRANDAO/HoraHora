using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.NetworkInformation;

namespace HoraHora
{
    public partial class Erro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ping = new Ping();
            var reply = ping.Send("10.19.215.201");
            //
            if (reply.Status == IPStatus.Success)
            {
                lbStatus.Text = "Terminal Online";
            }
            else
            {
                lbStatus.Text = "Terminal Offline";
            }

            Response.AppendHeader("Refresh",
                //Session TimeOut é em minutos e o Refresh e segundos, por isso o Session. Timeout em  segundos
            String.Concat(((1200 - 1200) + 0005),
                //Página para onde o usuário será redirecionado
            ";URL=/Home"));
        }
    }
}