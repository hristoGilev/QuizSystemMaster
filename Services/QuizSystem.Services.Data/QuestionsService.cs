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

        public async Task DeleteAsync(int id)
        {
            var questuion = this.repository.All().Where(n => n.Id == id).FirstOrDefault();
            this.repository.Delete(questuion);
            await this.repository.SaveChangesAsync();
        }

        public async Task EditAsync(string title, string content, int id)
        {
            var questuion = this.repository.All().Where(n => n.Id == id).FirstOrDefault();
            questuion.Title = title;
            questuion.Description = content;
            this.repository.Update(questuion);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Question> query =
               this.repository.All().OrderBy(x => x.Title);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var questuion = this.repository.All().Where(n => n.Id == id).To<T>().FirstOrDefault();

            return questuion;
        }
    }
}
