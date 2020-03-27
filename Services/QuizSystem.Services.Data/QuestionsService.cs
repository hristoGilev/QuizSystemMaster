namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> repository;

        public QuestionsService(IDeletableEntityRepository<Question> repository)
        {
            this.repository = repository;
        }

        public async Task<int> CreateAsync(string title, string content, string userId)
        {
            var newQuestion = new Question() { Title = title, Description = content, UserId = userId };

            await this.repository.AddAsync(newQuestion);
            await this.repository.SaveChangesAsync();
            return newQuestion.Id;
        }

        public T GetById<T>(int id)
        {
            var questuion = this.repository.All().Where(n => n.Id == id).To<T>().FirstOrDefault();

            return questuion;
        }
    }
}
