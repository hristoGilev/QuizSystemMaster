namespace QuizSystem.Web.Areas.Administration.Controllers
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
    using QuizSystem.Web.ViewModels.ArreasModels.ReportModels;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
    public class AnswerUserQuestionController : Controller
    {
        private readonly IDeletableEntityRepository<Answer> repositoryAnswers;
        private readonly IDeletableEntityRepository<Question> repositoryQuestions;
        private readonly UserManager<ApplicationUser> usermaneger;

        public AnswerUserQuestionController(
            IDeletableEntityRepository<Answer> repositoryAnswers,
            IDeletableEntityRepository<Question> repositoryQuestions,
            UserManager<ApplicationUser> usermaneger)
        {
            this.repositoryAnswers = repositoryAnswers;
            this.repositoryQuestions = repositoryQuestions;
            this.usermaneger = usermaneger;
        }

        public IActionResult ById(string id, string userId)
        {
            var question = this.repositoryQuestions.All().FirstOrDefault(t => t.Id == int.Parse(id));
            var answer = this.repositoryAnswers.All().FirstOrDefault(t => t.QuestionId == id && t.UserId == userId);
            if (answer == null)
            {
                var user = this.usermaneger.FindByIdAsync(userId);
                UserQuestionReportModel model = new UserQuestionReportModel()
                {
                    Answer = "User don not response",
                    Description = question.Description,
                    UserName = user.Result.UserName,
                    Title = question.Title,
                };
                return this.View(model);
            }
            else
            {
                var user = this.usermaneger.FindByIdAsync(userId);
                UserQuestionReportModel model = new UserQuestionReportModel()
                {
                    Answer = answer.Content,
                    Description = question.Description,
                    UserName = user.Result.UserName,
                    Title = question.Title,
                };
                return this.View(model);
            }
        }
    }
}
