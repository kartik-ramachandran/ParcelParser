using System.Configuration;
using System.Web;

namespace ParcelLoader.Business
{
    public class JsonFileRepository
    {
        public static string JsonFilePath
        {
            get { return HttpContext.Current.Server.MapPath("~/Content/json/" + ConfigurationManager.AppSettings["JSONFileName"]); }
        }
    }
}