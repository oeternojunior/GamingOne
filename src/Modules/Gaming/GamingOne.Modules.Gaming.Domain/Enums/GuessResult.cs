using SharedKernel;

namespace GamingOne.Modules.Gaming.Domain.Enums;

public class GuessResult : Enumeration
{
    public static readonly GuessResult Correct = new(1, "Correct");
    public static readonly GuessResult Higher = new(2, "Higher");
    public static readonly GuessResult Lower = new(3, "Lower");

    private GuessResult() { } // For EF Core

    private GuessResult(int value, string name) : base(value, name) { }

    public static GuessResult FromValue(int value) => GetAll<GuessResult>().FirstOrDefault(x => x.Value == value)
        ?? throw new ApplicationException($"Difficulty {value} not found");

    public static GuessResult FromName(string name) => GetAll<GuessResult>().FirstOrDefault(x => x.Name == name)
    ?? throw new ApplicationException($"Difficulty {name} not found");
}
