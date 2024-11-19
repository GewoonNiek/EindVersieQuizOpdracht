using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using QuizOpdracht.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizOpdracht
{
    internal class QuestionDB
    {

        public QuestionDB() { }

        // Put a question in DB
        public Tuple<int, string> createQuestion(int quizid, string question, string amt_of_answers)
        {
            DatabaseConnect db = DatabaseConnect.GetInstance();
            string query = $"INSERT INTO `question` (`questionid`, `quizid`, `amt_of_answers`, `question`) VALUES (NULL, '{quizid}', '{amt_of_answers}', '{question}');";
            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
            cmd.ExecuteNonQuery();

            int questionID = (int)cmd.LastInsertedId;

            string query2 = $"Select `amt_of_answers` from question where `questionid` = {questionID};";
            MySqlCommand cmd2 = new MySqlCommand(query2, db.GetConnection());
            string answer = cmd2.ExecuteScalar().ToString();

            // Return a tuple with questionID and amount of answers
            Tuple<int, string> result = new Tuple<int, string>(questionID, answer);


            return result;
        }

        // Get a question by questionID out of the DB
        public Question GetQuestionByID(int qid)
        {
            DatabaseConnect db = DatabaseConnect.GetInstance();
            string query = $"SELECT * from question where questionid = {qid}";
            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();
            Question question = new Question();

            while (reader.Read())
            {
                
                question.id = (int)reader["questionid"];
                question.quizid = (int)reader["quizid"];
                question.amt_of_answers = (int)reader["amt_of_answers"];
                question.question = (string)reader["question"];
            }
            reader.Close();

            return question;
        }

        // Get a list of questions by QuizID
        public List<Question> getQuestionsByQuiz(int quizID)
        {
            List<Question> questions = new List<Question>();
            DatabaseConnect db = DatabaseConnect.GetInstance();
            string query = $"SELECT * from question where quizid = {quizID}";
            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Question question = new Question();
                question.id = (int)reader["questionid"];
                question.quizid = (int)reader["quizid"];
                question.amt_of_answers = (int)reader["amt_of_answers"];
                question.question = (string)reader["question"];
                questions.Add(question);
            }

            reader.Close();
            return questions;
        }
    }
}
