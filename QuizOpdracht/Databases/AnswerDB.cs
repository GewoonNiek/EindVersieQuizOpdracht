using MySql.Data.MySqlClient;
using QuizOpdracht.Databases;
using QuizOpdracht.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizOpdracht
{
    internal class AnswerDB
    {
        public AnswerDB(){}

        // Put answers in DB
        public void createAnswers(List<Tuple<string, string>> answ, int qid)
        {
            MenuHelper mh = new MenuHelper();
            DatabaseConnect db = DatabaseConnect.GetInstance();
            foreach (Tuple<string, string> item in answ) {
                string query;
                if(item.Item2 == "true")
                {
                    query = $"INSERT INTO `answer` (`answerid`, `questionid`, `answer`, `isTrue`) VALUES (NULL, '{qid}', '{item.Item1}', '1');";
                } else
                {
                    query = $"INSERT INTO `answer` (`answerid`, `questionid`, `answer`, `isTrue`) VALUES (NULL, '{qid}', '{item.Item1}', '0');";
                }
                MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
                cmd.ExecuteNonQuery();
            }
        }

        // Get a list of answers sorted by questions
        public List<Tuple<int, Answer>> getAnswerByQuestion(List<Question> questions)
        {
            DatabaseConnect db = DatabaseConnect.GetInstance();
            List<Tuple<int, Answer>> resultList = new List<Tuple<int, Answer>>();

            foreach (Question question in questions) {
                string query = $"select * from answer where questionid = {question.id}";
                MySqlCommand cmd = new MySqlCommand( query, db.GetConnection());
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Answer answer = new Answer();
                    answer.answerID = (int)reader["answerid"];
                    answer.questionID = (int)reader["questionid"];
                    answer.answer = (string)reader["answer"];
                    answer.isTrue = (bool)reader["isTrue"];

                    Tuple<int, Answer> result = new Tuple<int, Answer>(question.id, answer);
                    resultList.Add(result);
                }

                reader.Close();
            }
            return resultList;

        }
    }
}
