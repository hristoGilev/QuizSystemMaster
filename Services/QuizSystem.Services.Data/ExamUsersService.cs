namespace QuizSystem.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;

    public class ExamUsersService : IExamUsersService
    {
        private readonly IDeletableEntityRepository<Exam> repositoryExams;
        private readonly IDeletableEntityRepository<ApplicationUser> repositoryUsers;
        private readonly IDeletableEntityRepository<ExamUser> repository1;

        public ExamUsersService(
            IDeletableEntityRepository<Exam> repositoryExams,
            IDeletableEntityRepository<ApplicationUser> repositoryUsers,
            IDeletableEntityRepository<ExamUser> repository1)
        {
            this.repositoryExams = repositoryExams;
            this.repositoryUsers = repositoryUsers;
            this.repository1 = repository1;
        }

        public async Task AddUserToExamAsync(int idExam, string[] usersName)
        {
            foreach (var item in usersName)
            {
                var user = this.repositoryUsers.All().FirstOrDefault(t => t.UserName == item);
                var exam = this.repositoryExams.All().FirstOrDefault(t => t.Id == idExam);
                var examUser = this.repository1.All().FirstOrDefault(t => (t.UserId == user.Id && t.ExamId == idExam.ToString()));
                if (examUser != null)
                {
                    continue;
                }

                examUser = new ExamUser() { UserId = user.Id, ExamId = exam.Id.ToString() };
                await this.repository1.AddAsync(examUser);
                await this.repository1.SaveChangesAsync();
                exam.ExamUser.Add(examUser);
                this.repositoryExams.Update(exam);
                await this.repositoryExams.SaveChangesAsync();
                user.ExamUser.Add(examUser);
                this.repositoryUsers.Update(user);
                await this.repositoryUsers.SaveChangesAsync();
            }
        }
    }
}
