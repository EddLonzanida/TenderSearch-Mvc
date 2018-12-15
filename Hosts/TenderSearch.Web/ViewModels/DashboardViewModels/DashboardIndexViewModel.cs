using System;
using System.Collections.Generic;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;

namespace TenderSearch.Web.ViewModels.DashboardViewModels
{
    public class DashboardIndexViewModel : LayoutViewModelBase
    {
        public IEnumerable<string> DasboardItems { get; set; }

        //public FooterViewModel FooterViewModel { get; }

        public string UserName { get; set; }

        //public int ItemsPerRow { get; }

        //public string Title1 { get; }

        //public string DashboardMessage { get; }

        //public DashboardIndexViewModel(IEnumerable<string> dasboardItems, FooterViewModel footerViewModel, string userName, int itemsPerRow, string title1, string dashboardMessage)
        //{
        //    DasboardItems = dasboardItems;
        //    FooterViewModel = footerViewModel;
        //    UserName = userName;
        //    ItemsPerRow = itemsPerRow;
        //    Title1 = title1;
        //    DashboardMessage = dashboardMessage;
        //}
        public DashboardIndexViewModel()
        {
        }

        public DashboardIndexViewModel(string userName, IEnumerable<string> dasboardItems, string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            DasboardItems = dasboardItems;
            UserName = userName;
        }
    }
}