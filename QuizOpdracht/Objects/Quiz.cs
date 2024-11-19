using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizOpdracht
{
    internal class Quiz
    {
        public int quizID { get; set; }
        public string quizName { get; set; }
        public int amtOfQuestions { get; set; }
        public string creator { get; set; }

        public Quiz()
        {
        }

        public string toString()
        {
            return $"{quizName} created by {creator} with {amtOfQuestions} questions";
        }
    }
}
