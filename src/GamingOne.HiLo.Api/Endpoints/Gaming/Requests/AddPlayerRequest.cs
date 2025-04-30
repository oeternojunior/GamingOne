namespace GamingOne.HiLo.Api.Endpoints.Gaming.Requests;

public sealed record AddPlayerRequest(Guid GameId, string PlayerName);