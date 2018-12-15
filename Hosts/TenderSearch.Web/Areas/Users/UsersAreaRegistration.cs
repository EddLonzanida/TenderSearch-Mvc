using TenderSearch.Web.Utils;
using System.Web.Mvc;

namespace TenderSearch.Web.Areas.Users
{
    public class UsersAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Users";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RouteMapper.ConfigureArea(context, AreaName);
        }
    }
}