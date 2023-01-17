using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using static _2._3.Models.QuestionModel;

namespace _2._3.Models
{
    public class ResultModel
    {
        public List<QuestionModel> Questions { get; set; }

        public int Count => Questions.Count;

        public int CorrectAnswerCount
        {
            get
            {
                return Questions.Count(q => q.CorrectAnswerCheck);
            }
        }

    }
}
