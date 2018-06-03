using AskMe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;

        public UserViewComponent(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

    }
}
