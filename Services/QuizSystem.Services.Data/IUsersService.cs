namespace QuizSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizSystem.Data.Models;

    public interface IUsersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
