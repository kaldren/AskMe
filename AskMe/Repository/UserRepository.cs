using AskMe.Data;
using AskMe.Models;
using AskMe.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUserDataById(string id)
        {
            return _context.Users
                .SingleOrDefault(p => p.Id == id);
        }
    }
}
