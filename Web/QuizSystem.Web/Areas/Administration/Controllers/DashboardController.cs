namespace QuizSystem.Web.Areas.Administration.Controllers
{
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService, RoleManager<IdentityRole> roleManager)
            : base(roleManager)
        {
            this.settingsService = settingsService;
            this.RoleManager = roleManager;
        }

        public RoleManager<IdentityRole> RoleManager { get; }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
