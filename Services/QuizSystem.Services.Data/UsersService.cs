namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;
    using QuizSystem.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> repositoryUsers;

        public UsersService(IDeletableEntityRepository<ApplicationUser> repositoryUsers)
        {
            this.repositoryUsers = repositoryUsers;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query =
               this.repositoryUsers.All().OrderBy(x => x.UserName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
