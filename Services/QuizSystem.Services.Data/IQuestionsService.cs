namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IQuestionsService
    {
        Task<int> CreateAsync(string title, string content,  string userId);

        T GetById<T>(int id);
    }
}
