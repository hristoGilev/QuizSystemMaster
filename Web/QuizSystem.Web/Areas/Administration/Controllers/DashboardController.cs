namespace QuizSystem.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels.Administration.Dashboard;

    public class DashboardController /*: AdministrationController*/
    {
        private readonly ISettingsService settingsService;
        private readonly RoleManager<ApplicationRole> roleManager;

        public DashboardController(ISettingsService settingsService, RoleManager<ApplicationRole> roleManager)
        //: base(roleManager)
        {
            this.settingsService = settingsService;
            this.roleManager = roleManager;
        }

        //        public IActionResult Index()
        //        {
        //            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
        //            return this.View(viewModel);
        //        }
        //    }
    }
}