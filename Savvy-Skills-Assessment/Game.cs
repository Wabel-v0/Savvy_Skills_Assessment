namespace Savvy_Skills_Assessment;

public class Game
{
    private readonly string _secretCode;
    private readonly int _maxAttempts;

    public Game(string secretCode, int maxAttempts)
    {
        _secretCode = secretCode;
        _maxAttempts = maxAttempts;
    }

    public void Start()
    {
        Console.WriteLine("Can you break the code?");
        Console.WriteLine("Enter your guess:");

        for (int attempt = 1; attempt < _maxAttempts; attempt++)
        {
            Console.WriteLine($"\nAttempt: {attempt} / {_maxAttempts}: ");
            string? guess = Console.ReadLine();

            if (guess == null)
            {
                Console.WriteLine("\nEnd of input. Exiting...");
                return;
            }

            if (!isValildGuess(guess))
            {
                continue;
            }
            int wellPlaced = CountWellPlaced(guess , _secretCode);
            int missPlaced = CountMisplaced(guess , _secretCode);

            if (wellPlaced == 4)
            {
                Console.WriteLine("Congratz! You did it!");
                return;
            }
            Console.WriteLine($"Well placed: {wellPlaced}. Miss placed: {missPlaced}");
        }
        Console.WriteLine("\nGame Over. Better luck next time!");
        Console.WriteLine($"The secret code was: {_secretCode}");
    }

    private bool isValildGuess(string guess)
    {
        if (guess.Length < 4 || guess.Length > 4)
        {
            Console.WriteLine("\nEnter 4 digit numbers");
            return false;
        }
        foreach (char c in guess)
        {
            if (c < '0' || c > '8')
            {
                Console.WriteLine("\nEnter number between 0 and 8");
                return false;
            }
            
        }
        return guess.Length == 4;
        
    }

    private int CountWellPlaced(string guess, string code)
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (guess[i] == code[i])
                count++;
            
        }
        return count;
    }

    private int CountMisplaced(string guess, string code)
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (guess[i] != code[i] && code.Contains(guess[i]))
                count++;


        }

        return count;
    }
}