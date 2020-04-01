namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;

    public class AnswersService : IAnswersSerrvice
    {
        private readonly IDeletableEntityRepository<Answer> repositoryAnswer;

        public AnswersService(IDeletableEntityRepository<Answer> repositoryAnswer)
        {
            this.repositoryAnswer = repositoryAnswer;
        }

        public async Task<int> CreateAsync(string questionId, string content, string userId)
        {
            var answer = new Answer() { Content = content, QuestionId = questionId, UserId = userId };

            await this.repositoryAnswer.AddAsync(answer);
            await this.repositoryAnswer.SaveChangesAsync();
            return answer.Id;
        }
    }
}
