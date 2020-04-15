namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IQuestionsMultiSelectService
    {
        Task<int> CreateAsync(
            string title,
            string description,
            string answersA,
            string answerB,
            string answerC,
            string userId);

        T GetById<T>(int id);

        Task DeliteAsync(int id);

        Task EditAsync(
            string title, string content,
            string answersA,
            string answerB,
            string answerC,
            int id);

        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
