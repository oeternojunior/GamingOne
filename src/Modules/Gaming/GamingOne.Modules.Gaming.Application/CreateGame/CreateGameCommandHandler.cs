using Application.Abstractions.Messaging;
using SharedKernel;

namespace GamingOne.Modules.Gaming.Application.CreateGame;

internal sealed class CreateGameCommandHandler(GameManager gameManager) : ICommandHandler<CreateGameCommand, Guid>
{
    public Task<Result<Guid>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var game = gameManager.CreateGame(request.Min, request.Max);
        return Task.FromResult(game != null
            ? Result.Success(game.Id)
            : Result.Failure<Guid>(Error.Problem("ProblemCreateGame", "Failure when attemp to create a game")));
    }
}
