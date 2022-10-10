namespace Venato.Application.Rfid.Events;

public sealed record RfidRead : INotification
{
    public string CardId { get; init; } = string.Empty;
}
