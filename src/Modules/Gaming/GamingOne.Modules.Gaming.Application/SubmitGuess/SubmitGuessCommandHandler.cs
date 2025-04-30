using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Enums;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Application.SubmitGuess;

internal sealed class SubmitGuessCommandHandler(GameManager gameManager) : ICommandHandler<SubmitGuessCommand, GuessResult>
{
    public Task<Result<GuessResult>> Handle(SubmitGuessCommand request, CancellationToken cancellationToken)
    {
        var game = gameManager.GetGame(request.GameId);

        if (game == null)
            return Task.FromResult(Result.Failure<GuessResult>(Error.NotFound("NotFound", $"Game with ID {request.GameId} not found.")));

        var player = game.Players.FirstOrDefault(o => o.Id == request.PlayerId);
        var gameResult = game.SubmitGuess(player, request.Guess);

        return Task.FromResult(Result.Success(gameResult));
    }
}
