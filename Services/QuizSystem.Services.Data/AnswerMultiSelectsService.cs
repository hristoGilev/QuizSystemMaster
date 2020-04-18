namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;

    public class AnswerMultiSelectsService : IAnswerMultiSelectsService
    {
        private readonly IDeletableEntityRepository<AnswerMultiSelect> repository;

        public AnswerMultiSelectsService(IDeletableEntityRepository<AnswerMultiSelect> repository)
        {
            this.repository = repository;
        }

        public async Task<int> CreateAsync(string questionId, string content, string userId)
        {
            var result = this.repository.All().FirstOrDefault(n => n.UserId == userId && n.QuestionMultiSelectId == questionId);
            if (result == null)
            {
                var answer = new AnswerMultiSelect() { Content = content, QuestionMultiSelectId = questionId, UserId = userId };

                await this.repository.AddAsync(answer);
                await this.repository.SaveChangesAsync();
                return answer.Id;
            }
            else
            {
                result.Content = content;
                this.repository.Update(result);
                await this.repository.SaveChangesAsync();
                return result.Id;
            }
        }
    }
}
