using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace HoraHora
{
    /// <summary>
    /// Summary description for exibi_imagem
    /// </summary>
    public class exibi_imagem : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string arquivo = context.Request.QueryString["arquivo"];//caminho e imagem
            //          
            string caminho = System.IO.Path.Combine(arquivo);
            System.Drawing.Image imagem = System.Drawing.Bitmap.FromFile(caminho);           
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            imagem.Save(ms, System.Drawing.Imaging.ImageFormat.Png);           
            context.Response.BinaryWrite(ms.ToArray());

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}