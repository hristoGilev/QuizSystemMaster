namespace QuizSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class ExamViewModel : IMapFrom<Exam>
    {
        public ExamViewModel()
        {
            this.Questions = new HashSet<Question>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<QuestionMultiSelect> QuestionMultiSelects { get; set; }

        public string Content { get; set; }

        public int Id { get; set; }

        public bool IsOpen { get; set; }
    }
}
