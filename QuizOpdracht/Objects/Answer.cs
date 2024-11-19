using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizOpdracht
{
    internal class Answer
    {
        public Answer(){}

        public int answerID { get; set; }
        public int questionID { get; set; }

        public string answer { get; set; }

        public bool isTrue { get; set; }
    }
}
