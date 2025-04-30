using Application.Abstractions.Messaging;

namespace GamingOne.Modules.Gaming.Application.StartGame;

public sealed record StartGameCommand(Guid GameId) : ICommand;