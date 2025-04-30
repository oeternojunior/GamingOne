using GamingOne.HiLo.Api.Endpoints.Gaming.Requests;
using GamingOne.HiLo.Api.Extensions;
using GamingOne.HiLo.Api.Infrastructure;
using GamingOne.Modules.Gaming.Application.AddPlayer;
using GamingOne.Modules.Gaming.Domain.Models;
using MediatR;
using SharedKernel;

namespace GamingOne.HiLo.Api.Endpoints.Gaming;

public sealed class AddPlayer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/add-player", async (ISender sender, AddPlayerRequest request, CancellationToken cancellationToken) =>
        {
            var command = new AddPlayerCommand(request.GameId, request.PlayerName);
            Result<Player> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Gaming);
    }
}
