namespace Venato.Infrastructure.Rfid;

using System.ComponentModel.DataAnnotations;

public sealed class RfidOptions
{
    public const string SectionName = "Rfid";

    [Required] public string PortName { get; init; } = string.Empty;
    [Required] public int BaudRate { get; init; } = 9600;
}
