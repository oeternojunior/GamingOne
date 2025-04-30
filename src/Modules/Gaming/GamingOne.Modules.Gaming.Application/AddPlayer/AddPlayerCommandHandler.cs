using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Models;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Application.AddPlayer;

internal sealed class AddPlayerCommandHandler(GameManager gameManager) : ICommandHandler<AddPlayerCommand, Player>
{

    public Task<Result<Player>> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
    {
        var game = gameManager.GetGame(request.GameId);
        if (game == null)
            return Task.FromResult(Result.Failure<Player>(Error.NotFound("NotFound", $"Game with ID {request.GameId} not found.")));

        var player = Player.Create(request.PlayerName);
        game.AddPlayer(player);

        return Task.FromResult(Result.Success(player));
    }
}
