using QuizOpdracht.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizOpdracht
{
    internal class AdminFunctions
    {
        MenuHelper menuHelper = new MenuHelper();
        public AdminFunctions() { }

        public void checkAdminChoice(string choice)
        {
            if (choice.Length != 1 || !choice.All(char.IsDigit))
            {
                Console.WriteLine("Invalid input detected. Please try again.");
                string newChoice = Console.ReadLine();
                checkAdminChoice(newChoice);
                return;
            }

            switch (choice)
            {
                case "1":
                    manageQuizzes();
                    break;
                case "2":
                    createQuiz();
                    break;
                case "3":
                    menuHelper.printMainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input detected. Please try again.");
                    string newChoice = Console.ReadLine();
                    checkAdminChoice(newChoice);
                    break;
            }
        }

        public void manageQuizzes()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Select a quiz to delete:");

            QuizDB quizDB = new QuizDB();
            List<Quiz> quizzes = quizDB.getAllQuizzes();

            if (quizzes.Count == 0)
            {
                Console.WriteLine("No quizzes available to manage.");
                Thread.Sleep(2000);
                return;
            }

            int num = 1;
            foreach (Quiz quiz in quizzes)
            {
                Console.WriteLine($"{num}) {quiz.toString()}");
                num++;
            }

            Console.Write("Enter the number of the quiz to delete: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= quizzes.Count)
            {
                Quiz selectedQuiz = quizzes[choice - 1];
                quizDB.deleteQuiz(selectedQuiz);
                Console.WriteLine($"Quiz '{selectedQuiz.quizName}' deleted successfully.");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Invalid selection. Returning to menu.");
                Thread.Sleep(2000);
            }
        }

        public void createQuiz()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("You selected to create a quiz.");
            Console.Write("Enter the quiz name: ");
            string quizName = Console.ReadLine();

            Console.Write("How many questions do you wish to create? ");
            string amtOfQuestionsInput = Console.ReadLine();

            if (!int.TryParse(amtOfQuestionsInput, out int amtOfQuestions) || amtOfQuestions <= 0)
            {
                Console.WriteLine("Invalid number of questions. Please try again.");
                Thread.Sleep(2000);
                createQuiz();
                return;
            }

            Console.Write("Enter the creator's name: ");
            string creator = Console.ReadLine();

            QuizDB db = new QuizDB();
            int quizId = db.createQuiz(quizName, creator, amtOfQuestionsInput);

            createQuestions(amtOfQuestions, quizId);
        }

        public void createQuestions(int amtOfQuestions, int quizId)
        {
            QuestionDB db = new QuestionDB();
            List<Tuple<int, string>> questions = new List<Tuple<int, string>>();

            for (int i = 1; i <= amtOfQuestions; i++)
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------------------------------------");
                Console.Write($"Enter the text for question {i}: ");
                string questionText = Console.ReadLine();

                Console.Write("How many answers should it have? ");
                string amtOfAnswersInput = Console.ReadLine();

                if (int.TryParse(amtOfAnswersInput, out int amtOfAnswers) && amtOfAnswers > 0)
                {
                    Tuple<int, string> result = db.createQuestion(quizId, questionText, amtOfAnswersInput);
                    questions.Add(result);
                }
                else
                {
                    Console.WriteLine("Invalid number of answers. Please try again.");
                    i--; 
                }
            }

            createAnswers(questions);
        }

        public void createAnswers(List<Tuple<int, string>> questions)
        {
            AnswerDB db = new AnswerDB();
            QuestionDB questionDB = new QuestionDB();

            foreach (Tuple<int, string> questionTuple in questions)
            {
                int questionId = questionTuple.Item1;
                int amtOfAnswers = int.Parse(questionTuple.Item2);

                bool validAnswers = false;

                while (!validAnswers)
                {
                    // Create answers for questions
                    List<Tuple<string, string>> answers = new List<Tuple<string, string>>();
                    Question question = questionDB.GetQuestionByID(questionId);

                    Console.Clear();
                    Console.WriteLine("--------------------------------------------------------------------");
                    Console.WriteLine($"Creating answers for question: {question.question}");

                    for (int i = 1; i <= amtOfAnswers; i++)
                    {
                        Console.Write($"Enter text for answer {i}: ");
                        string answerText = Console.ReadLine();

                        Console.Write("Is this answer correct? (true/false): ");
                        string isTrueInput = Console.ReadLine().ToLower();

                        if (isTrueInput == "true" || isTrueInput == "false")
                        {
                            answers.Add(new Tuple<string, string>(answerText, isTrueInput));
                        }
                        else
                        {
                            // Retry if its not true or false
                            Console.WriteLine("Invalid input for correctness. Please try again.");
                            i--;
                        }
                    }

                    // Make sure 1 answer is true
                    if (answers.Any(a => a.Item2 == "true"))
                    {
                        validAnswers = true; 
                        db.createAnswers(answers, questionId); 
                        Console.WriteLine($"Answers saved for question: {question.question}.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("No correct answer provided for this question. Please try again.");
                        Thread.Sleep(2000);
                    }
                }
            }

            Console.WriteLine("All questions and answers have been successfully created!");
            Console.WriteLine("Sending you back to the main menu..");
            Thread.Sleep(2000);
            menuHelper.printMainMenu();

        }

    }
}
