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
    using QuizSystem.Web.ViewModels.Questions;

    public class QuestionsController : Controller
    {
        private readonly IQuestionsService questionsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ExamUser> repository;
        private readonly IDeletableEntityRepository<Answer> answersRepossitory;

        public QuestionsController(
            IQuestionsService questionsService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ExamUser> repository,
            IDeletableEntityRepository<Answer> answersRepossitory)
        {
            this.questionsService = questionsService;
            this.userManager = userManager;
            this.repository = repository;
            this.answersRepossitory = answersRepossitory;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(QuestionInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var questionId = await this.questionsService.CreateAsync(model.Title, model.Description, user.Id);

            return this.RedirectToAction("ById", new { id= questionId });
        }

        [Authorize]
        public async Task<IActionResult> ByIdAsync(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var exams = this.repository.All().Where(t => t.UserId == user.Id).Select(t => t.ExamId);
            var model = this.questionsService.GetById<QuestionsViewOutputModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            if (!exams.Contains(model.ExamId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            var answer = this.answersRepossitory.All().
                        FirstOrDefault(n => n.UserId == user.Id && n.QuestionId == model.Id.ToString());

            if (answer != null)
            {
                model.Answer = answer.Content;
            }

            return this.View(model);
        }
    }
}
