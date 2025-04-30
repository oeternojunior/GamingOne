using SharedKernel;

namespace GamingOne.Modules.Gaming.Domain.Enums;

public class GameStatus : Enumeration
{
    public static readonly GameStatus NotStarted = new(1, "Not Started");
    public static readonly GameStatus InProgress = new(2, "In Progress");
    public static readonly GameStatus Completed = new(3, "Completed");

    private GameStatus() { } // For EF Core

    private GameStatus(int value, string name) : base(value, name) { }

    public static GameStatus FromValue(int value) => GetAll<GameStatus>().FirstOrDefault(x => x.Value == value)
        ?? throw new ApplicationException($"Difficulty {value} not found");

    public static GameStatus FromName(string name) => GetAll<GameStatus>().FirstOrDefault(x => x.Name == name)
    ?? throw new ApplicationException($"Difficulty {name} not found");
}
