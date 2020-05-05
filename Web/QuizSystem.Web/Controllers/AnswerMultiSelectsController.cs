namespace QuizSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels;
    using QuizSystem.Web.ViewModels.Exams;

    public class AnswerMultiSelectsController : Controller
    {
        private readonly IAnswerMultiSelectsService answerMultiSelectsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IExamUsersService examUsersService;
        private readonly IExamsService examsService;

        public AnswerMultiSelectsController(
            IAnswerMultiSelectsService answerMultiSelectsService,
            UserManager<ApplicationUser> userManager,
            IExamUsersService examUsersService,
            IExamsService examsService)
        {
            this.answerMultiSelectsService = answerMultiSelectsService;
            this.userManager = userManager;
            this.examUsersService = examUsersService;
            this.examsService = examsService;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string id, string examId, string answer)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var exams = this.examUsersService.Exams(user.Id);
            if (!exams.Contains(examId))
            {
                ErrorViewModelTekst model = new ErrorViewModelTekst() { Tekst = "NE ste dobaven kum izpita!" };
                return this.View("Error", model);
            }

            var exam = this.examsService.GetById<ExamViewModel>(int.Parse(examId));
            var questionFromExamIdM = exam.QuestionMultiSelects.Select(t => t.Id).ToList();
            if (exam == null || !questionFromExamIdM.Contains(int.Parse(id)) || !exam.IsOpen)
            {
                ErrorViewModelTekst model = new ErrorViewModelTekst() { Tekst = "Connot be answered!" };
                return this.View("Error", model);
            }

            await this.answerMultiSelectsService.CreateAsync(id, answer, user.Id);
            return this.RedirectToAction("ById", "Exams", new { id = examId });
        }
    }
}
