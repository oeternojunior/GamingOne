using GamingOne.HiLo.Api.Endpoints.Gaming.Requests;
using GamingOne.HiLo.Api.Extensions;
using GamingOne.HiLo.Api.Infrastructure;
using GamingOne.Modules.Gaming.Application.SubmitGuess;
using GamingOne.Modules.Gaming.Domain.Enums;
using MediatR;
using SharedKernel;

namespace GamingOne.HiLo.Api.Endpoints.Gaming;

public sealed class SubmitGuess : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("submit-guess", async (ISender sender, SubmitGuessRequest request, CancellationToken cancellationToken) =>
        {
            var command = new SubmitGuessCommand(request.GameId, request.PlayerId, request.Guess);
            Result<GuessResult> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Gaming);
    }
}
