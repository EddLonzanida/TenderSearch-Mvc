using System;
using System.Collections.Generic;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;

namespace TenderSearch.Web.ViewModels.HomeViewModels
{
    public class HomeIndexViewModel : LayoutViewModelBase
    {
        public HomeIndexViewModel()
        {
        }

        public HomeIndexViewModel(string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
        }
    }
}