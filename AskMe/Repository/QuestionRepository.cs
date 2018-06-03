using AskMe.Data;
using AskMe.Models;
using AskMe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Question>> GetQuestionsByUser(string username)
        {
            return await _context.Question
                .Where(p => p.User.NickName == username)
                .ToListAsync();
        }

        public async Task<Question> GetQuestionDetails(int? id)
        {
            return await _context.Question
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Answer>> GetAllAnswersById(int? id)
        {
            return await _context.Answers
                .Where(p => p.QuestionId == id)
                .ToListAsync();
        }

        public IEnumerable<ApplicationUser> GetAllAnswerAuthors(IEnumerable<Answer> answers)
        {
            // Get all ids of users who wrote an answer
            var answerUserId = new HashSet<string>();

            foreach (var item in answers)
            {
                answerUserId.Add(item.UserId);
            }

            var users = new List<ApplicationUser>();

            foreach (var item in answerUserId)
            {
                users.Add(_context.Users.SingleOrDefault(p => p.Id == item));
            }

            return users;
        }
    }
}
