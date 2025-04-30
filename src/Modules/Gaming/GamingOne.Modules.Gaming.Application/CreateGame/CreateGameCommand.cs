using Application.Abstractions.Messaging;

namespace GamingOne.Modules.Gaming.Application.CreateGame;

public sealed record CreateGameCommand(int Min, int Max) : ICommand<Guid>;
