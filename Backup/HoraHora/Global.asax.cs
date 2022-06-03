using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace HoraHora
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("default", "Home", "~/Default.aspx");
            //SMT1
            //routes.MapPageRoute("bot", "Bot", "~/Bot.aspx");
            routes.MapPageRoute("desempenho_bot", "Desempenho_bot", "~/DesempenhoBot.aspx");
            routes.MapPageRoute("yield_bot", "Yield_bot", "~/YieldBot.aspx");
            //SMT2
            //routes.MapPageRoute("top", "Top", "~/Top.aspx");
           // routes.MapPageRoute("desempenho_top", "Desempenho_top", "~/DesempenhoTop.aspx");
            //routes.MapPageRoute("yield_top", "Yield_top", "~/YieldTop.aspx");
            //gráficos
            //routes.MapPageRoute("fe", "Fe", "~/graficos/Fe.aspx");
            routes.MapPageRoute("be", "Be", "~/graficos/Be.aspx");
            routes.MapPageRoute("rma", "Rma", "~/graficos/Rma.aspx");
            //Aniversáriantes do mês
            routes.MapPageRoute("aniversariante", "Aniversariante", "~/Aniversariante.aspx");
            //
            routes.MapPageRoute("erro", "Erro", "~/Erro.aspx");

            //MENU
            //routes.MapPageRoute("novoUsuario", "novoUsuario/{id}", "~/novoUsuario.aspx", false, new RouteValueDictionary { { "id", "1" } });
            //routes.MapPageRoute("listaPermissaoAcesso", "listaPermissaoAcesso", "~/listaPermissaoAcesso.aspx");         
            //Qdo houver mais de 1 parametro na url
            //routes.MapPageRoute("produtos2", "Produto/{id}/{categoria}", "~/Produto.aspx", false, new RouteValueDictionary { { "id", "1" }, { "categoria", "nenhuma" } });
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
