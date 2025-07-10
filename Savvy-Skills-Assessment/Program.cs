// See https://aka.ms/new-console-template for more information

using Savvy_Skills_Assessment;

class Program
{
    static void Main(string[] args)
    {
        string code = "1234";
        int maxAttempts = 5;
        
        Game game = new Game(code, maxAttempts);
        game.Start();
    }
}

