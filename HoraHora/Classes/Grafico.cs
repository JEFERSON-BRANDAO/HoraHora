using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Globalization;

namespace Classes
{
    public class Grafico
    {
        public IList<ChartRMA> ListaRMA(string De, string ate)
        {
            DateTimeFormatInfo ukDtfi = new CultureInfo("pt-BR", false).DateTimeFormat;
            DateTime SqlDataInicio = new DateTime();
            DateTime SqlDataFim = new DateTime();
            //
            DateTime.TryParse(De, ukDtfi, DateTimeStyles.None, out SqlDataInicio);
            DateTime.TryParse(ate, ukDtfi, DateTimeStyles.None, out SqlDataFim);
            //SqlDataFim = SqlDataFim.AddDays(1);

            List<ChartRMA> ListaGrafrico = new List<ChartRMA>();
            //
            string sql = @" ";


            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    int rowCount = Objconn.Tabela.Rows.Count;
                    if (rowCount > 0)
                    {
                        decimal total = 1;//Convert.ToDecimal(getTotal(Objconn.Tabela));
                        //
                        if (total > 0)
                        {
                            foreach (DataRow linha in Objconn.Tabela.Rows)
                            {
                                int cont = 0;
                                //
                                DateTime DataIni = new DateTime();
                                DateTime DataF = new DateTime();
                                //
                                DateTime.TryParse(De, ukDtfi, DateTimeStyles.None, out DataIni);
                                DateTime.TryParse(ate, ukDtfi, DateTimeStyles.None, out DataF);
                                //
                                DataIni = new DateTime(DataIni.Year, DataIni.Month, 01);
                                DataF = new DateTime(DataF.Year, DataF.Month, 01);
                                //
                                Decimal valor = 0;
                                cont++;
                                //
                                while (cont == 1)//para nao duplicar os dados qnd preencher o grafico
                                {
                                    ChartRMA cht = new ChartRMA();
                                    //
                                    cht.SubCategoria = "";
                                    cht.Categoria = linha["matricula"].ToString();//DataIni.Year.ToString();
                                    cht.Serie = linha["nome"].ToString();
                                    //
                                    string qtdFalhaPulseira = string.IsNullOrEmpty(linha["totalFalhaP"].ToString()) ? "0" : linha["totalFalhaP"].ToString();
                                    string qtdFalhaCalcanheira = string.IsNullOrEmpty(linha["totalFalhaC"].ToString()) ? "0" : linha["totalFalhaC"].ToString();
                                    //
                                    decimal total_falhas = (Convert.ToDecimal(qtdFalhaPulseira) + Convert.ToDecimal(qtdFalhaCalcanheira));
                                    //
                                    valor = ((Convert.ToDecimal(total_falhas) * 100) / total);
                                    cht.Valor = Math.Round(valor);
                                    //
                                    if (valor > 0)//adiciona no grafico somente funcionario que tiveram alguma falha durante teste
                                        ListaGrafrico.Add(cht);
                                    //DataIni = DataIni.AddMonths(1);
                                    //
                                    cont = 0;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    ListaGrafrico.Clear();
                }

            }
            finally
            {
                Objconn.Desconectar();
            }

            return ListaGrafrico;
        }

        public class ChartRMA
        {
            public string Serie { get; set; }
            public string Categoria { get; set; }
            public string SubCategoria { get; set; }
            public Decimal Valor { get; set; }

            public ChartRMA()
            {
                this.Valor = 0;
            }

        }
    }
}