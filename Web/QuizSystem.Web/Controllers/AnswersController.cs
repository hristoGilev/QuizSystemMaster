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

    public class AnswersController : Controller
    {
        private readonly IAnswersService answersService;
        private readonly UserManager<ApplicationUser> userManager;

        public AnswersController(IAnswersService answersService, UserManager<ApplicationUser> userManager)
        {
            this.answersService = answersService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(string id, string answer)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.answersService.CreateAsync(id, answer, user.Id);

            return this.Ok();

        }
    }
}
