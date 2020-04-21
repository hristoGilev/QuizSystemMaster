namespace QuizSystem.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Common;
    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.Areas.ArreasModels.ReportModels;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName /*|| Roles= "Administrator"*/)]
    public class DashboardController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IExamsService examsService;
        private readonly IDeletableEntityRepository<ExamUser> repository;
        private readonly UserManager<ApplicationUser> usermaneger;

        public DashboardController(
            IUsersService usersService,
            IExamsService examsService,
            IDeletableEntityRepository<ExamUser> repository,
            UserManager<ApplicationUser> usermaneger)
        {
            this.usersService = usersService;
            this.examsService = examsService;
            this.repository = repository;
            this.usermaneger = usermaneger;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult UserReport()
        {
            var model = this.usersService.GetAll<Userraport>();

            return this.View(model);
        }

        [HttpPost]
        public IActionResult GetAllExamsForUser(string user)
        {
            var model = new List<ExamReportModel>();

            var exams = this.repository.All().Where(t => t.UserId == user).Select(t => t.ExamId);
            model = this.examsService.GetAll<ExamReportModel>().Where(t => exams.Contains(t.Id.ToString())).ToList();
            this.ViewData["userId"] = user;
            this.ViewData["UserName"] = this.usermaneger.Users.Where(t => t.Id == user).First().UserName;
            return this.View(model);
        }

        public IActionResult GetAllExams()
        {

           var model = this.examsService.GetAll<ExamReportModel>();
           return this.View(model);
        }

        public IActionResult ByIdExam(int id)
        {
            var model = this.examsService.GetById<ExamReportModel>(id);

            model.Users = this.repository.All().Where(h => h.ExamId == id.ToString()).Select(b => b.User.UserName);

            return this.View(model);
        }

        public IActionResult ByIdExamUser(int id, string userid)
        {
            var model = this.examsService.GetById<ExamReportModel>(id);
            return this.View(model);
        }

    }
}
