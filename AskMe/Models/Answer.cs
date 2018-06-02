namespace AskMe.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int Description { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int QuestionId { get; set; }
        public Question Question{ get; set; }
    }
}