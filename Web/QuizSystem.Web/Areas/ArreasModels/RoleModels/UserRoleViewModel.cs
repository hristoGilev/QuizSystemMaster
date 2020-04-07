namespace QuizSystem.Web.Areas.ArreasModels.RoleModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class UserRoleViewModel : IMapFrom<ApplicationUser>
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public bool IsSelected { get; set; }
    }
}
