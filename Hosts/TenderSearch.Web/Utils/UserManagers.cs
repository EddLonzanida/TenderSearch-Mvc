using Eml.ControllerBase.Mvc;
using TenderSearch.Contracts.Infrastructure;

namespace TenderSearch.Web.Utils
{
    public static class UserManagers
    {
        private static MenuItem _userManagersMainMenus;
        public static MenuItem GetMainMenus()
        {
            return _userManagersMainMenus ?? (_userManagersMainMenus = MenuItems.GetMainMenus(MvcArea.UserManagers));
        }
    }
}