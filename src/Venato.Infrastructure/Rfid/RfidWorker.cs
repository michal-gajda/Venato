namespace Venato.Infrastructure.Rfid;

using System.IO.Ports;
using Microsoft.Extensions.Hosting;
using Venato.Application.Rfid.Events;

public sealed class RfidWorker : BackgroundService
{
    private readonly ILogger<RfidWorker> logger;
    private readonly IMediator mediator;
    private readonly SerialPort serialPort;

    public RfidWorker(ILogger<RfidWorker> logger, IMediator mediator, RfidOptions options)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.serialPort = new SerialPort(portName: options.PortName, baudRate: options.BaudRate, parity: Parity.None, dataBits: 8);
        this.serialPort.DataReceived += this.DataReceived;
        this.serialPort.Open();
    }

    private void DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (e.EventType != SerialData.Chars)
        {
            return;
        }

        var cardId = (sender as SerialPort)!.ReadExisting();
        var notification = new RfidRead
        {
            CardId = cardId,
        };
        
        this.mediator.Publish(notification);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        this.serialPort.DataReceived -= this.DataReceived;
        _ = serialPort.ReadExisting();
        this.serialPort.Close();
        this.serialPort.Dispose();
        
        base.Dispose();
    }
}
