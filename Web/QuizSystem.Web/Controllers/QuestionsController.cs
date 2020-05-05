namespace QuizSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Common;
    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels.Exams;
    using QuizSystem.Web.ViewModels.Questions;

    public class QuestionsController : Controller
    {
        private readonly IQuestionsService questionsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IExamUsersService examUsersService;
        private readonly IAnswersSerrvice answersSerrvice;
        private readonly IExamsService examsService;

        public QuestionsController(
            IQuestionsService questionsService,
            UserManager<ApplicationUser> userManager,
            IExamUsersService examUsersService,
            IAnswersSerrvice answersSerrvice,
            IExamsService examsService)
        {
            this.questionsService = questionsService;
            this.userManager = userManager;
            this.examUsersService = examUsersService;
            this.answersSerrvice = answersSerrvice;
            this.examsService = examsService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
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
            var exams = this.examUsersService.Exams(user.Id);
            var model = this.questionsService.GetById<QuestionsViewOutputModel>(id);

            if (model == null)
            {
                return this.NotFound();
            }

            var answer = this.answersSerrvice.Result(user.Id, model.Id);
            if (answer != null)
            {
                model.Answer = answer.Content;
            }

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName) || this.User.IsInRole(GlobalConstants.ModeratorRoleName))
            {
                return this.View(model);
            }

            if (!this.examsService.GetById<ExamViewModel>(int.Parse(model.ExamId)).IsOpen)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!exams.Contains(model.ExamId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.questionsService.GetById<QuestionsViewOutputModel>(id);
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> EditAsync(QoesttionEditModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.questionsService.EditAsync(model.Title, model.Description, model.Id);

            return this.RedirectToAction("ById", "Questions", new { id = model.Id });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await this.questionsService.DeleteAsync(int.Parse(id));
            return this.RedirectToAction("List", "Questions");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        public IActionResult List()
        {
            var model = this.questionsService.GetAll<QuestionsViewOutputModel>();

            return this.View(model);
        }
    }
}
