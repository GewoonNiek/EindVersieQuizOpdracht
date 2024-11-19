using MySql.Data.MySqlClient;
using QuizOpdracht.Databases;
using QuizOpdracht.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizOpdracht
{
    internal class QuizDB
    {
        public QuizDB() { }

        // Get a list of all made Quizzes
        public List<Quiz> getAllQuizzes()
        {
            DatabaseConnect db = DatabaseConnect.GetInstance();
            string query = $"select * from quiz";
            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Quiz> quizzes = new List<Quiz>();

            while (reader.Read())
            {
                Quiz quiz = new Quiz();
                quiz.quizID = (int)reader["quizid"];
                quiz.quizName = (string)reader["quizname"];
                quiz.amtOfQuestions = (int)reader["amt_of_question"];
                quiz.creator = (string)reader["creator"];
                quizzes.Add(quiz);
            }
            reader.Close();
            return quizzes;
        }

        // Put a quiz in the database
        public int createQuiz(string quizname, string creator, string amt_of_question)
        {
            DatabaseConnect db = DatabaseConnect.GetInstance();
            string query = $"INSERT INTO `quiz` (`quizid`, `quizname`, `amt_of_question`, `creator`) VALUES (NULL, '{quizname}', '{amt_of_question}', '{creator}');";
            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
            cmd.ExecuteNonQuery();

            int quizID = (int)cmd.LastInsertedId;

            return quizID;
        }

        // Remove a quiz from the DB
        public void deleteQuiz(Quiz quiz)
        {
            MenuHelper menuHelper = new MenuHelper();
            DatabaseConnect db = DatabaseConnect.GetInstance();
            string query = $"DELETE from `quiz` where `quizid` = '{quiz.quizID}'";
            MySqlCommand cmd = new MySqlCommand( query, db.GetConnection());
            cmd.ExecuteNonQuery();
            Console.WriteLine("Deletion done...");
            Console.WriteLine("Sending you to the main menu..");
            Thread.Sleep(1000);
            menuHelper.printMainMenu();
        }

        // Get a quiz by quizid
        public Quiz getQuizByID(int quizid)
        {
            DatabaseConnect db = DatabaseConnect.GetInstance();
            string query = $"Select * from quiz where quizid = {quizid}";
            MySqlCommand cmd = new MySqlCommand( query , db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();

            Quiz quiz = new Quiz();
            while (reader.Read())
            {
                quiz.quizID = (int)reader["quizid"];
                quiz.quizName = (string)reader["quizname"];
                quiz.creator = (string)reader["creator"];
                quiz.amtOfQuestions = (int)reader["amt_of_question"];
            }
            reader.Close();

            return quiz;
        }
    }
}
