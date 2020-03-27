namespace QuizSystem.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class QuestionInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(200 , MinimumLength =5)]
        public string Description { get; set; }
    }
}
