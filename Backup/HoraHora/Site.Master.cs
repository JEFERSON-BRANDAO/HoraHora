using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Net;
using System.Configuration;
// ===============================
// AUTHOR       : JEFFERSON BRANDÃO DA COSTA - ANALISTA/PROGRAMADOR
// CREATE DATE  : 12/09/2019
// DESCRIPTION  : Sistema hora a hora
// SPECIAL NOTES:
// ===============================
// Change History: Incluir visualização produção BE ROKU NEMO
// Date: 07/16/2020
//==================================

namespace HoraHora
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);//limpa cache das paginas     
            //
            int anoCriacao = 2019;
            int anoAtual = DateTime.Now.Year;
            string texto = anoCriacao == anoAtual ? " Foxconn CNSBG All Rights Reserved." : "-" + anoAtual + " Foxconn CNSBG All Rights Reserved.";
            lbRodape.Text = "Copyright © " + anoCriacao + texto + " v1.0.0.4";            
            lbHora.Text = DateTime.Now.ToLongTimeString();            
            //string ipCliente = Request.UserHostAddress;
            string linha = ConfigurationManager.AppSettings["linha"].ToString().Trim();
            //          
            lbIp.Text = linha;
        }
    }
}
