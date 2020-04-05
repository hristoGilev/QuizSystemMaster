namespace QuizSystem.Data.Models
{
    using QuizSystem.Data.Common.Models;
    using System.Collections.Generic;

    public class Exam : BaseDeletableModel<int>
    {
        public Exam()
        {
            this.Questions = new HashSet<Question>();
            this.ExamUser = new HashSet<ExamUser>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<ExamUser> ExamUser { get; set; }
    }
}
