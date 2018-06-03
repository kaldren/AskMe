using System.Collections.Generic;

namespace AskMe.Models.QuestionViewModels
{
    public class AllQuestionsViewModel
    {
        public IEnumerable<Question> AllQuestions { get; set; }
        public ApplicationUser User { get; set; }
    }
}
