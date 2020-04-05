namespace QuizSystem.Web.ViewModels.ExamUses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class ExamUserView : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}
