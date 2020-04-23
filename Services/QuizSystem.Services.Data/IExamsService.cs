using QuizSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizSystem.Services.Data
{
    public interface IExamsService
    {
        Task<int> CreateAsync(string name, string descrption);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>(int? count = null);

        bool CheckForQuestions();

        Task DeleteAsync(int id);

        Task OpenorNotOpen(int id);

        bool CheckForQuestionsMulti();
    }
}
