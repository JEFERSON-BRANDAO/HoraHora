using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Configuration;
using System.Threading;
using System.Globalization;

namespace HoraHora
{
    public partial class YieldBot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string linha = ConfigurationManager.AppSettings["linha"].ToString().Trim();
            string data = DateTime.Now.Date.ToString("yyyy-MM-dd");
            lbData.Text = "DIA " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            string eventPoint = string.Empty;//lbLado.Text;
            //
            Producao producao = new Producao();

            if (!IsPostBack)
            {
                //DESCRIÇÃO DO PARTNUMBER
                List<string> modeloLista = new List<string>();
                modeloLista = producao.Modelo(data, linha, "DESCRICAO");
                //SKUNO DO PARTNUMBER
                List<string> partNumberLista = new List<string>();
                partNumberLista = producao.Modelo(data, linha, "PARTNUMBER");
                lbLine.Text = linha.Replace("SI", "LINE ");

                Session["rowCountLista"] = modeloLista.Count;
                Session["modeloLista"] = modeloLista;
                Session["partNumberLista"] = partNumberLista;
            }
            //
            int contagem = (int.Parse(lbContagem.Text));//contagem tempo
            string modelo = ((List<string>)Session["partNumberLista"])[0];//partNumberLista[0].ToString();
            lbModelo.Text = ((List<string>)Session["modeloLista"])[0];//modeloLista[0].ToString();
            int rowCountListEvento = producao.Estacao(modelo).Count;
            //
            if (rowCountListEvento > 0)
            {
                if (contagem < rowCountListEvento)
                {
                    eventPoint = producao.Estacao(modelo)[contagem];
                    lbLado.Text = eventPoint;
                    CarregaGrid(data, eventPoint, modelo, linha);
                    //
                    Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString() + "000"));

                }
                //
                if (int.Parse(lbContagem.Text) == Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString()))
                {
                    Response.RedirectToRoute("be");
                }
            }
            else
            {
                if (int.Parse(lbContagem.Text) == Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString()))
                {
                    Response.RedirectToRoute("be");
                }
            }

            //
            lbContagem.Text = (int.Parse(lbContagem.Text) + 1).ToString();
        }

        public void CarregaGrid(string data, string eventPoint, string modelo, string linha)
        {
            decimal realizado = 0;
            decimal falhas = 0;
            decimal yield = 0;
            decimal acumPlan = 0;
            int tipoObs = 1;//yield rate           
            //
            Producao producao = new Producao();
            List<Classes.Producao.Dados> listDados = new List<Producao.Dados>();
            listDados = producao.Hora_hora(data, eventPoint, modelo, linha, tipoObs);
            //
            int rowCount = listDados.Count;//producao.Hora_hora(data, eventPoint, modelo, linha).Count;
            if (rowCount > 0)
            {
                gridHoraHora.DataSource = listDados;//producao.Hora_hora(data, eventPoint, modelo, linha);
                gridHoraHora.DataBind();
                //
                for (int index = 0; index < rowCount; index++)
                {
                    GridViewRow gvRow = gridHoraHora.Rows[index];
                    //
                    string planejado = string.IsNullOrEmpty(gvRow.Cells[1].Text) ? "0" : gvRow.Cells[1].Text.Trim().Replace("-", "0");
                    if (int.Parse(planejado) > 0)
                    {
                        string falha = string.IsNullOrEmpty(gvRow.Cells[3].Text) ? "0" : gvRow.Cells[3].Text.Trim().Replace("-", "0");
                        if (int.Parse(falha) > 0)
                        {
                            if (gvRow.Cells[3].Text != "-")//houve produção no horário
                            {
                                //se houver falha
                                gvRow.Cells[4].BackColor = System.Drawing.Color.Red;//YIELD                           
                                gvRow.Cells[4].ForeColor = System.Drawing.Color.White;

                                // this.diYield.Style.Add("background-color", "#FF0000");
                            }
                        }
                        else
                        {
                            if (gvRow.Cells[3].Text != "-")//houve produção no horário
                            {
                                //sem falha
                                gvRow.Cells[4].BackColor = System.Drawing.Color.LightGreen;//YIELD 
                            }
                        }
                    }
                    else
                    {
                        gvRow.Cells[4].Text = "-";//Quando não houver valor planejado
                    }


                    //string falha = string.IsNullOrEmpty(gvRow.Cells[3].Text) ? "0" : gvRow.Cells[3].Text.Trim().Replace("-", "0");
                    //if (int.Parse(falha) > 0)
                    //{
                    //    if (gvRow.Cells[3].Text != "-")//houve produção no horário
                    //    {
                    //        //se houver falha
                    //        gvRow.Cells[4].BackColor = System.Drawing.Color.Red;//YIELD                           
                    //        gvRow.Cells[4].ForeColor = System.Drawing.Color.White;

                    //        // this.diYield.Style.Add("background-color", "#FF0000");
                    //    }
                    //}
                    //else
                    //{
                    //    string pajenado = string.IsNullOrEmpty(gvRow.Cells[1].Text) ? "0" : gvRow.Cells[1].Text.Trim().Replace("-", "0");
                    //    if (int.Parse(pajenado) > 0)
                    //    {
                    //        if (gvRow.Cells[3].Text != "-")//houve produção no horário
                    //        {
                    //            //sem falha
                    //            gvRow.Cells[4].BackColor = System.Drawing.Color.LightGreen;//YIELD                           

                    //            //this.diYield.Style.Add("background-color", "#7CFC00");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        gvRow.Cells[4].Text = "-";//Quando não houver valor planejado
                    //    }
                    //}
                    //
                    if (gvRow.Cells[1].Text != "-")
                        acumPlan += decimal.Parse(gvRow.Cells[1].Text.Replace("%", string.Empty).Trim().Replace("-", "0"));

                    realizado += decimal.Parse(gvRow.Cells[2].Text.Replace("%", string.Empty).Trim().Replace("-", "0"));
                    falhas += decimal.Parse(gvRow.Cells[3].Text.Replace("%", string.Empty).Trim().Replace("-", "0"));

                }
                //
                //if (acumPlan > 0)
                if (realizado > 0)
                {
                    lbPlanejado.Text = acumPlan.ToString();
                    lbRealizado.Text = realizado.ToString();
                    lbFalha.Text = falhas.ToString();
                    //yield = ((plan - falhas) * 100) / plan;
                    yield = ((realizado - falhas) * 100) / realizado;//acumPlan;
                    lbYield.Text = Math.Round(yield, 2).ToString() + "%";
                    //lbPorcentagem.Text = lbYield.Text;
                    //
                    //decimal valor = Math.Round(Convert.ToDecimal(yield), 2);
                    double valor = Math.Round(Convert.ToDouble(yield), 2);
                    if (valor >= 99.6)
                    {
                        diYield.Style.Add("background-color", "#00FF00");
                        lbYield.ForeColor = System.Drawing.Color.Black;

                        //divCirculo.Style.Add("background-color", "#00FF00");
                    }
                    else
                    {
                        diYield.Style.Add("background-color", "#FF0000");
                        lbYield.ForeColor = System.Drawing.Color.White;

                        //divCirculo.Style.Add("background-color", "#FF0000");
                    }
                }
                else
                {
                    diYield.Style.Add("background-color", "#4b6c9e");
                    lbYield.ForeColor = System.Drawing.Color.White;
                    //
                    lbPlanejado.Text = "0";
                    lbRealizado.Text = "0";
                    lbFalha.Text = "0";
                    lbYield.Text = "0";
                    //lbPorcentagem.Text = "0%";
                    //
                    //divCirculo.Style.Add("background-color", "#00FF00");
                }
            }
            else
            {
                diYield.Style.Add("background-color", "#4b6c9e");
                lbYield.ForeColor = System.Drawing.Color.White;
                //
                gridHoraHora.DataSource = null;
                gridHoraHora.DataBind();
                //
                lbPlanejado.Text = "0";
                lbRealizado.Text = "0";
                lbFalha.Text = "0";
                lbYield.Text = "0";
                //lbPorcentagem.Text = "0%";
                //
                //divCirculo.Style.Add("background-color", "#00FF00");
            }

        }

    }
}