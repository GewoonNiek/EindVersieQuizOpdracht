using QuizOpdracht.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizOpdracht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuHelper mh = new MenuHelper();

            Console.WriteLine("Welcome to the quizprogram.");
            Console.WriteLine("Are you a player or an admin:");
            string choice = Console.ReadLine();
            mh.checkTypeOfUser(choice);
        } 
    }
}
