using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Models;

namespace GamingOne.Modules.Gaming.Application.GetPlayers;

public sealed record GetPlayersQuery(Guid GameId) : IQuery<IReadOnlyList<Player>>;
