using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizSystem.Web.Areas.ArreasModels.RoleModels
{
    public class ListUserRoleViewModel
    {
        public ICollection<UserRoleViewModel> UserRoleViewModels { get; set; }
    }
}
