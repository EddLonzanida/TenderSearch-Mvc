using TenderSearch.Web.Utils;
using System.Web.Mvc;

namespace TenderSearch.Web.Areas.Admins
{
    public class AdminsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admins";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RouteMapper.ConfigureArea(context, AreaName);

            //context.MapRoute(
            //    name: $"{AreaName}_index_with_parent",
            //    url: "Admins/{controller}/{parentId}/{action}",
            //    defaults: new { action = ActionNames.IndexWithParent },
            //    namespaces: new[] { $"TenderSearch.Web.Areas.{AreaName}.Controllers" }
            //);

            //context.MapRoute(
            //    name: $"{AreaName}_suggestion_with_parent",
            //    url: "Admins/{controller}/{parentId}/SuggestionsWithParent",
            //    defaults: new { action = ActionNames.SuggestionsWithParent },
            //    namespaces: new[] { $"TenderSearch.Web.Areas.{AreaName}.Controllers" }
            //);

            //context.MapRoute(
            //    name: $"{AreaName}_create_with_parent",
            //    url: "Admins/{controller}/{parentId}/CreateWithParent",
            //    defaults: new { action = ActionNames.CreateWithParent },
            //    namespaces: new[] { $"TenderSearch.Web.Areas.{AreaName}.Controllers" }
            //);

            //context.MapRoute(
            //    name: $"{AreaName}_default",
            //    url: "Admins/{controller}/{action}/{id}",
            //    defaults: new { action = ActionNames.Index, id = UrlParameter.Optional },
            //    namespaces: new[] { $"TenderSearch.Web.Areas.{AreaName}.Controllers" }
            //);
        }
    }
}