using System;

namespace GuessTheNumber
{
    internal class Game
    {
        private int userGuesses = 0;
        private int generatedNumber = 0;
        private int howManyNumber = 0;
        private int thresholdHint = 0;
        private int howManyRetries = 0;
        private bool tryAgain = true;
        
        private static void Main(string[] args)
        {
            Game game = new Game();
            game.StartTheGame();
        }

        private void StartTheGame()
        {
            do
            {
                Console.WriteLine("\n==== Welcome to Number Guesser ====");

                howManyNumber = TryParsing("\n[=] How many number you want to guess?");
                generatedNumber = GenerateTheNumber(howManyNumber);
                thresholdHint = TryParsing("\n[=] How many threshold you want?");
                howManyRetries = TryParsing("\n[=] How many retries do you want?");

                Console.WriteLine($"\n[i] Now guess the number between 1 and {howManyNumber}");
                LetTheUserGuess();
            }
            while (tryAgain == true);

            Console.WriteLine("\n[i] Bye-bye!\n");
        }

        private int GenerateTheNumber(int totalNumber)
        {
            Random rand = new Random();
            return rand.Next(1, totalNumber);
        }

        private void LetTheUserGuess()
        {
            bool done = false;

            while (done == false)
            {
                Console.WriteLine(howManyRetries);
                if (howManyRetries != 0)
                {
                    userGuesses = TryParsing("\n[=] Can you guess the number?");
                    done = GuessingNow(userGuesses, generatedNumber);
                }
                else
                {
                    Console.WriteLine("\n[i] Sorry You Lose!!\n");
                    done = true;
                }
            } 
        }

        private bool GuessingNow(int guessedNumber, int numberGenerated)
        {
            if (guessedNumber == numberGenerated)
            {
                Console.WriteLine("\n[i] Congrats you guessess correctly, you win!!\n");
                Console.WriteLine("[=] Do you want to replay the game? (Insert 'y' to replay)");
                if (Console.ReadLine() == "y")
                {
                    tryAgain = true;
                    return true;
                }
                tryAgain = false;
                return true;
            }
            else if (Math.Abs(guessedNumber - numberGenerated) < thresholdHint)
            {
                Console.WriteLine("[i] Sorry but woah, you are closer than you thought!");
                howManyRetries -= 1;
            }
            else if (guessedNumber < numberGenerated)
            {
                Console.WriteLine("[i] Sorry you are wayy lower than you thought, try higher number");
                howManyRetries -= 1;
            }
            else if (guessedNumber > numberGenerated)
            {
                Console.WriteLine("[i] Sorry you are wayy above than you thought, try lower number");
                howManyRetries -= 1;
            }
            else
            {
                Console.WriteLine("[!] Womp Womp, unrecognized input :()");
            }
            // Console.WriteLine($"{guessedNumber} | {numberGenerated}"); // debug
            return false;
        }

        private int TryParsing(string msg)
        {
            while (true)
            {
                Console.WriteLine(msg);
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("[!] Womp Womp, unrecognized input :()");
                }
            }
        }
    }
}