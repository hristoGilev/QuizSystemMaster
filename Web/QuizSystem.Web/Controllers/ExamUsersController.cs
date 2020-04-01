using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizSystem.Services.Data;
using QuizSystem.Web.ViewModels.ExamUses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizSystem.Web.Controllers
{
    public class ExamUsersController : Controller
    {
        private readonly IUsersService usersService;

        public ExamUsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddUsersToExam(int id)
        {
            var model = new ExamUsersview() { examUsers = this.usersService.GetAll<ExamUserView>(), ExamId = id };
            return this.View(model);

        }

        //[Authorize]
        //[HttpPost]
        //public IActionResult AddUsersToExam(int id)
        //{

        //    return this.View(id);

        //}

    }
}
