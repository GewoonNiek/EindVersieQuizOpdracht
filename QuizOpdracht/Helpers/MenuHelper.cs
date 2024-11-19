using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizOpdracht.Helpers
{
    internal class MenuHelper
    {
        public MenuHelper() { }

        public void printAdminMenu()
        {
            AdminFunctions af = new AdminFunctions();
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("What do you want to do:");
            Console.WriteLine("1) Manage Quizzes");
            Console.WriteLine("2) Create Quizzes");
            Console.WriteLine("3) Logout");
            Console.WriteLine("--------------------------------------------------------------------");
            string choice = Console.ReadLine();
            af.checkAdminChoice(choice);

        }

        public void printUserMenu()
        {
            UserFunctions uf = new UserFunctions();
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("What do you want to do:");
            Console.WriteLine("1) Play Quizzes");
            Console.WriteLine("2) Logout");
            Console.WriteLine("--------------------------------------------------------------------");
            string choice = Console.ReadLine();
            uf.checkUserChoice(choice);
        }

        public void printMainMenu()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Welcome to the quizprogram.");
            Console.WriteLine("Are you a player or an admin:");
            string choice = Console.ReadLine();

            checkTypeOfUser(choice);
        }

        public void checkTypeOfUser(string user)
        {
            if (user == "admin")
            {
                printAdminMenu();
            }
            else if (user == "player")
            {
                printUserMenu();
            }
            else
            {
                Console.WriteLine("Not a valid option, try again!");
                string choice = Console.ReadLine();
                checkTypeOfUser(choice);
            }
        }

        public void printQuizChoices()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Which quiz do you want to play:");
            QuizDB quizDB = new QuizDB();
            List<Quiz> quizzes = quizDB.getAllQuizzes();

            int num = 0;
            foreach (Quiz quiz in quizzes)
            {
                num++;
                Console.WriteLine($"{num}) " + quiz.toString());
            }

            string choice = Console.ReadLine();
            UserFunctions uf = new UserFunctions();
            uf.playQuiz(quizzes[int.Parse(choice)-1].quizID);
        }
    }
}
