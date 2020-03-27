namespace QuizSystem.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class QuestionsViewOutputModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        //public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
