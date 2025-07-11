namespace Savvy_Skills_Assessment.Mode;

using Raylib_cs;
using System.Numerics;
public class RaylibGameRunner : ModeRunner
{
    private const int WindowWidth = 800;
    private const int WindowHeight = 600;
    private const int InputBoxWidth = 200;
    private const int InputBoxHeight = 40;
    private const int ButtonWidth = 100;
    private const int ButtonHeight = 35;

    private Game _game;
    private string _currentInput = "";
    private string _errorMessage = "";
    private bool _showingError;
    private float _errorTimer;
    private const float ErrorDisplayTime = 3f;

    private readonly Rectangle _inputBox;
    private readonly Rectangle _submitButton;
    private readonly Rectangle _playAgainButton;

    public RaylibGameRunner(string? code = null, int maxAttempts = 10)
    {
        _game = new Game(code, maxAttempts);

        _inputBox = new Rectangle(
            WindowWidth / 2 - InputBoxWidth / 2,
            100,
            InputBoxWidth,
            InputBoxHeight
        );

        _submitButton = new Rectangle(
            _inputBox.X + InputBoxWidth + 10,
            _inputBox.Y + 2.5f,
            ButtonWidth,
            ButtonHeight
        );

        _playAgainButton = new Rectangle(
            WindowWidth / 2 - ButtonWidth / 2,
            WindowHeight - 100,
            ButtonWidth,
            ButtonHeight
        );
    }

    public void Run()
    {
        Raylib.InitWindow(WindowWidth, WindowHeight, "Mastermind Game");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            HandleInput();
            Update();
            Draw();
        }

        Raylib.CloseWindow();
    }

    private void HandleInput()
    {
        if (!_game.IsOver)
        {
            int key = Raylib.GetCharPressed();

            while (key > 0)
            {
                if (key >= '0' && key <= '8' && _currentInput.Length < 4)
                {
                    _currentInput += (char)key;
                    ClearError();
                }
                key = Raylib.GetCharPressed();
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && _currentInput.Length > 0)
            {
                _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                ClearError();
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                SubmitGuess();
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePos = Raylib.GetMousePosition();
                if (Raylib.CheckCollisionPointRec(mousePos, _submitButton))
                {
                    SubmitGuess();
                }
            }
        }
        else
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePos = Raylib.GetMousePosition();
                if (Raylib.CheckCollisionPointRec(mousePos, _playAgainButton))
                {
                    RestartGame();
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.R))
            {
                RestartGame();
            }
        }
    }

    private void Update()
    {
        if (_showingError)
        {
            _errorTimer += Raylib.GetFrameTime();
            if (_errorTimer >= ErrorDisplayTime)
            {
                ClearError();
            }
        }
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DarkBlue);

        string title = "MASTERMIND";
        int titleWidth = Raylib.MeasureText(title, 30);
        Raylib.DrawText(title, WindowWidth / 2 - titleWidth / 2, 30, 30, Color.White);

        if (!_game.IsOver)
        {
            DrawGameUi();
        }
        else
        {
            DrawEndGameUi();
        }

        string attemptsText = $"Attempts: {_game.History.Count} / {_game.History.Count + _game.RemainingAttempts}";
        Raylib.DrawText(attemptsText, 20, 20, 20, Color.White);

        DrawHistory();

        if (_showingError)
        {
            DrawErrorMessage();
        }

        Raylib.EndDrawing();
    }

    private void DrawGameUi()
    {
        string inputLabel = "Enter your 4-digit guess (0-8):";
        int labelWidth = Raylib.MeasureText(inputLabel, 20);
        Raylib.DrawText(inputLabel, WindowWidth / 2 - labelWidth / 2, 70, 20, Color.White);

        Color inputBoxColor = Color.White;
        if (_currentInput.Length == 4)
        {
            inputBoxColor = Color.Green;
        }
        else if (_currentInput.Length > 0)
        {
            inputBoxColor = Color.Yellow;
        }

        Raylib.DrawRectangleRec(_inputBox, inputBoxColor);
        Raylib.DrawRectangleLinesEx(_inputBox, 2, Color.Black);

        if (_currentInput.Length > 0)
        {
            string displayText = string.Join(" ", _currentInput.ToCharArray());
            int textWidth = Raylib.MeasureText(displayText, 20);
            Raylib.DrawText(displayText,
                (int)(_inputBox.X + InputBoxWidth / 2 - textWidth / 2),
                (int)(_inputBox.Y + 10), 20, Color.Black);
        }

        if ((int)(Raylib.GetTime() * 2) % 2 == 0) 
        {
            int cursorX = (int)(_inputBox.X + 10 + _currentInput.Length * 15);
            Raylib.DrawLine(cursorX, (int)(_inputBox.Y + 8), cursorX, (int)(_inputBox.Y + 32), Color.Black);
        }

        bool canSubmit = _currentInput.Length == 4;
        Color buttonColor = canSubmit ? Color.Green : Color.Gray;

        Raylib.DrawRectangleRec(_submitButton, buttonColor);
        Raylib.DrawRectangleLinesEx(_submitButton, 2, Color.Black);

        string buttonText = "Submit";
        int buttonTextWidth = Raylib.MeasureText(buttonText, 16);
        Raylib.DrawText(buttonText,
            (int)(_submitButton.X + ButtonWidth / 2 - buttonTextWidth / 2),
            (int)(_submitButton.Y + 10), 16, Color.White);

        string instructions = "Use digits 0-8 only. Press Enter or click Submit.";
        int instrWidth = Raylib.MeasureText(instructions, 16);
        Raylib.DrawText(instructions, WindowWidth / 2 - instrWidth / 2, 160, 16, Color.LightGray);
    }

    private void DrawEndGameUi()
    {
        string resultText;
        Color resultColor;

        if (_game.HasWon)
        {
            resultText = "CONGRATULATIONS! YOU WON!";
            resultColor = Color.Green;
        }
        else
        {
            resultText = "GAME OVER - YOU LOST";
            resultColor = Color.Red;
        }

        int resultWidth = Raylib.MeasureText(resultText, 24);
        Raylib.DrawText(resultText, WindowWidth / 2 - resultWidth / 2, 80, 24, resultColor);

        string secretText = $"Secret Code: {_game.SecretCode}";
        int secretWidth = Raylib.MeasureText(secretText, 20);
        Raylib.DrawText(secretText, WindowWidth / 2 - secretWidth / 2, 120, 20, Color.Yellow);

        Raylib.DrawRectangleRec(_playAgainButton, Color.Blue);
        Raylib.DrawRectangleLinesEx(_playAgainButton, 2, Color.White);

        string playAgainText = "Play Again";
        int playAgainWidth = Raylib.MeasureText(playAgainText, 16);
        Raylib.DrawText(playAgainText,
            (int)(_playAgainButton.X + ButtonWidth / 2 - playAgainWidth / 2),
            (int)(_playAgainButton.Y + 10), 16, Color.White);

        string restartInstr = "Press 'R' or click 'Play Again' to restart";
        int restartWidth = Raylib.MeasureText(restartInstr, 16);
        Raylib.DrawText(restartInstr, WindowWidth / 2 - restartWidth / 2,
            WindowHeight - 60, 16, Color.LightGray);
    }

    private void DrawHistory()
    {
        if (_game.History.Count == 0) return;

        string historyTitle = "Guess History:";
        Raylib.DrawText(historyTitle, 50, 200, 18, Color.White);

        int yOffset = 230;
        for (int i = 0; i < _game.History.Count; i++)
        {
            var entry = _game.History[i];
            string guessText = $"{i + 1}. {FormatGuess(entry.Guess)}";

            Raylib.DrawText(guessText, 50, yOffset, 16, Color.White);

            Raylib.DrawText("Well Placed:", 200, yOffset, 16, Color.Red);
            Raylib.DrawText($" {entry.Result.WellPlaced}", 290, yOffset, 16, Color.White);
            Raylib.DrawText("Misplaced:", 330, yOffset, 16, Color.Yellow);
            Raylib.DrawText($" {entry.Result.Misplaced}", 400, yOffset, 16, Color.White);

            yOffset += 25;

            if (yOffset > WindowHeight - 100) break;
        }

        string legend = "Red = Well placed    Yellow = Misplaced";
        Raylib.DrawText(legend, 50, yOffset + 10, 14, Color.LightGray);
    }

    private void DrawErrorMessage()
    {
        Rectangle errorRect = new Rectangle(
            WindowWidth / 2 - 200,
            _inputBox.Y + InputBoxHeight + 10,
            400,
            30
        );

        Raylib.DrawRectangleRec(errorRect, Color.Red);
        Raylib.DrawRectangleLinesEx(errorRect, 2, Color.DarkPurple);

        int errorWidth = Raylib.MeasureText(_errorMessage, 16);
        Raylib.DrawText(_errorMessage,
            (int)(errorRect.X + 200 - errorWidth / 2),
            (int)(errorRect.Y + 7), 16, Color.White);
    }

    private void SubmitGuess()
    {
        if (_currentInput.Length != 4)
        {
            ShowError("Please enter exactly 4 digits!");
            return;
        }

        var result = _game.SubmitGuess(_currentInput);
        if (!result.Success)
        {
            ShowError(result.ErorrMsg);
        }
        else
        {
            _currentInput = "";
            ClearError();
        }
    }

    private void ShowError(string message)
    {
        _errorMessage = message;
        _showingError = true;
        _errorTimer = 0f;
    }

    private void ClearError()
    {
        _showingError = false;
        _errorMessage = "";
        _errorTimer = 0f;
    }

    private void RestartGame()
    {
        _game = new Game(null, _game.History.Count + _game.RemainingAttempts);
        _currentInput = "";
        ClearError();
    }

    private string FormatGuess(string guess)
    {
        return string.Join(" ", guess.ToCharArray());
    }
}