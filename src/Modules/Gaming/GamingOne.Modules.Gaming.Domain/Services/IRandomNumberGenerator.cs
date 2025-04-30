namespace GamingOne.Modules.Gaming.Domain.Services;

public interface IRandomNumberGenerator
{
    int Generate(int min, int max);
}
