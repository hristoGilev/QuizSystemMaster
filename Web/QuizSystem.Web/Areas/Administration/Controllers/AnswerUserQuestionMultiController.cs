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
    [Authorize(Roles = "Administrator,Moderator")]
    public class AnswerUserQuestionMultiController : Controller
    {
        private readonly IDeletableEntityRepository<QuestionMultiSelect> repositoryQuestions;
        private readonly UserManager<ApplicationUser> usermaneger;
        private readonly IDeletableEntityRepository<AnswerMultiSelect> repositoryAnswersMulti;

        public AnswerUserQuestionMultiController(
            IDeletableEntityRepository<QuestionMultiSelect> repositoryQuestions,
            UserManager<ApplicationUser> usermaneger,
            IDeletableEntityRepository<AnswerMultiSelect> repositoryAnswersMulti)
        {
            this.repositoryQuestions = repositoryQuestions;
            this.usermaneger = usermaneger;
            this.repositoryAnswersMulti = repositoryAnswersMulti;
        }

        public IActionResult ById(string id, string userId)
        {
            var question = this.repositoryQuestions.All().FirstOrDefault(t => t.Id == int.Parse(id));
            var answer = this.repositoryAnswersMulti.All().FirstOrDefault(t => t.QuestionMultiSelectId == id && t.UserId == userId);
            var user = this.usermaneger.FindByIdAsync(userId);
            if (answer == null)
            {
                UserQuestionMultiReport model = new UserQuestionMultiReport()
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
                UserQuestionMultiReport model = new UserQuestionMultiReport()
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
