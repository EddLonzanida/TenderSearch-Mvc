using System.Web.Mvc;

namespace TenderSearch.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Enable Logging
            filters.Add(new HandleErrorAttribute());
        }
    }
}
