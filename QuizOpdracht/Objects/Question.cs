using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizOpdracht
{
    internal class Question
    {
        public Question() { }

        public int id { get; set; }
        public int quizid { get; set; }
        public int amt_of_answers { get; set; }
        public string question { get; set; }
    }
}
