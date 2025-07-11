namespace Savvy_Skills_Assessment;

public class Game
{
    private  Code _code { get; }
    private readonly int _maxAttempts;
    private List<(string Guess, Feedback Result)> _history;
    
    public bool IsOver => _history.Count >= _maxAttempts || HasWon;
    public bool HasWon { get; private set; }
    public int RemainingAttempts => _maxAttempts - _history.Count;
    public string SecretCode => _code.Secret;

    public IReadOnlyList<(string Guess, Feedback Result)> History => _history;

    public Game(string? code = null, int maxAttempts = 10)
    {
        _code = new Code(code);
        _maxAttempts = maxAttempts;
        _history = new List<(string, Feedback)>();
        HasWon = false;    
    }

    public (bool Success, string ErorrMsg) SubmitGuess(string guess)
    {
        var playerGuess = new PlayerGuess(guess);
        if (!playerGuess.IsValid)
        {
            return (false, playerGuess.ErrorMessage);
        }

        var feedback = new Feedback(playerGuess.Value, _code.Secret);
        _history.Add((playerGuess.Value, feedback));

        if (feedback.WellPlaced == 4)
        {
            HasWon = true;
        }
        return (true,"");
    }
}