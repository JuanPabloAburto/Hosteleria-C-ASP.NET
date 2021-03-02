using System.Web;
using System.Web.Mvc;

namespace Hostal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filter.Verificar());
        }
    }
}
