namespace Venato.Application.Rfid.CommandHandlers;

using Venato.Application.Rfid.Commands;
using Venato.Application.Rfid.Events;

internal sealed class SendCardIdHandler : IRequestHandler<SendCardId>
{
    private readonly ILogger<SendCardIdHandler> logger;
    private readonly IMediator mediator;

    public SendCardIdHandler(ILogger<SendCardIdHandler> logger, IMediator mediator)
        => (this.logger, this.mediator) = (logger, mediator);

    public async Task<Unit> Handle(SendCardId request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Sent {CardId}", request.CardId);

        var notification = new RfidRead
        {
            CardId = request.CardId,
        };

        await this.mediator.Publish(notification, cancellationToken);

        return await Task.FromResult(Unit.Value);
    }
}
