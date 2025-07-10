// See https://aka.ms/new-console-template for more information

using Savvy_Skills_Assessment;

class Program
{
    static void Main(string[] args)
    {
        int maxAttempts = 10;
        string? codeInput = null;
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-c" :
                    if (i+1 < args.Length)
                        codeInput = args[++i];
                    break;
                case "-t":
                    if (i+1 < args.Length && int.TryParse(args[++i], out int userAttempts))
                    {
                        maxAttempts = userAttempts;
                        i++;
                    }
                    break;
                    
                    
            }
        }

        Code code = new Code();
        Game game = new Game(code.Secret, maxAttempts);
        game.Start();
    }
}

