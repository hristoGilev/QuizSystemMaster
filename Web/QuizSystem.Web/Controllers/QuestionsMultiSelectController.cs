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
    using QuizSystem.Web.ViewModels.QuestionsMultiCelect;

    public class QuestionsMultiSelectController : Controller
    {
        private readonly IQuestionsMultiSelectService questionsMultiSelectService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ExamUser> repository;
        private readonly IDeletableEntityRepository<AnswerMultiSelect> answersRepossitory;
        private readonly IExamsService examsService;

        public QuestionsMultiSelectController(
            IQuestionsMultiSelectService questionsMultiSelectService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ExamUser> repository,
            IDeletableEntityRepository<AnswerMultiSelect> answersRepossitory,
            IExamsService examsService)
        {
            this.questionsMultiSelectService = questionsMultiSelectService;
            this.userManager = userManager;
            this.repository = repository;
            this.answersRepossitory = answersRepossitory;
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
        public async Task<IActionResult> CreateAsync(QuestionMultySelectInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var questionId = await this.questionsMultiSelectService.CreateAsync(
                model.Title,
                model.Description,
                model.AnswerTypeA,
                model.AnswerTypeB,
                model.AnswerTypeC,
                user.Id);
            return this.RedirectToAction("ById", new { id = questionId });
        }

        [Authorize]
        public async Task<IActionResult> ByIdAsync(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var exams = this.repository.All().Where(t => t.UserId == user.Id).Select(t => t.ExamId);
            var model = this.questionsMultiSelectService.GetById<QuestionMultiSelectOutputModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            var answer = this.answersRepossitory.All().
                        FirstOrDefault(n => n.UserId == user.Id && n.QuestionMultiSelectId == model.Id.ToString());

            if (answer != null)
            {
                model.Answer = answer.Content;
            }

            if (this.User.IsInRole(GlobalConstants.ModeratorRoleName) || this.User.IsInRole(GlobalConstants.AdministratorRoleName))
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
        public IActionResult List()
        {
            var model = this.questionsMultiSelectService.GetAll<QuestionMultiSelectOutputModel>();

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await this.questionsMultiSelectService.DeleteAsync(int.Parse(id));
            return this.RedirectToAction("LIst");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.questionsMultiSelectService.GetById<QuestionMultiSelectOutputModel>(id);
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> EditAsync(QuestionMultiSelectEditModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.questionsMultiSelectService.EditAsync(
                model.Title,
                model.Description,
                model.AnswerTypeA,
                model.AnswerTypeB,
                model.AnswerTypeC,
                model.Id);

            return this.RedirectToAction("ById", new { id = model.Id });
        }
    }
}
