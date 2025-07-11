# Savvy Skills Assessment - Mastermind

This project is a C# implementation of the classic code-breaking game Mastermind. You can play it in either a console-based version or a graphical version using Raylib.

## How to Run

1.  **Run from the command line:**
    Navigate to the project and run the following command:

    ```bash
    dotnet run --project mastermind/mastermind.csproj

    ```

## Command-line Flags

You can customize the game using the following flags:

- **`-g` or `--gui`**: Run the game in graphical mode.

  ```bash
  dotnet run -- -g
  ```

- **`-c <cood>`**: Specify the secret code for the game.

  ```bash
  dotnet run -- -c 1234
  ```

- **`-t <number>`**: Set the maximum number of attempts.
  ```bash
  dotnet run -- -t 5
  ```

You can also combine these flags:

```bash
dotnet run -- -g -c 1234 -t 5
# or
dotnet run --project mastermind/mastermind.csproj -g -c 1234 -t 5

```
