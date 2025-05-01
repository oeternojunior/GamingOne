using GamingOne.Modules.Gaming.Domain.Enums;
using GamingOne.Modules.Gaming.Domain.Services;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Domain.Models;

public sealed class GameSession : Entity
{
    private readonly List<Player> _players = new List<Player>();
    private readonly List<PlayerGuess> _guesses = new List<PlayerGuess>();
    private readonly IRandomNumberGenerator _randomNumberGenerator;

    private int _currentTurnIndex = -1;

    public int Min { get; private set; }
    public int Max { get; private set; }
    public int MysteryNumber { get; private set; }
    public GameStatus Status { get; private set; } = GameStatus.NotStarted;
    public Player Winner { get; private set; }
    public IReadOnlyList<Player> Players => _players.AsReadOnly();
    public IReadOnlyList<PlayerGuess> Guesses => _guesses.AsReadOnly();

    private GameSession() { } // For EF Core

    private GameSession(int min, int max, IRandomNumberGenerator randomNumberGenerator, Guid id)
    {
        Ensure.NotNegative(min, "Min must be non-negative.");
        Ensure.NotNegative(max, "Max must be non-negative.");
        if (min >= max) throw new ArgumentException("Min must be less than Max.");
        Id = id;
        Min = min;
        Max = max;
        _randomNumberGenerator = randomNumberGenerator;
    }

    public static GameSession Create(int min, int max, IRandomNumberGenerator randomNumberGenerator)
    {
        return new GameSession(min, max, randomNumberGenerator, Guid.NewGuid());
    }

    public void StartGame()
    {
        if (Status != GameStatus.NotStarted)
            throw new InvalidOperationException("Game has already started or completed.");

        MysteryNumber = _randomNumberGenerator.Generate(Min, Max);
        Status = GameStatus.InProgress;
    }

    public void AddPlayer(Player player)
    {
        if (Status != GameStatus.NotStarted)
            throw new InvalidOperationException("Cannot add players after the game has started.");

        _players.Add(player);
    }

    public GuessResult SubmitGuess(Player player, int guess)
    {
        if (Status != GameStatus.InProgress)
            throw new InvalidOperationException("Game is not active.");
        if (!_players.Contains(player))
            throw new ArgumentException("Player is not part of this game session.");
        if (guess < Min || guess > Max)
            throw new ArgumentException("Guess is outside the valid range.");

        // Multiplayer validation
        if (_players.Count > 1 && _guesses.Count > 0)
        {
            var lastGuess = _guesses[^1];
            if (lastGuess.Player.Id == player.Id)
            {
                throw new InvalidOperationException(
                    "Player cannot submit consecutive guesses in multiplayer mode. " +
                    "Wait for another player to guess.");
            }
        }

        if (_players.Count > 1)
        {
            // Get expected next player index
            var expectedPlayerIndex = (_currentTurnIndex + 1) % _players.Count;
            var expectedPlayer = _players[expectedPlayerIndex];

            if (player.Id != expectedPlayer.Id)
            {
                throw new InvalidOperationException(
                    $"It's {expectedPlayer.Name}'s turn. " +
                    $"{player.Name} cannot guess now.");
            }

            _currentTurnIndex = expectedPlayerIndex;
        }

        var result = guess == MysteryNumber ? GuessResult.Correct :
            guess < MysteryNumber ? GuessResult.Higher : GuessResult.Lower;

        _guesses.Add(PlayerGuess.Create(player, guess, result));

        if (result == GuessResult.Correct)
        {
            Winner = player;
            Status = GameStatus.Completed;
        }

        return result;
    }
}