namespace QuizSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using QuizSystem.Data.Common.Models;

    public class AnswerMultiSelect : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string QuestionMultiSelectId { get; set; }

        public virtual QuestionMultiSelect QuestionMultiSelect { get; set; }
    }
}
