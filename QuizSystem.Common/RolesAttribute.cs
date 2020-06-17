namespace QuizSystem.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Authorization;

    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(List<string> roles)
        {
            this.Roles = string.Join(",", roles);
        }
    }
}
