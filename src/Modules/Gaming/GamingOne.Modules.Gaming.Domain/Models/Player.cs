using SharedKernel;

namespace GamingOne.Modules.Gaming.Domain.Models;

public sealed class Player : Entity
{
    public string Name { get; private set; }

    private Player() { } // For EF Core

    private Player(string name, Guid id)
    {
        Ensure.NotNullOrEmpty(name);
        Ensure.NotWhitespace(name);
        Ensure.NotNegative(name.Length);
        Id = id;
        Name = name;
    }

    public static Player Create(string name)
    {
        return new Player(name, Guid.NewGuid());
    }
}
