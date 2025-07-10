namespace Savvy_Skills_Assessment;

public class PlayerGuess
{
    private string _input;
    
    public bool IsValid { get; private set; }

    public string Value { get; private set; } = string.Empty;

    public PlayerGuess(string input)
    {
        _input = input;
        Validate();
        
    }

    private void Validate()
    {
        IsValid = true;
        if (_input.Length !=4)
        {
            Console.WriteLine("\nEnter 4 digit numbers");
            IsValid = false;
        }
        foreach (char c in _input)
        {
            if (c < '0' || c > '8')
            {
                Console.WriteLine("\nEnter number between 0 and 8");
                IsValid = false;
            }
            
        }
        Value = _input;
    }
}