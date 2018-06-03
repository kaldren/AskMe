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

        public IEnumerable<Question> GetQuestionsByUser(string username)
        {
            return _context.Question
                .Where(p => p.User.NickName == username)
                .ToList();
        }

        public Question GetQuestionDetails(int? id)
        {
            return _context.Question
                .SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Answer> GetAllAnswersById(int? id)
        {
            return _context.Answers
                .Where(p => p.QuestionId == id)
                .ToList();
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
