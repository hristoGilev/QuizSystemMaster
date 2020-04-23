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
        private readonly IDeletableEntityRepository<Question> repositoryQuestions;
        private readonly IDeletableEntityRepository<QuestionMultiSelect> repositoryQuestionMultiSelect;
        private readonly IDeletableEntityRepository<ExamUser> repositoryExamUser;

        public ExamsService(
            IDeletableEntityRepository<Exam> repositoryExams,
            IDeletableEntityRepository<Question> repositoryQuestion,
            IDeletableEntityRepository<QuestionMultiSelect> repositoryQuestionMultiSelect,
            IDeletableEntityRepository<ExamUser> repositoryExamUser)
        {
            this.repositoryExams = repositoryExams;
            this.repositoryQuestions = repositoryQuestion;
            this.repositoryQuestionMultiSelect = repositoryQuestionMultiSelect;
            this.repositoryExamUser = repositoryExamUser;
        }

        public async Task<int> CreateAsync(string name, string descrption)
        {
            var exam = new Exam() { Name = name, Description = descrption };
            var question1 = this.repositoryQuestions.All().Where(t => t.ExamId == null).ToList();
            var t1 = question1.Count;
            var question2 = this.repositoryQuestionMultiSelect.All().Where(t => t.ExamId == null).ToList();
            var t2 = question2.Count;

            await this.repositoryExams.AddAsync(exam);
            await this.repositoryExams.SaveChangesAsync();
            for (int i = 0; i < 2; i++)
            {
                var q = question1.ElementAt(this.RandomNuber(t1));

                q.ExamId = exam.Id.ToString();
                exam.Questions.Add(q);
            }

            for (int i = 0; i < 3; i++)
            {
                var q = question2.ElementAt(this.RandomNuber(t2));

                q.ExamId = exam.Id.ToString();
                exam.QuestionMultiSelects.Add(q);
            }

            this.repositoryExams.Update(exam);
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

        public bool CheckForQuestions()
        {
            var q2 = this.repositoryQuestionMultiSelect.All()
                .Where(t => t.ExamId == null).ToList().Count;
            var q1 = this.repositoryQuestions.All()
                .Where(t => t.ExamId == null).ToList().Count;
            if (q1 < 4 || q2 < 6)
            {
                return false;
            }

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var exam = this.repositoryExams.All().Where(n => n.Id == id).FirstOrDefault();
            var users = this.repositoryExamUser.All().Where(m => m.ExamId == id.ToString()).ToList();
            foreach (var item in users)
            {
                this.repositoryExamUser.Delete(item);
                await this.repositoryExamUser.SaveChangesAsync();
            }

            var questiuns = this.repositoryQuestions.All().Where(m => m.ExamId == id.ToString()).ToList();
            foreach (var item in questiuns)
            {
                item.ExamId = null;
                this.repositoryQuestions.Update(item);
                await this.repositoryQuestions.SaveChangesAsync();
            }

            var questinsMulti = this.repositoryQuestionMultiSelect.All()
                                     .Where(m => m.ExamId == id.ToString()).ToList();
            foreach (var item in questinsMulti)
            {
                item.ExamId = null;
                this.repositoryQuestionMultiSelect.Update(item);
                await this.repositoryQuestionMultiSelect.SaveChangesAsync();
            }

            this.repositoryExams.Delete(exam);
            await this.repositoryExams.SaveChangesAsync();
        }

        public async Task OpenorNotOpen(int id)
        {
            var exam = this.repositoryExams.All().Where(n => n.Id == id).FirstOrDefault();
            if (exam.IsOpen)
            {
                exam.IsOpen = false;
            }
            else
            {
                exam.IsOpen = true;
            }

            this.repositoryExams.Update(exam);
            await this.repositoryExams.SaveChangesAsync();
        }

        private int RandomNuber(int t)
        {
            Random randam = new Random();
            return randam.Next(0, t - 1);
        }
    }
}
