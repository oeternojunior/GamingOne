using GamingOne.HiLo.Api.Extensions;
using GamingOne.HiLo.Api.Infrastructure;
using GamingOne.Modules.Gaming.Application.GetGameStatus;
using GamingOne.Modules.Gaming.Domain.Enums;
using MediatR;
using SharedKernel;

namespace GamingOne.HiLo.Api.Endpoints.Gaming;

public sealed class GetGameStatus : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("game-status", async (Guid gameId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetGameStatusQuery(gameId);
            Result<GameStatus> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Gaming);
    }
}
