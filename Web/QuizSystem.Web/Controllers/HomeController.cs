namespace QuizSystem.Web.Controllers
{
    using System.Diagnostics;

    using QuizSystem.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels.Home;
    using QuizSystem.Data.Models;

    public class HomeController : BaseController
    {
        private readonly IExamsService examServise;

        public HomeController(IExamsService examServise)
        {
            this.examServise = examServise;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel() {Exams = this.examServise.GetAll<ExamIndexViewModel>() };
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
