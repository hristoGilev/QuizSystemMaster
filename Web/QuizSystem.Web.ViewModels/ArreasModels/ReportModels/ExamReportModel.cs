namespace QuizSystem.Web.Areas.ArreasModels.ReportModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class ExamReportModel : IMapFrom<Exam>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Users { get; set; }

        public bool IsOpen { get; set; }
    }
}
