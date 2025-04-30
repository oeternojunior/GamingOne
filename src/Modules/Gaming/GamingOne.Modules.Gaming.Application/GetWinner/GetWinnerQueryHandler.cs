using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Models;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Application.GetWinner;

internal sealed class GetWinnerQueryHandler(GameManager gameManager) : IQueryHandler<GetWinnerQuery, Player>
{
    public Task<Result<Player>> Handle(GetWinnerQuery request, CancellationToken cancellationToken)
    {
        var game = gameManager.GetGame(request.GameId);
        return game != null
            ? Task.FromResult(Result.Success(game.Winner))
            : Task.FromResult(Result.Failure<Player>(Error.NotFound("NotFound", $"Game with ID {request.GameId} not found.")));
    }
}
