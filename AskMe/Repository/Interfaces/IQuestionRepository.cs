using AskMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Repository.Interfaces
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetQuestionsByUser(string username);
    }
}
