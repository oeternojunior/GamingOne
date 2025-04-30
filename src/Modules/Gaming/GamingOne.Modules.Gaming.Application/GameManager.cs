using GamingOne.Modules.Gaming.Domain.Models;
using GamingOne.Modules.Gaming.Domain.Services;
using System.Collections.Concurrent;

namespace GamingOne.Modules.Gaming.Application;

internal sealed class GameManager
{
    private readonly ConcurrentDictionary<Guid, GameSession> _gameSessions = new();
    private readonly IRandomNumberGenerator _randomNumberGenerator;

    public GameManager(IRandomNumberGenerator randomNumberGenerator)
    {
        _randomNumberGenerator = randomNumberGenerator;
    }

    public GameSession CreateGame(int min, int max)
    {
        var game = GameSession.Create(min, max, _randomNumberGenerator);
        _gameSessions.TryAdd(game.Id, game);
        return game;
    }

    public GameSession? GetGame(Guid gameId) => _gameSessions.TryGetValue(gameId, out var game) ? game : null;
}
