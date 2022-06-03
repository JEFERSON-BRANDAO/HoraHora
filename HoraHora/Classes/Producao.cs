using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

namespace Classes
{
    public class Producao
    {
        public List<string> Estacao(string modelo)
        {
            List<string> estacoes = new List<string>();
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                // 
                string sql = @"SELECT a.EVENTPOINT FROM  SFCROUTEDEFB a
                                INNER JOIN SFCCODELIKE b ON a.ROUTEID = b.AUTOROUTE
                                WHERE b.SKUNO = '" + modelo + "' " +
                                "AND (A.EVENTSEQNO =13  OR A.EVENTSEQNO =17 OR A.EVENTSEQNO =21) "+
                //OR A.EVENTSEQNO =28) " +
                                "ORDER BY a.EVENTSEQNO ";
                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    for (int index = 0; index < Objconn.Tabela.Rows.Count; index++)
                    {
                        string eventpoint = Objconn.Tabela.Rows[index][0].ToString();
                        if (!string.IsNullOrEmpty(eventpoint))
                        {
                            estacoes.Add(eventpoint);
                        }
                    }
                }

            }
            finally
            {
                Objconn.Desconectar();
            }
            //
            return estacoes;
        }

        public List<Dados> Informacao(string data, string eventPoint, string modelo, string turno)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            List<Dados> lista = new List<Dados>();
            //
            try
            {

                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //             
                string sql = @"SELECT b.DATA2 modelo, a.uphdate, a.hourperiod, a.Eventpoint, a.productionline, b.DATA5 planejado, 
                               SUM(a.inputunit) total_qty, SUM(a.outputunit) pass_qty,SUM(a.inputunit)-SUM(a.outputunit) fail_qty,
                               ROUND((sum(a.outputunit)/sum(a.inputunit))*100,2)||'%' YIELD_RATE,
                               CASE b.DATA5 WHEN '0' THEN '0' 
                                  ELSE (ROUND(sum(a.outputunit*100)/(SUM(a.inputunit)) ,2)||'%')
                               END PERFORMANCE
                               FROM SFCUPHRATEDETAIL a 
                               INNER JOIN R_AP_TEMP b  ON a.HOURPERIOD = b.DATA4 
                               INNER JOIN SFCROUTEDEFB c ON a.EVENTPOINT =c.EVENTPOINT
                               INNER JOIN SFCCODELIKE  d ON c.ROUTEID= d.AUTOROUTE
                               WHERE a.uphdate= '" + data + "' " +
                               "AND d.SKUNO= '" + modelo + "' " +
                               "AND b.DATA1='ARRIS_PLAN' " +
                               "AND b.DATA2= d.SKUNO " +
                               " AND a.EVENTPOINT in (SELECT a.EVENTPOINT FROM  SFCROUTEDEFB a  " +
                                               "INNER JOIN SFCCODELIKE b ON a.ROUTEID = b.AUTOROUTE  " +
                                               "WHERE b.SKUNO ='" + modelo + "' " +
                                               "AND (A.EVENTSEQNO =13  OR A.EVENTSEQNO =17 OR A.EVENTSEQNO =21 OR A.EVENTSEQNO =28) )  " +
                               "AND b.data4 IN (SELECT DISTINCT HOURPERIOD FROM  SFCUPHRATEDETAIL " +
                                                     "WHERE WORKORDERNO IN (SELECT WORKORDERNO FROM MFWORKORDER WHERE SKUNO='RU9026000643') " +
                                                                                 "AND UPHDATE= '" + data + "' ) " +
                               @"GROUP BY a.Eventpoint, a.uphdate, a.hourperiod, a.productionline, b.data2, b.DATA5, c.EVENTSEQNO  order by a.hourperiod, c.EVENTSEQNO";
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    for (int index = 0; index < Objconn.Tabela.Rows.Count; index++)
                    {
                        Dados item = new Dados();
                        item.HOURPERIOD = Objconn.Tabela.Rows[index]["HOURPERIOD"].ToString();
                        item.EVENTPOINT = Objconn.Tabela.Rows[index]["EVENTPOINT"].ToString();
                        item.PLANEJADO = Objconn.Tabela.Rows[index]["PLANEJADO"].ToString();
                        item.REALIZADO = Objconn.Tabela.Rows[index]["PASS_QTY"].ToString();
                        item.DEFEITO = Objconn.Tabela.Rows[index]["FAIL_QTY"].ToString();
                        item.TOTAL = Objconn.Tabela.Rows[index]["TOTAL_QTY"].ToString();
                        item.YIELD = Objconn.Tabela.Rows[index]["YIELD_RATE"].ToString().StartsWith(".") ? "0" + Objconn.Tabela.Rows[index]["YIELD_RATE"].ToString() : Objconn.Tabela.Rows[index]["YIELD_RATE"].ToString();
                        item.PERFORMANCE = Objconn.Tabela.Rows[index]["PERFORMANCE"].ToString().Replace("0100%", "100%");
                        //
                        lista.Add(item);
                    }
                }
            }
            finally
            {
                Objconn.Desconectar();
            }
            //
            return lista;
        }

        public List<string> Modelo(string data, string linha, string tipo)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            List<string> modelo = new List<string>();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                // 
                string sql = @" SELECT DISTINCT TRIM(REPLACE(REPLACE(REPLACE(TRIM(a.CODENAME), 'BE',''),'-',''),'MODEL','' )) CODENAME, a.SKUNO
                                     FROM SFCCODELIKE a 
                                     INNER JOIN MFWORKORDER b ON a.SKUNO= b.SKUNO
                                     inner join SFCUPHRATEDETAIL c on b.WORKORDERNO= c.WORKORDERNO
                                     WHERE a.CATEGORY='MODEL'
                                     AND a.SKUNO='RU9026000643' ";
                // AND c.productionline ='" + linha + "' " +
                // "AND c.uphdate='" + data + "'";
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    for (int index = 0; index < Objconn.Tabela.Rows.Count; index++)
                    {
                        if (tipo == "PARTNUMBER")
                        {
                            //partnumber
                            modelo.Add(Objconn.Tabela.Rows[index]["SKUNO"].ToString());
                        }
                        else
                        {
                            //descrição
                            modelo.Add(Objconn.Tabela.Rows[index]["CODENAME"].ToString());

                        }
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

        public List<Observacoes> Observacao(string modelo, string eventPoint, string data, string linha, int tipoObs)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            List<Observacoes> lista = new List<Observacoes>();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                //                string sqlTurno = " ";
                //                string shift = turno;
                //                shift = shift.Replace(" ", string.Empty);
                //                //
                //                if (shift.Trim() == "SHIFT1")
                //                {
                //                    sqlTurno = @"(SELECT  hourperiod                                  
                //                                                 from  mfshifttime                                   
                //                                                   where seqno >=10 
                //                                                   and seqno<=90 or seqno=260) order by hour";
                //                }
                //                //
                //                if (shift.Trim() == "SHIFT2")
                //                {
                //                    sqlTurno = @"(SELECT  hourperiod                                  
                //                                                 from  mfshifttime                                   
                //                                                  where seqno >=100 and seqno<=170 ) order by hour";
                //                }
                //                //
                //                if (shift.Trim() == "SHIFT3")
                //                {
                //                    sqlTurno = @"(SELECT  hourperiod                                  
                //                                                 from  mfshifttime                                   
                //                                                  where seqno >=200 and seqno<=250) order by hour";
                //                }
                //
                string sql = @"SELECT HOUR AS HOURPERIOD, DESCRIPTION AS OBSERVACAO FROM SFCRUNTIME.SFC_TV_FAIL                                           
                                              WHERE MODEL  ='" + modelo + "' " +
                                             "AND SIDE     ='" + eventPoint + "' " +
                                             "AND DATE1    ='" + data + "' " +
                                             "AND LINE     ='" + linha + "' " +
                                             "AND TIPO_OBS = " + tipoObs + " ORDER BY hour";
                //"AND HOUR IN " + sqlTurno;
                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    for (int index = 0; index < Objconn.Tabela.Rows.Count; index++)
                    {
                        Observacoes item = new Observacoes();
                        //
                        item.HOURPERIOD = Objconn.Tabela.Rows[index]["HOURPERIOD"].ToString();
                        item.OBSERVACAO = Objconn.Tabela.Rows[index]["OBSERVACAO"].ToString();
                        lista.Add(item);
                    }
                }

            }
            finally
            {
                Objconn.Desconectar();
            }
            //
            return lista;
        }

        public List<Planejado> Valor_Planejado(string modelo, string linha, string lado)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            List<Planejado> lista = new List<Planejado>();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear(); 
                //
                string sqlValor = " ";

                if (linha.Trim().ToUpper().Contains("L1"))
                {
                    sqlValor = " a.DATA5 VALOR ";
                }
                else if (linha.Trim().ToUpper().Contains("L2"))
                {
                    sqlValor = " a.DATA6 VALOR ";
                }
                else if (linha.Trim().ToUpper().Contains("L3"))
                {
                    sqlValor = " a.DATA8 VALOR ";
                }
                else if (linha.Trim().ToUpper().Contains("L4"))
                {
                    sqlValor = " a.DATA9 VALOR ";
                }
                else if (linha.Trim().ToUpper().Contains("L5"))
                {
                    sqlValor = " a.DATA10 VALOR ";
                }
                else
                {
                    sqlValor = " a.DATA5 VALOR ";
                }

                string sql = @"SELECT case  a.data4 
                                    when '00:00-01:00' then 19                              
                                    when '01:00-02:00' then 20 
                                    when '02:00-03:00' then 21 
                                    when '03:00-04:00' then 22 
                                    when '04:00-05:00' then 23 
                                    when '05:00-06:00' then 24 
                                    when '06:00-07:00' then 1 
                                    when '07:00-08:00' then 2 
                                    when '08:00-09:00' then 3 
                                    when '09:00-10:00' then 4 
                                    when '10:00-11:00' then 5 
                                    when '11:00-12:00' then 6 
                                    when '12:00-13:00' then 7 
                                    when '13:00-14:00' then 8 
                                    when '14:00-15:00' then 9 
                                    when '15:00-16:00' then 10 
                                    when '16:00-17:00' then 11 
                                    when '17:00-18:00' then 12 
                                    when '18:00-19:00' then 13                                     
                                    when '19:00-20:00' then 14 
                                    when '20:00-21:00' then 15 
                                    when '21:00-22:00' then 16 
                                    when '22:00-23:00' then 17                                     
                                    when '23:00-24:00' then 18 
                                 end seqno,
                                 a.DATA2 AS MODELO, a.DATA4 AS HORARIO, " + sqlValor +
                                " FROM R_AP_TEMP a " +
                                "INNER JOIN SFCROUTEDEFB b  ON a.data3 = b.EVENTSEQNO " +
                                "INNER JOIN SFCCODELIKE  c  ON b.ROUTEID = c.AUTOROUTE " +
                                " WHERE a.DATA1='ARRIS_PLAN' AND a.DATA2='" + modelo + "'  and b.EVENTPOINT ='" + lado + "'" +
                    //"AND DATA3=" + lado +  //sqlTurno +
                                  " ORDER BY seqno";

                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    for (int index = 0; index < Objconn.Tabela.Rows.Count; index++)
                    {
                        Planejado item = new Planejado();
                        //
                        item.MODELO = Objconn.Tabela.Rows[index]["MODELO"].ToString();
                        item.HORARIO = Objconn.Tabela.Rows[index]["HORARIO"].ToString();
                        item.VALOR = Objconn.Tabela.Rows[index]["VALOR"].ToString();
                        //
                        lista.Add(item);

                    }
                }

            }
            finally
            {
                Objconn.Desconectar();
            }
            //
            return lista;
        }

        public List<Dados> Hora_hora(string data, string eventPoint, string modelo, string linha, int tipoObs)
        {

            OLEDBConnect Objconn = new OLEDBConnect();
            List<Dados> lista = new List<Dados>();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //              
                string sql = @"SELECT distinct   c.DATA2 modelo, a.uphdate, a.hourperiod, a.Eventpoint, a.productionline,            
                                       SUM(a.inputunit) total_qty, 
                                       SUM(a.outputunit) pass_qty,
                                       SUM(a.inputunit)-SUM(a.outputunit) fail_qty, 
                                       ROUND((sum(a.outputunit)/sum(a.inputunit))*100,2)||'%' YIELD_RATE, 
                                       CASE  c.DATA5 WHEN '0' THEN '0' 
                                       ELSE (ROUND(sum(a.outputunit*100)/( c.DATA5  ) ,2)||'%')
                                       END PERFORMANCE,
                                       case  c.data4 
                                            when '00:00-01:00' then 19                              
                                            when '01:00-02:00' then 20 
                                            when '02:00-03:00' then 21 
                                            when '03:00-04:00' then 22 
                                            when '04:00-05:00' then 23 
                                            when '05:00-06:00' then 24 
                                            when '06:00-07:00' then 1 
                                            when '07:00-08:00' then 2 
                                            when '08:00-09:00' then 3 
                                            when '09:00-10:00' then 4 
                                            when '10:00-11:00' then 5 
                                            when '11:00-12:00' then 6 
                                            when '12:00-13:00' then 7 
                                            when '13:00-14:00' then 8 
                                            when '14:00-15:00' then 9 
                                            when '15:00-16:00' then 10 
                                            when '16:00-17:00' then 11 
                                            when '17:00-18:00' then 12 
                                            when '18:00-19:00' then 13                                     
                                            when '19:00-20:00' then 14 
                                            when '20:00-21:00' then 15 
                                            when '21:00-22:00' then 16 
                                            when '22:00-23:00' then 17                                     
                                            when '23:00-24:00' then 18 
                                      end seqno                                       
                                      FROM SFCUPHRATEDETAIL a                               
                                      inner join SFCROUTEDEFB b on a.EVENTPOINT = b.EVENTPOINT
                                      inner join R_AP_TEMP    c on b.EVENTSEQNO = c.DATA3
                                      inner join SFCCODELIKE  d on b.ROUTEID    = d.AUTOROUTE 
                                      WHERE a.uphdate= '" + data + "' " +
                                      "AND c.data2= '" + modelo + "' " +
                                      "AND b.Eventpoint = '" + eventPoint + "' " +
                                      "AND b.EVENTSEQNO = c.DATA3 and a.HOURPERIOD " +
                                       @"in (SELECT a.data4
                                                     FROM R_AP_TEMP a 
                                                     INNER JOIN SFCROUTEDEFB b  ON a.data3 = b.EVENTSEQNO 
                                                     INNER JOIN SFCCODELIKE  c  ON b.ROUTEID = c.AUTOROUTE 
                                                     WHERE a.DATA1='ARRIS_PLAN'
                                                     AND a.DATA2='" + modelo + "'" +
                                                     "and b.EVENTPOINT ='" + eventPoint + "') " +
                    /*and a.HOURPERIOD in (select DISTINCT a.data4  
                                             FROM R_AP_TEMP a
                                             inner join SFCUPHRATEDETAIL b on a.DATA4 = b.HOURPERIOD                                    
                                             where a.DATA1= 'ARRIS_PLAN' 
                                             and a.data2= '" + modelo + "' " +
                                             "and b.UPHDATE='" + data + "' " +
                                             "and b.EVENTPOINT='" + eventPoint + "')" +*/
                                      @"AND c.DATA1 ='ARRIS_PLAN' 
                                      AND c.DATA2 = d.SKUNO                                  
                                      AND (TO_CHAR(a.LASTEDITDT,'HH24:MI')) not in ('00:59', '00:58', '00:57', '00:56', '00:55', '00:54', '00:53', '00:52', '00:51', '00:50', '00:49', '00:48', '00:47', '00:46')                                    
                                      GROUP BY a.Eventpoint,  a.uphdate, a.hourperiod, a.productionline, 
                                      c.DATA2, c.data4, c.DATA3,  c.DATA5, b.EVENTSEQNO ";
                // order by seqno, b.EVENTSEQNO";

                Objconn.SetarSQL(sql);
                Objconn.Executar();

                #region REMOVE HORARIO DUPLICADO 
                //
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                //
                foreach (DataRow drow in Objconn.Tabela.Rows)
                {
                    if (hTable.Contains(drow["hourperiod"]))
                        duplicateList.Add(drow);
                    else
                        hTable.Add(drow["hourperiod"], string.Empty);
                }
                //
                foreach (DataRow dRow in duplicateList)
                    Objconn.Tabela.Rows.Remove(dRow);

                #endregion

                int rowTotal_Produzido = Objconn.Tabela.Rows.Count;
                //
                if (rowTotal_Produzido > 0)
                {                  
                    Turno objTurno = new Turno();
                    int rowCount_turno = objTurno.Informacao().Count;
                    List<Turno.Horario> listaHorario = new List<Turno.Horario>();
                    listaHorario = objTurno.Informacao();
                    //
                    List<Observacoes> lista_Obs = new List<Observacoes>();
                    lista_Obs = Observacao(modelo, eventPoint, data, linha, tipoObs);
                    //
                    Producao producao = new Producao();
                    List<Producao.Planejado> listaPlanejado = new List<Producao.Planejado>();
                    listaPlanejado = producao.Valor_Planejado(modelo, linha, eventPoint);
                    string planejado = "0";
                    //
                    int row = 0;
                    int row_obs = 0;
                    int row_Valor = 0;
                    //
                    if (listaPlanejado.Count > 0)
                    {
                        for (int index = 0; index < rowCount_turno; index++)
                        {
                            Dados item = new Dados();
                            string horarioProduzido = "FIM";
                            //
                            if (row < rowTotal_Produzido)
                                horarioProduzido = Objconn.Tabela.Rows[row]["HOURPERIOD"].ToString();

                            //adiciona somente horário que houve produção
                            if (listaHorario[index].HOURPERIOD.Contains(horarioProduzido))
                            {
                                if (row <= listaPlanejado.Count)
                                {
                                    if (listaPlanejado.Count > 0)
                                    {
                                        if (listaPlanejado.Count < listaHorario.Count)
                                        {
                                            planejado = listaPlanejado[row].VALOR;
                                        }
                                        else
                                        {
                                            planejado = listaPlanejado[index].VALOR;
                                        }
                                        // row_Valor++;
                                    }

                                }
                                //
                                item.HOURPERIOD = Objconn.Tabela.Rows[row]["HOURPERIOD"].ToString();
                                item.EVENTPOINT = Objconn.Tabela.Rows[row]["EVENTPOINT"].ToString();
                                item.PLANEJADO = planejado; //Objconn.Tabela.Rows[row]["total_qty"].ToString();//planejado;
                                item.REALIZADO = Objconn.Tabela.Rows[row]["PASS_QTY"].ToString();
                                item.DEFEITO = Objconn.Tabela.Rows[row]["FAIL_QTY"].ToString();
                                item.TOTAL = Objconn.Tabela.Rows[row]["TOTAL_QTY"].ToString();
                                item.YIELD = Objconn.Tabela.Rows[row]["YIELD_RATE"].ToString().StartsWith(".") ? "0" + Objconn.Tabela.Rows[row]["YIELD_RATE"].ToString() : Objconn.Tabela.Rows[row]["YIELD_RATE"].ToString();
                                item.TARGET = "99.6";
                                item.PERFORMANCE = Objconn.Tabela.Rows[row]["PERFORMANCE"].ToString().StartsWith(".") ? "0" + Objconn.Tabela.Rows[row]["PERFORMANCE"].ToString() : Objconn.Tabela.Rows[row]["PERFORMANCE"].ToString();
                                //
                                if (lista_Obs.Count > 0)
                                {
                                    //busca por observaçoes inseridas nos horários                                   
                                    for (int i = 0; i < (lista_Obs.Count); i++)
                                    {
                                        string periodo = lista_Obs[i].HOURPERIOD.ToString();
                                        string observacao = lista_Obs[i].OBSERVACAO.ToString();
                                        //
                                        if (Objconn.Tabela.Rows[row]["HOURPERIOD"].ToString() == periodo)
                                        {
                                            item.OBSERVACAO = observacao;
                                            //  
                                            break;
                                        }
                                        else
                                        {
                                            item.OBSERVACAO = "-";
                                        }
                                    }

                                }
                                else
                                {
                                    item.OBSERVACAO = "-";
                                }
                                //
                                lista.Add(item);
                                //
                                row++;
                                planejado = "-";
                            }
                            else///////adicionado para exibir em horarios que naõ houve produção
                            {
                                //if (row_Valor <= listaPlanejado.Count)
                                //{
                                //    planejado = listaPlanejado[row_Valor].VALOR;//adiciona valor planejado também ao horarios que ainda nao houveram producao
                                //}
                                //else
                                //{
                                //    planejado = "-";
                                //}


                                if (listaPlanejado.Count > 0)
                                {
                                    if (listaPlanejado.Count < listaHorario.Count)
                                    {
                                        planejado = "-";
                                    }
                                    else
                                    {
                                        planejado = listaPlanejado[row_Valor].VALOR;
                                    }

                                }
                                else
                                {
                                    planejado = "-";
                                }

                                //busca por observaçoes inseridas nos horários
                                if (row_obs < (lista_Obs.Count))
                                {
                                    if (row < rowTotal_Produzido)
                                    {
                                        row_obs = 0;
                                        //
                                        for (int linhaOBS = 0; linhaOBS < (lista_Obs.Count); linhaOBS++)
                                        {
                                            string periodo = lista_Obs[row_obs].HOURPERIOD.ToString();
                                            if (listaHorario[index].HOURPERIOD.Contains(periodo))
                                            {
                                                string observacao = lista_Obs[row_obs].OBSERVACAO.ToString();
                                                item.OBSERVACAO = observacao;
                                                //
                                                break;

                                            }
                                            else
                                            {
                                                string observacao = "-";
                                                item.OBSERVACAO = observacao;
                                            }
                                            //
                                            row_obs++;
                                        }
                                    }
                                    //
                                    item.OBSERVACAO = string.IsNullOrEmpty(item.OBSERVACAO) ? "-" : item.OBSERVACAO;
                                }
                                else
                                {
                                    item.OBSERVACAO = "-";
                                }

                                item.HOURPERIOD = listaHorario[index].HOURPERIOD;//objTurno.Informacao(turno)[index].HOURPERIOD.ToString();
                                item.EVENTPOINT = eventPoint;
                                item.PLANEJADO = planejado;//"-";
                                item.REALIZADO = "-";
                                item.DEFEITO = "-";
                                item.TOTAL = "-";
                                item.YIELD = "-";
                                item.TARGET = "-";
                                item.PERFORMANCE = "-";
                                //item.OBSERVACAO = "-";
                                //
                                lista.Add(item);

                            }

                            row_Valor++;

                        }

                        //var distinct = new HashSet<Dados>();
                        //var duplicates = new HashSet<Dados>();

                        //foreach (var s in lista)
                        //    if (!distinct.Add(s))
                        //        duplicates.Add(s);
                    }
                    else
                    {
                        Dados item = new Dados();
                        item.HOURPERIOD = "Sem cadastro";//objTurno.Informacao(turno)[index].HOURPERIOD.ToString();
                        item.EVENTPOINT = eventPoint;
                        item.PLANEJADO = "-";
                        item.REALIZADO = "-";
                        item.DEFEITO = "-";
                        item.TOTAL = "-";
                        item.YIELD = "-";
                        item.TARGET = "-";
                        item.PERFORMANCE = "-";
                        item.OBSERVACAO = "-";
                        //
                        lista.Add(item);
                    }
                }

            }
            finally
            {
                Objconn.Desconectar();
            }

            return lista;
        }

        public class Observacoes
        {
            public string HOURPERIOD { get; set; }
            public string OBSERVACAO { get; set; }
        }

        public class Dados
        {
            //public string HORA { get; set; }
            public string HOURPERIOD { get; set; }
            public string EVENTPOINT { get; set; }
            public string PLANEJADO { get; set; }
            public string REALIZADO { get; set; }
            public string DEFEITO { get; set; }
            public string TOTAL { get; set; }
            public string YIELD { get; set; }
            public string TARGET { get; set; }
            public string PERFORMANCE { get; set; }
            public string OBSERVACAO { get; set; }
        }

        public class Planejado
        {
            public string MODELO { get; set; }
            public string HORARIO { get; set; }
            public string VALOR { get; set; }
        }
    }
}