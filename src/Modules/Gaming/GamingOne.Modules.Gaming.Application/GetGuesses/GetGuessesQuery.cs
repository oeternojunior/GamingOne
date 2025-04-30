using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Models;

namespace GamingOne.Modules.Gaming.Application.GetGuesses;

public sealed record GetGuessesQuery(Guid GameId) : IQuery<IReadOnlyList<PlayerGuess>>;