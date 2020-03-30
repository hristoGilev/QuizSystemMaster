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

    public class ExamsService : IExamsService
    {
        private readonly IDeletableEntityRepository<Exam> repositoryExams;
        private readonly IDeletableEntityRepository<Question> repositoryQuetion;

        public ExamsService(
            IDeletableEntityRepository<Exam> repositoryExams,
            IDeletableEntityRepository<Question> repositoryQuetion)
        {
            this.repositoryExams = repositoryExams;
            this.repositoryQuetion = repositoryQuetion;
        }

        public async Task<int> CreateAsync(string name, string descrption)
        {
            var exam = new Exam() { Name = name, Description = descrption };
            var t = this.repositoryQuetion.All().ToList().Count;

            await this.repositoryExams.AddAsync(exam);
            await this.repositoryExams.SaveChangesAsync();
            for (int i = 0; i < 2; i++)
            {
                var q = this.repositoryQuetion.All().ElementAt(this.RandomNuber(t));
                q.ExamId = exam.Id.ToString();
                exam.Questions.Add(q);
            }

            await this.repositoryExams.SaveChangesAsync();
            return exam.Id;
        }

        public T GetById<T>(int id)
        {
            var exam = this.repositoryExams.All().Where(n => n.Id == id).To<T>().FirstOrDefault();

            return exam;

        }


        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Exam> query =
               this.repositoryExams.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        private int RandomNuber(int t)
        {
            Random randam = new Random();
            return randam.Next(0, t - 1);
        }
    }
}
