using Application.Abstractions.Messaging;
using GamingOne.Modules.Gaming.Domain.Enums;
using GamingOne.Modules.Gaming.Domain.Models;

namespace GamingOne.Modules.Gaming.Application.SubmitGuess;

public sealed record SubmitGuessCommand(Guid GameId, Guid PlayerId, int Guess) : ICommand<GuessResult>;
