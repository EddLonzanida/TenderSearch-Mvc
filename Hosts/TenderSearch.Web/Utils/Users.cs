using TenderSearch.Contracts.Infrastructure;
using Eml.ControllerBase.Mvc;

namespace TenderSearch.Web.Utils
{
    public static class Users
    {
        private static MenuItem _usersMainMenus;

        public static MenuItem GetMainMenus()
        {
            return _usersMainMenus ?? (_usersMainMenus = MenuItems.GetMainMenus(MvcArea.Users));
        }
    }
}