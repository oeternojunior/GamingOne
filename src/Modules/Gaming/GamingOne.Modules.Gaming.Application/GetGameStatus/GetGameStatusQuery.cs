using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Enums;

namespace GamingOne.Modules.Gaming.Application.GetGameStatus;

public sealed record class GetGameStatusQuery(Guid GameId) : IQuery<GameStatus>;
