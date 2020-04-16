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

    public class QuestionsMultiSelectService : IQuestionsMultiSelectService
    {
        private readonly IDeletableEntityRepository<QuestionMultiSelect> repository;

        public QuestionsMultiSelectService(IDeletableEntityRepository<QuestionMultiSelect> repository)
        {
            this.repository = repository;
        }

        public async Task<int> CreateAsync(
            string title,
            string description,
            string answersA,
            string answerB,
            string answerC,
            string userId)
        {
            var newQuestion = new QuestionMultiSelect()
            {
                Title = title,
                Description = description,
                AnswerTypeA = answersA,
                AnswerTypeB = answerB,
                AnswerTypeC = answerC,
                UserId = userId,
            };

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

        public async Task EditAsync(
            string title,
            string content,
            string answersA,
            string answerB,
            string answerC,
            int id)
        {
            var questuion = this.repository.All().Where(n => n.Id == id).FirstOrDefault();
            questuion.Title = title;
            questuion.Description = content;
            questuion.AnswerTypeA = answersA;
            questuion.AnswerTypeB = answerB;
            questuion.AnswerTypeC = answerC;
            this.repository.Update(questuion);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<QuestionMultiSelect> query =
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
