using System;

namespace GuessTheNumber
{
    public class Game
    {
        public int userGuesses = 0;
        public int generatedNumber = 0;
        public int howManyNumber = 0;
        public int thresholdHint = 0;
        public bool tryAgain = true;
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.StartTheGame();
        }

        void StartTheGame()
        {
            do
            {
                Console.WriteLine("\n==== Welcome to Number Guesser ====\n");
                InputHowManyNumber();
                askHowHintThreshold();

                Console.WriteLine($"\n[i] Now guess the number between 1 and {howManyNumber}");
                LetTheUserGuess();
            }
            while (tryAgain == true);

            Console.WriteLine("\n[i] Bye-bye!\n");
        }

        int GenerateTheNumber(int totalNumber)
        {
            Random rand = new Random();
            return rand.Next(1, totalNumber);
        }

        void InputHowManyNumber()
        {
            while (true)
            {
                Console.WriteLine("[=] How many number you want to guess?");
                if (int.TryParse(Console.ReadLine(), out howManyNumber))
                {
                    generatedNumber = GenerateTheNumber(howManyNumber);
                    // Console.WriteLine(generatedNumber); // debug
                    break;
                }
                else
                {
                    Console.WriteLine("[!] Womp Womp, unrecognized input :()");
                }
            }
        }

        void askHowHintThreshold()
        {
            while (true)
            {
                Console.WriteLine("\n[=] How many threshold you want?");
                if (int.TryParse(Console.ReadLine(), out thresholdHint))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("[!] Womp Womp, unrecognized input :()");
                }
            }
        }
        void LetTheUserGuess()
        {
            bool winning = false;
            while (winning == false)
            {
                Console.WriteLine("\n[=] Can you guess the number?");
                if (int.TryParse(Console.ReadLine(), out userGuesses))
                {
                    winning = GuessingNow(userGuesses, generatedNumber);
                }
                else
                {
                    Console.WriteLine("[!] Womp Womp, unrecognized input :()");
                }
            }
        }

        bool GuessingNow(int guessedNumber, int numberGenerated)
        {
            if (guessedNumber == numberGenerated)
            {
                Console.WriteLine("\n[i] Congrats you guessess correctly, you win!!\n");
                Console.WriteLine("[=] Do you want to replay the game? (y/n)");
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
            }

            else if (guessedNumber < numberGenerated)
            {
                Console.WriteLine("[i] Sorry you are wayy lower than you thought, try higher number");
            }
            else if (guessedNumber > numberGenerated)
            {
                Console.WriteLine("[i] Sorry you are wayy above than you thought, try lower number");
            }
            else
            {
                Console.WriteLine("[!] Womp Womp, unrecognized input :()");
            }
            // Console.WriteLine($"{guessedNumber} | {numberGenerated}"); // debug
            return false;
        }
    }
}