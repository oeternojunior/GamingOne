using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Models;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Application.GetGuesses;

internal sealed class GetGuessesQueryHandler(GameManager gameManager) : IQueryHandler<GetGuessesQuery, IReadOnlyList<PlayerGuess>>
{
    public Task<Result<IReadOnlyList<PlayerGuess>>> Handle(GetGuessesQuery request, CancellationToken cancellationToken)
    {
        var game = gameManager.GetGame(request.GameId);
        return Task.FromResult(game != null
            ? Result.Success(game.Guesses)
            : Result.Failure<IReadOnlyList<PlayerGuess>>(Error.NotFound("NotFound", $"Game with ID {request.GameId} not found.")));
    }
}
