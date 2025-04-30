using Application.Abstractions.Messaging;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Application.StartGame;

internal sealed class StartGameCommandHandler(GameManager gameManager) : ICommandHandler<StartGameCommand>
{
    public Task<Result> Handle(StartGameCommand request, CancellationToken cancellationToken)
    {
        var game = gameManager.GetGame(request.GameId);

        if (game is null)
            return Task.FromResult(Result.Failure(Error.NotFound("NotFound", "Game not found.")));

        game?.StartGame();

        return Task.FromResult(Result.Success());
    }
}