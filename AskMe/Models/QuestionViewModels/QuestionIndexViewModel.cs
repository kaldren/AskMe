﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Models.QuestionViewModels
{
    public class QuestionIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public IEnumerable<Answer> Answers { get; set; }
    }
}
