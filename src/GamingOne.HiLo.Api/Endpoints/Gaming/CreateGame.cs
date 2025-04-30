using GamingOne.HiLo.Api.Endpoints.Gaming.Requests;
using GamingOne.HiLo.Api.Extensions;
using GamingOne.HiLo.Api.Infrastructure;
using GamingOne.Modules.Gaming.Application.CreateGame;
using MediatR;
using SharedKernel;

namespace GamingOne.HiLo.Api.Endpoints.Gaming;

public sealed class CreateGame : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/create-game", async (ISender sender, CreateGameRequest request, CancellationToken cancellationToken) =>
        {
            var command = new CreateGameCommand(request.Min, request.Max);
            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Gaming);
    }
}
