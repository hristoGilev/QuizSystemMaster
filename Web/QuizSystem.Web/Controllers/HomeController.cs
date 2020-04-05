namespace QuizSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels;
    using QuizSystem.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Identity;
    using QuizSystem.Data.Common.Repositories;

    public class HomeController : BaseController
    {
        private readonly IExamsService examServise;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ExamUser> repository;

        public HomeController(
            IExamsService examServise,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ExamUser> repository)
        {
            this.examServise = examServise;
            this.userManager = userManager;
            this.repository = repository;
        }

        public async System.Threading.Tasks.Task<IActionResult> IndexAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var exams = this.repository.All().Where(t => t.UserId == user.Id).Select(t => t.ExamId);
            var model = new IndexViewModel()
            { Exams = this.examServise.GetAll<ExamIndexViewModel>().Where(t => exams.Contains(t.Id.ToString())) };
            return this.View(model);
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
