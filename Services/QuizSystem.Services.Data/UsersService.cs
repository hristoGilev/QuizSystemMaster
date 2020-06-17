namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;
    using QuizSystem.Web.Areas.ArreasModels.RoleModels;

    public class UsersService : IUsersService
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> repositoryUsers;

        public UsersService(
             RoleManager<ApplicationRole> roleManager,
             UserManager<ApplicationUser> userManager,
             IDeletableEntityRepository<ApplicationUser> repositoryUsers)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.repositoryUsers = repositoryUsers;
        }

        public async Task<IList<UserRoleViewModel>> ChecRoleAsync(string id)
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

            return model;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query =
               this.repositoryUsers.All().OrderBy(x => x.UserName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
