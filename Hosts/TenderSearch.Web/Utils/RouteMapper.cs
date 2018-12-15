using TenderSearch.Contracts.Infrastructure;
using System.Web.Mvc;

namespace TenderSearch.Web.Utils
{
    public static class RouteMapper
    {
        public static void ConfigureArea(AreaRegistrationContext context, string areaName)
        {
            context.MapRoute(
                name: $"{areaName}_index_with_parent",
                url: areaName + "/{controller}/{parentId}/" + ActionNames.IndexWithParent,
                defaults: new { action = ActionNames.IndexWithParent },
                namespaces: new[] { $"TenderSearch.Web.Areas.{areaName}.Controllers" }
            );

            context.MapRoute(
                name: $"{areaName}_suggestion_with_parent",
                url: areaName + "/{controller}/{parentId}/" + ActionNames.SuggestionsWithParent,
                defaults: new { action = ActionNames.SuggestionsWithParent },
                namespaces: new[] { $"TenderSearch.Web.Areas.{areaName}.Controllers" }
            );

            context.MapRoute(
                name: $"{areaName}_create_with_parent",
                url: areaName + "/{controller}/{parentId}/" + ActionNames.CreateWithParent,
                defaults: new { action = ActionNames.CreateWithParent },
                namespaces: new[] { $"TenderSearch.Web.Areas.{areaName}.Controllers" }
            );

            context.MapRoute(
                name: $"{areaName}_default",
                url: areaName + "/{controller}/{action}/{id}",
                defaults: new { action = ActionNames.Index, id = UrlParameter.Optional },
                namespaces: new[] { $"TenderSearch.Web.Areas.{areaName}.Controllers" }
            );
        }
    }
}