namespace QuizSystem.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizSystem.Common;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Data;
    using QuizSystem.Web.Areas.ArreasModels.RoleModels;
    using QuizSystem.Web.Controllers;
    using System.Linq;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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
                return this.RedirectToAction("Home", "Idex");
            }

            return this.View(model);
        }

        [HttpGet]
        public IActionResult ChooseaRole()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRoleAsync(string id)
        {
            return this.Json(id);
            ////roleName = "Administrator";
            //var model = new ListUserRoleViewModel() { UserRoleViewModels = new List<UserRoleViewModel>() };
            //var users = this.userManager.Users.ToList();
            //foreach (var item in users)
            //{
            //    var user = new UserRoleViewModel()
            //    { UserName = item.UserName, UserId = item.Id };
            //    if (await this.userManager.IsInRoleAsync(item, id))
            //    {
            //        user.IsSelected = true;
            //    }
            //    else
            //    {
            //        user.IsSelected = false;
            //    }

            //model.UserRoleViewModels.Add(user);
       
    
            //return this.View(model);
        }
    }
}
