namespace Venato.Application.Rfid.Commands;

public sealed record SendCardId : IRequest
{
    public string CardId { get; init; } = string.Empty;
}
