using TenderSearch.Contracts.Infrastructure;
using Eml.ControllerBase.Mvc;

namespace TenderSearch.Web.Utils
{
    public static class Admins
    {
        private static MenuItem _adminsMainMenus;

        public static MenuItem GetMainMenus()
        {
            return _adminsMainMenus ?? (_adminsMainMenus = MenuItems.GetMainMenus(MvcArea.Admins));
        }
    }
}