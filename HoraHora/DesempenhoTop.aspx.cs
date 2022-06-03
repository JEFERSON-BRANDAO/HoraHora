using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Globalization;
using System.Configuration;
using System.Threading;

namespace HoraHora
{
    public partial class DesempenhoTop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CultureInfo culture = new CultureInfo("pt-BR");
                //DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                //int dia = DateTime.Now.Day;
                //string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                //lbData.Text = dia + " " + mes.Remove(3).ToUpper();               
            }
            //
            string linha = ConfigurationManager.AppSettings["linha"].ToString().Trim();
            //lbLine.Text = linha.Replace("SMT L", "LINE");
            string data = DateTime.Now.Date.ToString("yyyy-MM-dd");
            lbData.Text = "DIA " + DateTime.Now.Date.ToString("dd/MM/yyyy"); 
            string eventPoint = lbLado.Text;
            //
            Producao producao = new Producao();
            //DESCRIÇÃO DO PARTNUMBER
            List<string> modeloLista = new List<string>();
            modeloLista = producao.Modelo(data, linha, "DESCRICAO");
            //SKUNO DO PARTNUMBER
            List<string> partNumberLista = new List<string>();
            partNumberLista = producao.Modelo(data, linha, "PARTNUMBER");
            //
            lbLine.Text = linha.Replace("SMT L", "LINE");
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
                        string modelo = partNumberLista[row].ToString();
                        lbModelo.Text = modeloLista[row].ToString() + " TOP";
                        CarregaGrid(data, eventPoint, modelo, linha);
                        //
                        Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString() + "000"));
                    }

                }
                //
                if (int.Parse(lbContagem.Text) == Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString()))
                {
                    Response.RedirectToRoute("yield_top");
                }
            }
            else
            {
                if (int.Parse(lbContagem.Text) == Convert.ToInt32(ConfigurationManager.AppSettings["tempo"].ToString()))
                {
                    Response.RedirectToRoute("yield_top");
                }
            }
            //
            lbContagem.Text = (int.Parse(lbContagem.Text) + 1).ToString();
        }

        public void CarregaGrid(string data, string eventPoint, string modelo, string linha)
        {
            decimal realizado = 0;
            decimal falhas = 0;
            decimal performance = 0;
            decimal acumPlan = 0;
            int tipoObs = 0;//eficiência
            int TOP = 1;
            //
            Producao producao = new Producao();
            List<Classes.Producao.Dados> listDados = new List<Producao.Dados>();
            listDados = producao.Hora_hora(data, eventPoint, modelo, linha, tipoObs, TOP);
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
                    if (gvRow.Cells[3].Text != "-")//houve produção no horário
                    {
                        string porcent_Peformance = gvRow.Cells[3].Text.Replace("%", string.Empty).Trim().Replace("-", "0");
                        double valor = Convert.ToDouble(porcent_Peformance, System.Globalization.CultureInfo.InvariantCulture);
                        if (valor >= 100)
                        {
                            gvRow.Cells[3].BackColor = System.Drawing.Color.LightGreen;//PERFORMANCE
                        }
                        else
                        {
                            if (valor > 0)//quando valor planeja não for zero
                            {
                                gvRow.Cells[3].BackColor = System.Drawing.Color.Red;//PERFORMANCE
                                gvRow.Cells[3].ForeColor = System.Drawing.Color.White;
                            }
                            else 
                            {
                                gvRow.Cells[3].Text = "-";//Quando não houver valor planejado
                            }
                        }

                    }
                    //
                    if (gvRow.Cells[1].Text != "-")
                        acumPlan += decimal.Parse(gvRow.Cells[1].Text.Replace("%", string.Empty).Trim().Replace("-", "0"));
                    realizado += decimal.Parse(gvRow.Cells[2].Text.Replace("%", string.Empty).Trim().Replace("-", "0"));

                }
                //
                if (acumPlan > 0)
                {
                    lbPlanejado.Text = acumPlan.ToString();
                    lbRealizado.Text = realizado.ToString();
                    //lbFalha.Text = falhas.ToString();
                    //yield = ((plan - falhas) * 100) / plan;
                    performance = ((realizado - falhas) * 100) / acumPlan;
                    lbPerformance.Text = Math.Round(performance, 2).ToString() + "%";
                    //lbPorcentagem.Text = lbPerformance.Text;
                    ////
                    decimal valor = Math.Round(Convert.ToDecimal(performance), 2);
                    if (valor >= 100)
                    {
                        diPerformace.Style.Add("background-color", "#00FF00");
                        lbPerformance.ForeColor = System.Drawing.Color.Black;

                        //divCirculo.Style.Add("background-color", "#00FF00");
                    }
                    else
                    {
                        diPerformace.Style.Add("background-color", "#FF0000");
                        lbPerformance.ForeColor = System.Drawing.Color.White;

                        //divCirculo.Style.Add("background-color", "#FF0000");
                    }
                }
                else
                {
                    diPerformace.Style.Add("background-color", "#4b6c9e");
                    lbPerformance.ForeColor = System.Drawing.Color.White;
                    //
                    lbPlanejado.Text = "0";
                    lbRealizado.Text = "0";
                    //lbFalha.Text = "0";
                    lbPerformance.Text = "0";
                    //lbPorcentagem.Text = "0%";
                    //
                    // divCirculo.Style.Add("background-color", "#00FF00");
                }
            }
            else
            {
                diPerformace.Style.Add("background-color", "#4b6c9e");
                lbPerformance.ForeColor = System.Drawing.Color.White;
                //
                gridHoraHora.DataSource = null;
                gridHoraHora.DataBind();               
                //
                lbPlanejado.Text = "0";
                lbRealizado.Text = "0";
                //lbFalha.Text = "0";
                lbPerformance.Text = "0";
                //lbPorcentagem.Text = "0%";
                //
                //divCirculo.Style.Add("background-color", "#00FF00");
            }

        }
    }
}