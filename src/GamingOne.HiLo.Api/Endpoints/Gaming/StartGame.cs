using GamingOne.HiLo.Api.Endpoints.Gaming.Requests;
using GamingOne.Modules.Gaming.Application.StartGame;
using MediatR;
using SharedKernel;

namespace GamingOne.HiLo.Api.Endpoints.Gaming;

public sealed class StartGame : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("start-game", async (ISender sender, StartGameRequest request, CancellationToken cancellationToken) =>
        {
            var command = new StartGameCommand(request.GameId);
            Result result = await sender.Send(command, cancellationToken);
        }).WithTags(Tags.Gaming);
    }
}
