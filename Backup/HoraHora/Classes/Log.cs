using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes
{
    class Log
    {
        public void Gravar(string NumeroControle, string Mensagem, string TipoAviso)
        {
            #region CRIA AQUIVO DE LOG .txt

            string hora = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            string minuto = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            string segundo = DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString();
            //
            string dataHora = DateTime.Now.Date.ToString("dd-MM-yyyy") + " " + hora + ":" + minuto + ":" + segundo;
            string nomeArquivo = AppDomain.CurrentDomain.BaseDirectory + @"\LOG\HISTORICO.txt";
            //
            if (!System.IO.File.Exists(nomeArquivo))//quando arquivo não exite. Criado pela 1ra vez
            {
                System.IO.File.Create(nomeArquivo).Close();
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(nomeArquivo))
                {
                    string historico = "[" + dataHora + "] - AVISO: " + TipoAviso + " - SOLICITAÇÃO: " + NumeroControle + " - MENSAGEM: " + Mensagem;
                    //
                    sw.WriteLine(historico);
                    sw.Close();
                }
            }
            else
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(nomeArquivo))
                {
                    string historico = "[" + dataHora + "] - AVISO: " + TipoAviso + " - SOLICITAÇÃO: " + NumeroControle + " - MENSAGEM: " + Mensagem;
                    //
                    sw.WriteLine(historico);
                    sw.Close();
                }
            }

            #endregion
        }
    }
}
