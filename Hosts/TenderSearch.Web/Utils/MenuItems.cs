using System.Linq;
using System.Web.Mvc;
using Eml.ControllerBase.Mvc;
using Eml.Extensions;
using TenderSearch.Contracts.Infrastructure;

namespace TenderSearch.Web.Utils
{
    public static class MenuItems
    {
        public static MenuItem GetMainMenus(string area)
        {
            //var repository = MvcApplication.ClassFactory.Container
            //    .GetExportedValue<IDataRepositorySoftDeleteInt<DocumentType>>();

            //var documentTypes = repository.GetAll();

            var ns = $"TenderSearch.Web.Areas.{area}.Controllers";
            var assembly = typeof(Users).Assembly;
            var classNames = assembly.GetClassNames(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace == ns)
                .OrderBy(r => r)
                .Select(c => c.Replace("Controller", string.Empty));
            var rootMenu = new MenuItem();
            var childMenus = classNames.Select(c => new MenuItem
            {
                AreaName = area,
                ActionName = ActionNames.Index,
                ControllerName = c
            }).ToList();

            rootMenu.Children = childMenus;

            return rootMenu;
        }
    }
}