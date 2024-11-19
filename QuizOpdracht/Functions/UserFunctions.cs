using QuizOpdracht.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizOpdracht
{
    internal class UserFunctions
    {
        MenuHelper menuHelper = new MenuHelper();

        public void checkUserChoice(string choice)
        {
            if (choice.Length > 1 || choice.Length < 1)
            {
                Console.WriteLine("Wrong input detected!");
                Console.WriteLine("Try again!");
                string c = Console.ReadLine();
                checkUserChoice(c);
            }
            else if (choice.All(char.IsDigit))
            {
                switch (choice)
                {
                    case "1":
                        menuHelper.printQuizChoices();
                        break;
                    case "2":
                        menuHelper.printMainMenu();
                        break;
                    default:
                        Console.WriteLine("Wrong input detected!");
                        Console.WriteLine("Try again!");
                        string c = Console.ReadLine();
                        checkUserChoice(c);
                        break;
                }
            }
        }

        public void playQuiz(int choice)
        {
            QuizDB quizDB = new QuizDB();
            QuestionDB questionDB = new QuestionDB();
            AnswerDB answerDB = new AnswerDB();

            Quiz quiz = quizDB.getQuizByID(choice);

            Console.WriteLine($"Starting Quiz: {quiz.quizName}");
            Console.WriteLine($"Number of Questions: {quiz.amtOfQuestions}");
            Console.WriteLine($"Creator: {quiz.creator}");
            Console.WriteLine();


            List<Question> questionList = shuffleQuestions(questionDB.getQuestionsByQuiz(quiz.quizID));
            List<Tuple<int, Answer>> answerList = answerDB.getAnswerByQuestion(questionList);

            int score = 0;


            foreach (Question question in questionList)
            {
                Console.WriteLine($"Question: {question.question}");

                // Get answers for this question and filter by questionID
                List<Answer> answersForQuestion = answerList
                    .Where(a => a.Item1 == question.id)
                    .Select(a => a.Item2)
                    .ToList();


                for (int i = 0; i < answersForQuestion.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {answersForQuestion[i].answer}");
                }

                int userChoice = GetUserChoice(answersForQuestion.Count);


                if (answersForQuestion[userChoice - 1].isTrue)
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }
            }

            Console.WriteLine($"Quiz Completed! Your score: {score}/{quiz.amtOfQuestions}");
        }

        private int GetUserChoice(int numOptions)
        {
            int choice;
            while (true)
            {
                Console.Write("Enter your choice (1 - " + numOptions + "): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= numOptions)
                {
                    return choice;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        public List<Question> shuffleQuestions(List<Question> questions)
        {
            Random random = new Random();
            for (int i = 0; i < questions.Count; i++)
            {
                int randomIndex = random.Next(i, questions.Count);

                Question temp = questions[i];
                questions[i] = questions[randomIndex];
                questions[randomIndex] = temp;
            }

            return questions;
        }
    }
}
