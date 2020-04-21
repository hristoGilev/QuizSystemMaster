namespace QuizSystem.Web.Areas.ArreasModels.ReportModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class Userraport : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
