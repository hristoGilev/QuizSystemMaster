namespace QuizSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Common;
    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels;
    using QuizSystem.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IExamsService examServise;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IExamUsersService examUsersService;

        public HomeController(
            IExamsService examServise,
            UserManager<ApplicationUser> userManager,
            IExamUsersService examUsersService)
        {
            this.examServise = examServise;
            this.userManager = userManager;
            this.examUsersService = examUsersService;
        }

        public async System.Threading.Tasks.Task<IActionResult> IndexAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName) || this.User.IsInRole(GlobalConstants.ModeratorRoleName))
            {
                var model1 = new IndexViewModel() { Exams = this.examServise.GetAll<ExamIndexViewModel>() };
                return this.View(model1);
            }
            else
            {
                if (user != null)
                {
                    var exams1 = this.examUsersService.Exams(user.Id);
                    var model = new IndexViewModel()
                    { Exams = this.examServise.GetAll<ExamIndexViewModel>().Where(t => (exams1.Contains(t.Id.ToString()) && t.IsOpen == true)) };
                    return this.View(model);
                }

                return this.View();
            }
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
