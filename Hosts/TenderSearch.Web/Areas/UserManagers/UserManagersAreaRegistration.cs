using TenderSearch.Web.Utils;
using System.Web.Mvc;

namespace TenderSearch.Web.Areas.UserManagers
{
    public class UserManagersAreaRegistration : AreaRegistration
    {
        public override string AreaName => "UserManagers";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RouteMapper.ConfigureArea(context, AreaName);
        }
    }
}