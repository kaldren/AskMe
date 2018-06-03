using AskMe.Data;
using AskMe.Models;
using AskMe.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Question> GetQuestionsByUser(string username)
        {
            var questions = _context.Question
                .Where(p => p.User.NickName == username)
                .ToList();

            return questions;
        }
    }
}
