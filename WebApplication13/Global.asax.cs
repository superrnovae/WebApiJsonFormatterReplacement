using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace WebApplication13
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(new JsonFormatter()));
        }
    }
}
