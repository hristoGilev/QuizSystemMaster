namespace QuizSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using QuizSystem.Data.Common.Models;

    public class QuestionMultiSelect : BaseDeletableModel<int>
    {
        public QuestionMultiSelect()
        {
            this.AnswersMultiSelect = new HashSet<AnswerMultiSelect>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AnswerTypeA { get; set; }

        public string AnswerTypeB { get; set; }

        public string AnswerTypeC { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<AnswerMultiSelect> AnswersMultiSelect { get; set; }

        public string ExamId { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
