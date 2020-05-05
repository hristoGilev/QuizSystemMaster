namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using QuizSystem.Data.Models;

    public interface IAnswerMultiSelectsService
    {
        Task<int> CreateAsync(string questionId, string content, string userId);

        public AnswerMultiSelect Result(string userId, int modelId);
    }
}
