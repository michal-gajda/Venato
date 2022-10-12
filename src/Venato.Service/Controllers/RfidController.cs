namespace Venato.Service.Controllers;

using Microsoft.AspNetCore.Mvc;
using Venato.Application.Rfid.Commands;

[ApiController, Route("[controller]")]
internal sealed class RfidController : ControllerBase
{
    private readonly ILogger<RfidController> logger;
    private readonly IMediator mediator;

    public RfidController(ILogger<RfidController> logger, IMediator mediator)
        => (this.logger, this.mediator) = (logger, mediator);

    [HttpPost(Name = "SendCardId")]
    public async Task<ActionResult> PostAsync(SendCardId request, CancellationToken cancellationToken = default)
    {
        this.logger.LogInformation("Received {CardId}", request.CardId);
        _ = this.mediator.Send(request, cancellationToken);
        return await Task.FromResult(this.Ok());
    }
}
