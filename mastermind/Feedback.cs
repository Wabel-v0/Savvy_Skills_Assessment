namespace Savvy_Skills_Assessment;

public class Feedback
{
    private string _guess;   
    private string _code; 
    
    public int WellPlaced { get; }    
    public int Misplaced { get; }

    public Feedback(string guess, string code)
    {
        _guess = guess;
        _code = code;

        bool[] codeMatched = new bool[4];
        bool[] guessMatched = new bool[4];

        WellPlaced = CountWellPlaced(codeMatched, guessMatched);
        Misplaced = CountMisplaced(codeMatched, guessMatched);
    }

    private int CountWellPlaced(bool[] codeMatched, bool[] guessMatched)
    {
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            if (_guess[i] == _code[i])
            {
                count++;
                codeMatched[i] = true;
                guessMatched[i] = true;
            }
        }

        return count;
    }

    private int CountMisplaced(bool[] codeMatched, bool[] guessMatched)
    {
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            if (guessMatched[i]) continue;

            for (int j = 0; j < 4; j++)
            {
                if (!codeMatched[j] && _guess[i] == _code[j])
                {
                    count++;
                    codeMatched[j] = true;
                    break;
                }
            }
        }

        return count;
    }
}