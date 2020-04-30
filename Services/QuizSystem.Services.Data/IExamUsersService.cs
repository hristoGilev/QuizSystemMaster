namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IExamUsersService
    {
        Task AddUserToExamAsync(int idExam, string[] usersName);

        public IQueryable<string> Exams(string userId);
    }
}
