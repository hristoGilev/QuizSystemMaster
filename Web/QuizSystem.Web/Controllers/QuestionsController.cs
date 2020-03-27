namespace QuizSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.ViewModels.Questions;

    public class QuestionsController : Controller
    {
        private readonly IQuestionsService questionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionsController(IQuestionsService questionsService, UserManager<ApplicationUser> userManager)
        {
            this.questionsService = questionsService;
            this.userManager = userManager;
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
            var questionId = this.questionsService.CreateAsync(model.Title, model.Description, user.Id);

            return this.View();
        }


        public IActionResult ById(int id)
        {

            var model = this.questionsService.GetById<QuestionsViewOutputModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }


    }
}
