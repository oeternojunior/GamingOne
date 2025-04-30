using GamingOne.Modules.Gaming.Domain.Services;

namespace GamingOne.Modules.Gaming.Infrastructure.Services;

internal sealed class DefaultRandomNumberGenerator(Random random) : IRandomNumberGenerator
{
    public int Generate(int min, int max)
    {
        return random.Next(min, max + 1);
    }
}
