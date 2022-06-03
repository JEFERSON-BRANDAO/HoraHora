using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Configuration;
using System.Threading;
using System.IO;

namespace HoraHora.graficos
{
    public partial class Fe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //
            List<Modelos> modeloLista = new List<Modelos>();
            modeloLista = Modelo(data);
            //
            int rowCountLista = modeloLista.Count;
            if (rowCountLista > 0)
            {
                if (int.Parse(lbContagem.Text) < rowCountLista)
                {
                    int row = (int.Parse(lbContagem.Text));
                    //  
                    for (int index = 0; index < rowCountLista; index++)
                    {
                        lbPartNumberDesc.Text = modeloLista[row].DESCRICAO;//descrição
                        string partNumber = modeloLista[row].SKUNO;//skuno

                        string arquivo = ConfigurationManager.AppSettings["fe"] + partNumber + ".png";
                        if (!File.Exists(arquivo))
                        {
                            arquivo = System.AppDomain.CurrentDomain.BaseDirectory + "/Imagens/semInformacao.jpg";
                            imgGrafico.Width = 200;
                        }
                        else
                        {
                            imgGrafico.Width = 886;
                        }

                        imgGrafico.ImageUrl = "~/exibi_imagem.ashx?arquivo=" + arquivo;  //VirtualPathUtility.GetDirectory(@"e976.png");
                        //
                        Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString() + "000"));
                    }

                }
                //
                if (int.Parse(lbContagem.Text) == Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString()))
                {
                    Response.RedirectToRoute("rma");
                }
            }
            else
            {
                if (int.Parse(lbContagem.Text) == Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString()))
                {
                    Response.RedirectToRoute("rma");
                }
            }
            //
            lbContagem.Text = (int.Parse(lbContagem.Text) + 1).ToString();
        }


        public List<Modelos> Modelo(string data)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            List<Modelos> modelo = new List<Modelos>();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string sql = @"SELECT DISTINCT TRIM(REPLACE(REPLACE(REPLACE(TRIM(a.CODENAME), 'SMT',''),'-',''),'PCBA','' )) CODENAME, a.SKUNO
                                     FROM SFCCODELIKE a 
                                     INNER JOIN MFWORKORDER b ON a.SKUNO= b.SKUNO
                                     WHERE a.CATEGORY='PCBA'
                                     AND b.SKUNO IN (SELECT DISTINCT SKUNO FROM  (SELECT SKUNO FROM MFWORKORDER WHERE JOBSTARTED=1 AND CLOSED=0 AND SOFTWARE='SMTLOADING'  ORDER BY WORKORDERDATE DESC))
                                     AND WORKORDERNO IN (SELECT WORKORDERNO FROM SFCUPHRATEDETAIL WHERE uphdate='" + data + "')";


                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    for (int index = 0; index < Objconn.Tabela.Rows.Count; index++)
                    {
                        Modelos item = new Modelos();
                        //
                        item.DESCRICAO = Objconn.Tabela.Rows[index]["CODENAME"].ToString();
                        item.SKUNO = Objconn.Tabela.Rows[index]["SKUNO"].ToString();
                        modelo.Add(item);
                    }
                }

            }
            finally
            {
                Objconn.Desconectar();
            }
            //
            return modelo;
        }

        public class Modelos
        {
            public string DESCRICAO { get; set; }
            public string SKUNO { get; set; }
        }
    }
}