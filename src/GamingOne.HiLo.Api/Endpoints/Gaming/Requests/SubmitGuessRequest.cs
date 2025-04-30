
using GamingOne.Modules.Gaming.Domain.Models;

namespace GamingOne.HiLo.Api.Endpoints.Gaming.Requests;

public sealed record SubmitGuessRequest(Guid GameId, Guid PlayerId, int Guess);