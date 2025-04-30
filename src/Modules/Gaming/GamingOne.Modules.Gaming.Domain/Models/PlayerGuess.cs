using GamingOne.Modules.Gaming.Domain.Enums;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Domain.Models;

public sealed class PlayerGuess : Entity
{
    public Player Player { get; private set; }
    public int Guess { get; private set; }
    public GuessResult Result { get; private set; }
    public DateTime Timestamp { get; private set; }

    private PlayerGuess() { } // For EF Core

    private PlayerGuess(Player player, int guess, GuessResult result, Guid id)
    {
        Ensure.NotNull(player);
        Ensure.NotNegative(guess);
        Ensure.NotNull(result);

        Id = id;
        Player = player;
        Guess = guess;
        Result = result;
        Timestamp = DateTime.UtcNow;
    }

    public static PlayerGuess Create(Player player, int guess, GuessResult result)
    {
        return new PlayerGuess(player, guess, result, Guid.NewGuid());
    }
}
