using QuizSystem.Data.Models;
using QuizSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystem.Web.ViewModels.ExamUses
{
     public class ExamUserView : IMapFrom<ApplicationUser>
    {

        public string UserName { get; set; }
    }
}
