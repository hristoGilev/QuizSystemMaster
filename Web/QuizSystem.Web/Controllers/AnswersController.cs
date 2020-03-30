using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizSystem.Web.Controllers
{
    public class AnswersController : Controller
    {


        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(int id, string answer)
        {
            return this.Json(id);
        }
    }
}
