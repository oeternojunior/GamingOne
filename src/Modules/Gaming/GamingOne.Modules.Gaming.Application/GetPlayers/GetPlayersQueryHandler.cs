using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Models;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Application.GetPlayers;

internal sealed class GetPlayersQueryHandler(GameManager gameManager) : IQueryHandler<GetPlayersQuery, IReadOnlyList<Player>>
{
    public Task<Result<IReadOnlyList<Player>>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        var game = gameManager.GetGame(request.GameId);
        return Task.FromResult(game != null
            ? Result.Success(game.Players)
            : Result.Failure<IReadOnlyList<Player>>(Error.NotFound("NotFound", $"Game with ID {request.GameId} not found.")));
    }
}
