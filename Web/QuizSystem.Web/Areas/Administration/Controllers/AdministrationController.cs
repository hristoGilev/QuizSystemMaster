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
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.Areas.ArreasModels.RoleModels;
    using QuizSystem.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public AdministrationController(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IUsersService usersService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var role = new ApplicationRole() { Name = model.RoleName };
                await this.roleManager.CreateAsync(role);
                return this.View();
            }


            return this.View();
        }

        [HttpGet]
        public IActionResult ChooseaRole()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRoleAsync(string id)
        {
            var model = new List<UserRoleViewModel>();
            var users = this.userManager.Users.ToList();
            foreach (var item in users)
            {
                var user = new UserRoleViewModel()
                { UserName = item.UserName, UserId = item.Id };
                if (await this.userManager.IsInRoleAsync(item, id))
                {
                    user.IsSelected = true;
                }
                else
                {
                    user.IsSelected = false;
                }

                model.Add(user);
            }

            this.ViewData["ChooseRole"] = id;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInRoleAsync(string[] username, string id)
        {
            foreach (var item in username)
            {
                var user = await this.userManager.FindByNameAsync(item);
                if (await this.userManager.IsInRoleAsync(user, id))
                {
                    await this.userManager.RemoveFromRoleAsync(user, id);
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, id);
                }
            }

            //var model=this.userManager.Users.Select()
            return this.Ok();
        }
    }
}
