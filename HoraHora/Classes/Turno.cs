using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class Turno
    {
        public List<Horario> Informacao()
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            List<Horario> lista = new List<Horario>();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //              
//                string sqlShift = " ";
//                string shift = turno;
//                shift = shift.Replace(" ", string.Empty);
//                //
//                if (shift == "SHIFT1")
//                {
//                    sqlShift = @" where hourperiod IN (SELECT  hourperiod                                  
//                                                 from  mfshifttime                                   
//                                                   where seqno >=10 
//                                                   and seqno<=90 or seqno=260) ";
//                }
//                //
//                if (shift == "SHIFT2")
//                {
//                    sqlShift = @" where hourperiod IN (SELECT  hourperiod                                  
//                                                 from  mfshifttime                                   
//                                                  where seqno >=100 and seqno<=170 ) ";
//                }
//                //
//                if (shift == "SHIFT3")
//                {
//                    sqlShift = @" where hourperiod IN (SELECT  hourperiod                                  
//                                                 from  mfshifttime                                   
//                                                 where seqno >=200 and seqno<=250) ";
//                }
                //
//                string sql = @"SELECT shift, hourperiod,fromtime,totime,
//                                    case  seqno 
//                                    when 180 then 10 
//                                    when 190 then 20 
//                                    when 200 then 30 
//                                    when 210 then 40 
//                                    when 220 then 50 
//                                    when 230 then 60 
//                                    when 240 then 70
//                                    when 250 then 80 
//                                    when 260 then 90 
//                                    when 10 then 100 
//                                    when 20 then 110 
//                                    when 30 then 120 
//                                    when 40 then 130 
//                                    when 50 then 140 
//                                    when 60 then 150 
//                                    when 70 then 160 
//                                    when 80 then 170 
//                                    when 90 then 180 
//                                    when 100 then 190 
//                                    when 110 then 200 
//                                    when 120 then 210 
//                                    when 130 then 220 
//                                    when 140 then 230 
//                                    when 150 then 240 
//                                    when 160 then 250 
//                                    when 170 then 260 
//                                    end seqno2  FROM  mfshifttime ORDER BY seqno2";// + sqlShift + "  ORDER BY seqno2";
                string sql = @" SELECT shift, hourperiod,fromtime,totime,
                                    case  seqno 
                                    when 180 then 10                              
                                    when 260 then 90 
                                    when 10 then 100 
                                    when 20 then 110 
                                    when 30 then 120 
                                    when 40 then 130 
                                    when 50 then 140 
                                    when 60 then 150 
                                    when 70 then 160 
                                    when 80 then 170 
                                    when 90 then 180 
                                    when 100 then 190 
                                    when 110 then 200 
                                    when 120 then 210 
                                    when 130 then 220 
                                    when 140 then 230 
                                    when 150 then 240 
                                    when 160 then 250 
                                    when 170 then 260 
                                    end seqno2 
                                    FROM  mfshifttime 
                                    WHERE (seqno >=10 and seqno <=180)
                                    or seqno=260
                                    or seqno=200
                                    ORDER BY seqno2";
                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    for (int index = 0; index < Objconn.Tabela.Rows.Count; index++)
                    {
                        Horario item = new Horario();
                        //
                        item.SHIFT = Objconn.Tabela.Rows[index]["SHIFT"].ToString();
                        item.FROMTIME = Objconn.Tabela.Rows[index]["FROMTIME"].ToString();
                        item.HOURPERIOD = Objconn.Tabela.Rows[index]["HOURPERIOD"].ToString();
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

        public class Horario
        {
            public string SHIFT { get; set; }
            public string FROMTIME { get; set; }
            public string HOURPERIOD { get; set; }
        }
    }
}