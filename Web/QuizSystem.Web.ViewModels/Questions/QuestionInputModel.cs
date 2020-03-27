namespace QuizSystem.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class QuestionInputModel
    {
        [Required]
        [Range(1, 50)]
        public string Title { get; set; }

        [Required]
        [Range(1, 300)]
        public string Description { get; set; }
    }
}
