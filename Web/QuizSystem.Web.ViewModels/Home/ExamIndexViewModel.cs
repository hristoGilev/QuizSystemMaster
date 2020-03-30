namespace QuizSystem.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class ExamIndexViewModel : IMapFrom<Exam>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
