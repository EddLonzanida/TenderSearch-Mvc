namespace TenderSearch.Web.Utils
{
    public sealed class PageTitle
    {
        public string Value { get; }

        public PageTitle(string area, string controllerName, string mvcActionName)
        {
            switch (mvcActionName)
            {
                case "Index" when controllerName == "Home":

                    Value = string.IsNullOrWhiteSpace(area) ? controllerName : area;

                    break;

                case "Index":

                    Value = controllerName;

                    break;

                default:

                    if (controllerName == "Home") Value = string.IsNullOrWhiteSpace(area) ? controllerName : area;
                    else Value = $"{controllerName}-{mvcActionName}";

                    break;
            }
        }
    }
}