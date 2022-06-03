using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Classes;
using System.Threading;
using System.Configuration;

namespace HoraHora
{
    public partial class Aniversariante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> listaAniversariante = new List<string>();
                listaAniversariante = Aniversariantes();
                int rowCount = listaAniversariante.Count;
                //
                if (rowCount > 0)
                {
                    if (rowCount <= 10)//EXIBE LISTA DO MEIO ATÉ 10 NOMES
                    {
                        divListaMeio.Visible = true;
                        //
                        for (int index = 0; index < rowCount; index++)
                        {
                            string conteudoTexto = listaAniversariante[index].Replace(";", "<br />");
                            lbListaMeio.Text += conteudoTexto;
                        }
                    }
                    else//EXBITE AS LISTA ESQUERDA E DIREITA ACIMA DE 10 NOMES
                    {
                        divListaEsquerda.Visible = true;
                        for (int index = 0; index < (rowCount / 2) + 1; index++)
                        {
                            string conteudoTexto = listaAniversariante[index].Replace(";", "<br />");
                            lbListaEsquerda.Text += conteudoTexto;
                        }
                        //
                        divListaDireita.Visible = true;
                        for (int index = (rowCount / 2) + 1; index < rowCount; index++)
                        {
                            string conteudoTexto = listaAniversariante[index].Replace(";", "<br />");
                            lbListaDireita.Text += conteudoTexto;
                        }
                    }
                }
            }

            if (int.Parse(lbContagem.Text) >= Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString()))
            {
                Response.RedirectToRoute("desempenho_bot");
            }

            lbContagem.Text = (int.Parse(lbContagem.Text) + 1).ToString();

        }

        public List<string> Aniversariantes()
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            List<string> aniversariante = new List<string>();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string mes = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                string sql = @"SELECT NAME AS NOME, REGISTRATION AS MATRICULA, DEPARTMENT AS SETOR, 
                                    to_char(to_date(BDATE,'YYYY-MM-DD'), 'DD/MM/YYYY') AS DATAANIVERSARIO, 
                                    to_char(to_date(BDATE,'YYYY-MM-DD'), 'DD/MM') AS MESDIAANIVERSARIO 
                                    FROM SFCRUNTIME.NIVER 
                 WHERE to_char(to_date(BDATE,'YYYY-MM-DD'), 'MM')='" + mes + "' ORDER BY BDATE, NOME";
                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    for (int index = 0; index < Objconn.Tabela.Rows.Count; index++)
                    {
                        string nome = Objconn.Tabela.Rows[index]["NOME"].ToString();
                        //if (nome.Length > 15)
                        //    nome = nome.Remove(nome.Length-7);
                        string mesAniversario = Objconn.Tabela.Rows[index]["MESDIAANIVERSARIO"].ToString();
                        //
                        aniversariante.Add(nome + " - " + mesAniversario + ";");
                    }
                }

            }
            finally
            {
                Objconn.Desconectar();
            }
            //
            return aniversariante;
        }

    }
}