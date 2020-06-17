namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using QuizSystem.Data.Models;
    using QuizSystem.Web.Areas.ArreasModels.RoleModels;

    public interface IUsersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<IList<UserRoleViewModel>> ChecRoleAsync(string id);
    }
}
