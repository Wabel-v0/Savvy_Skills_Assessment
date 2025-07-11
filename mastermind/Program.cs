// See https://aka.ms/new-console-template for more information

using Savvy_Skills_Assessment;
using Savvy_Skills_Assessment.Mode;

class Program
{
    static void Main(string[] args)
    {
        int maxAttempts = 10;
        string? codeInput = null;
        bool useRaylib = false;

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
                case "-g":
                case "--gui":
                    useRaylib = true;
                    break;
                    
                    
            }
        }

        ModeRunner runner;
        if (useRaylib)
        {
            runner = new RaylibGameRunner(codeInput, maxAttempts);
        }
        else
        {
            runner = new ConsoleGameRunner(codeInput, maxAttempts);
        }
        
        runner.Run();
    }
}

