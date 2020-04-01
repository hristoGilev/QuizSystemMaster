using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystem.Web.ViewModels.ExamUses
{
    public class ExamUsersview
    {
        public int ExamId { get; set; }

        public IEnumerable<ExamUserView> examUsers { get; set; }
    }
}
