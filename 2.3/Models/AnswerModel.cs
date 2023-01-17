using _2._3.Tools;

namespace _2._3.Models
{
    public class AnswerModel
    {
        [Val(1, ErrorMessage = "Big number")]
        public int? Answer { get; set; }
    }
}
