using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Models;
namespace GamingOne.Modules.Gaming.Application.AddPlayer;

public sealed record AddPlayerCommand(Guid GameId, string PlayerName) : ICommand<Player>;
