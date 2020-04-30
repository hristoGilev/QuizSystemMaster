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
    using QuizSystem.Web.ViewModels;
    using QuizSystem.Web.ViewModels.Exams;

    public class ExamsController : Controller
    {
        private readonly IExamsService examsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ExamUser> repository;

        public ExamsController(
            IExamsService examsService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ExamUser> repository)
        {
            this.examsService = examsService;
            this.userManager = userManager;
            this.repository = repository;
        }

        [HttpGet]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        public async Task<IActionResult> CreateAsync(string name, string description)
        {
            if (!this.examsService.CheckForQuestions())
            {
                ErrorViewModelTekst model = new ErrorViewModelTekst() { Tekst = "Няма свободни въпроси със отворен отговор, създайте нови въпроси. " };
                return this.View("Error", model);
            }

            if (!this.examsService.CheckForQuestionsMulti())
            {
                ErrorViewModelTekst model = new ErrorViewModelTekst() { Tekst = "Няма свободни въпроси с отговор с избор, създайте нови въпроси. " };
                return this.View("Error", model);
            }

            var examId = await this.examsService.CreateAsync(name, description);
            return this.RedirectToAction("ById", new { id = examId });
        }

        [Authorize]
        public async Task<IActionResult> ByIdAsync(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var exams = this.repository.All().Where(t => t.UserId == user.Id).Select(t => t.ExamId);
            var model = this.examsService.GetById<ExamViewModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName) || this.User.IsInRole(GlobalConstants.ModeratorRoleName))
            {
                return this.View(model);
            }
            else

            if (!exams.Contains(model.Id.ToString()) || !model.IsOpen)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        public async Task<IActionResult> DeleteExamAsync(string id)
        {
          await this.examsService.DeleteAsync(int.Parse(id));

          return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        public async Task<IActionResult> OpenOrCloseExamAsync(string id)
        {
            await this.examsService.OpenorNotOpen(int.Parse(id));
            return this.RedirectToAction("ById", new { id = int.Parse(id) });
        }
    }
}
