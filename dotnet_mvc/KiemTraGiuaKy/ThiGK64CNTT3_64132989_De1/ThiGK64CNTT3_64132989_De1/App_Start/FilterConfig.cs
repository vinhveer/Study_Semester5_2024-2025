using System.Web;
using System.Web.Mvc;

namespace ThiGK64CNTT3_64132989_De1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
