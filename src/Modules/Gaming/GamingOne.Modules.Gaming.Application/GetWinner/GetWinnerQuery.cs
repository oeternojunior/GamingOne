using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Models;

namespace GamingOne.Modules.Gaming.Application.GetWinner;

public sealed record class GetWinnerQuery(Guid GameId) : IQuery<Player>;