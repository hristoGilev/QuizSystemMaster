using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystem.Common
{
    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(List<string> roles)
        {
            this.Roles = string.Join(",", roles);
        }
    }
}
