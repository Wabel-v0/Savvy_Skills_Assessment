namespace Savvy_Skills_Assessment.Mode;

public class ConsoleGameRunner : ModeRunner
{
    private readonly Game _game;

    public ConsoleGameRunner(string? codeInput = null, int maxAttempts = 10)
    {
        _game = new Game(codeInput, maxAttempts);
    }
    public void Run()
    {
        Console.WriteLine("Can you break the code?");
        Console.WriteLine("Enter your guess:");
        while (!_game.IsOver)
        {
            Console.WriteLine($"Remaining Attempts :{_game.RemainingAttempts}");
            string? guess = Console.ReadLine();
            if (guess == null)
            {
                Console.WriteLine("\nEnd of input. Exiting...");
                return;
            }
            var (success, errorMsg) = _game.SubmitGuess(guess);

            if (!success)
            {
                Console.WriteLine(errorMsg);
                continue;
            }

            if (_game.History.Count > 0)
            {
            var (g, f) = _game.History.Last();
            Console.WriteLine($" Well placed: {f.WellPlaced},  Misplaced: {f.Misplaced}");
                
            }

        }
        Console.WriteLine(_game.HasWon
            ? " Congratz! You did it!"
            : " Game over. Better luck next time.");
        Console.WriteLine($"The secret code was: {_game.SecretCode}");
    }
}