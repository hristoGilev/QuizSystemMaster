namespace QuizSystem.Data.Models
{
    using System;

    using QuizSystem.Data.Common.Models;

    public class ExamUser : BaseDeletableModel<string>
    {
        public ExamUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ExamId { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
