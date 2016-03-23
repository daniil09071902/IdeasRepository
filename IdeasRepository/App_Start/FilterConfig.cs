using IdeasRepository.Filters;
using System.Web;
using System.Web.Mvc;

namespace IdeasRepository
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RecordErrorHandlerAttribute());
        }
    }
}