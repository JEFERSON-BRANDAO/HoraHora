using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class Desempenho
    {
        public bool ExisteRegistro(string modelo, string linha, string data)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string sql = @" SELECT COUNT(*) ROW_COUNT FROM R_AP_TEMP 
                                       WHERE DATA1 = 'ARRIS_UPH' 
                                       AND DATA2 = '" + modelo + "' " +
                                       "AND DATA6 = '" + linha + "' " +
                                       "AND TO_CHAR(WORK_TIME,'YYYY-MM-DD')='" + data + "'";
                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();

            }
            finally
            {
                Objconn.Desconectar();
            }

            //
            if (Objconn.Tabela.Rows.Count > 0)
            {
                return string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["ROW_COUNT"].ToString()) ? false : true;
            }
            else
            {
                return false;
            }

        }

        public void Registrar(string modelo, string linha, string data, List<string>periodo)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string sql = @"INSERT INTO R_AP_TEMP ";
                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();

            }
            finally
            {
                Objconn.Desconectar();
            }

            //
            if (Objconn.Tabela.Rows.Count > 0)
            {
              
            }
            else
            {
               
            }

        }



        public class Temp
        {
            public string DATA1 { get; set; }//ARRIS_UPH
            public string DATA2 { get; set; }//PARTBUMBER            
            public string DATA4 { get; set; }//PERIODO
            public string DATA5 { get; set; }//OBSERVACAO
            public string DATA6 { get; set; }//LINHA
            public string WORK_TIME { get; set; }//DATA

        }
    }
}