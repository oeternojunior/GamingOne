using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Enums;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Application.GetGameStatus;

internal sealed class GetGameStatusQueryHandler(GameManager gameManager) : IQueryHandler<GetGameStatusQuery, GameStatus>
{
    public Task<Result<GameStatus>> Handle(GetGameStatusQuery request, CancellationToken cancellationToken)
    {
        var game = gameManager.GetGame(request.GameId);
        return Task.FromResult(game != null 
            ? Result.Success(game.Status) 
            : Result.Failure<GameStatus>(Error.NotFound("NotFound", $"Game with ID {request.GameId} not found.")));
    }
}
