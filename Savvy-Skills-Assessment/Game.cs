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
        int attempts = 0;
        Console.WriteLine("Can you break the code?");
        Console.WriteLine("Enter your guess:");

        while (attempts < _maxAttempts)
        {
            Console.WriteLine($"\nAttempt: {attempts} / {_maxAttempts}: ");
            string? guess = Console.ReadLine();

            if (guess == null)
            {
                Console.WriteLine("\nEnd of input. Exiting...");
                return;
            }

            var playerGuess = new PlayerGuess(guess);
            if (!playerGuess.IsValid)
            {
                continue;
            }

            var feadback = new Feedback(playerGuess.Value, _secretCode);
            

            if (feadback.WellPlaced == 4)
            {
                Console.WriteLine("Congratz! You did it!");
                return;
            }
            Console.WriteLine($"Well placed: {feadback.WellPlaced}. Miss placed: {feadback.Misplaced}");
            attempts++;
        }
        Console.WriteLine("\nGame Over. Better luck next time!");
        Console.WriteLine($"The secret code was: {_secretCode}");
    }
    

   
}