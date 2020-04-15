namespace QuizSystem.Web.ViewModels.QuestionsMultiCelect
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class QuestionMultiSelectEditModel
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string AnswerTypeA { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string AnswerTypeB { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string AnswerTypeC { get; set; }
    }
}
