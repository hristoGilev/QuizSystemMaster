namespace QuizSystem.Web.ViewModels.QuestionsMultiCelect
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class QuestionMultiSelectOutputModel : IMapFrom<QuestionMultiSelect>
    {
        public int Id { get; set; }

        //public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string AnswerTypeA { get; set; }

        [Required]
        public string AnswerTypeB { get; set; }

        [Required]
        public string AnswerTypeC { get; set; }

        [Required]
        public string Answer { get; set; }

        public string ExamId { get; set; }
    }
}
