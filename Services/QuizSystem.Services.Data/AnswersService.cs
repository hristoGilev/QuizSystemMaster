namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ganss.XSS;
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
            var sanitizer = new HtmlSanitizer();
            var contentnew = sanitizer.Sanitize(content);
            var result = this.repositoryAnswer.All().FirstOrDefault(n => n.UserId == userId && n.QuestionId == questionId);
            if (result == null)
            {
                var answer = new Answer() { Content = contentnew, QuestionId = questionId, UserId = userId };

                await this.repositoryAnswer.AddAsync(answer);
                await this.repositoryAnswer.SaveChangesAsync();
                return answer.Id;
            }
            else
            {
                result.Content = contentnew;
                this.repositoryAnswer.Update(result);
                await this.repositoryAnswer.SaveChangesAsync();
                return result.Id;
            }
        }
    }
}
