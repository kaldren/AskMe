using AskMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Repository.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetQuestionsByUser(string username);
        Task<Question> GetQuestionDetails(int? id);
        Task<IEnumerable<Answer>> GetAllAnswersById(int? id);
        Task<IEnumerable<ApplicationUser>> GetAllAnswerAuthors(IEnumerable<Answer> answers);
    }
}
