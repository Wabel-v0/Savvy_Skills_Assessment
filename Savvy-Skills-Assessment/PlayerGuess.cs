namespace Savvy_Skills_Assessment;

public class PlayerGuess
{
    private string _input;
    
    public bool IsValid { get; private set; }

    public string Value { get; private set; } = string.Empty;
    public string ErrorMessage { get; private set; } = "";

    public PlayerGuess(string input)
    {
        _input = input;
        Validate();
        
    }

    private string Validate()
    {
        IsValid = true;
        if (_input.Length !=4)
        {
            IsValid = false;
            return ErrorMessage = "Enter 4 digit numbers";
        }
        foreach (char c in _input)
        {
            if (c < '0' || c > '8')
            {
                IsValid = false;
                return ErrorMessage= "Enter number between 0 and 8";
            }
            
        }
        ErrorMessage = "";
        Value = _input;
        return Value;
    }
}