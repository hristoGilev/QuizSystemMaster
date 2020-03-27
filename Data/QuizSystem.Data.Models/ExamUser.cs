namespace QuizSystem.Data.Models
{
    public class ExamUser
    {
        private string id;

        public ExamUser()
        {
            this.id = this.UserId + this.ExamId;
        }

        public string Id { get => this.id; set => this.id = value; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual string ExamId { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
