namespace QuizSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels.Exams;

    public class ExamsController : Controller
    {
        private readonly IExamsService examsService;

        public ExamsController(IExamsService examsService)
        {
            this.examsService = examsService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(string name, string description)
        {
          var examId = await this.examsService.CreateAsync(name, description);
          return this.RedirectToAction(nameof(this.ById), new { id = examId });
        }

        [Authorize]
        public IActionResult ById(int id)
        {
            var model = this.examsService.GetById<ExamViewModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }
    }
}
