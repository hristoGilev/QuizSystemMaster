namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAnswersSerrvice
    {
        Task<int> CreateAsync(string questionId, string content, string userId);
    }
}
