namespace GestioneSagre.Utility.Frontend.Controllers;

public class UtilityController : BaseController
{
    IRequestClient<ScontrinoPagatoListRequest> scontrinoPagatoRequestClient;
    IRequestClient<ScontrinoStatoListRequest> scontrinoStatoRequestClient;
    IRequestClient<TipoClienteListRequest> tipoClienteRequestClient;
    IRequestClient<TipoPagamentoListRequest> tipoPagamentoRequestClient;
    IRequestClient<TipoScontrinoListRequest> tipoScontrinoRequestClient;

    private readonly ILoggerService logger;

    public UtilityController(IRequestClient<ScontrinoPagatoListRequest> scontrinoPagatoRequestClient,
        IRequestClient<ScontrinoStatoListRequest> scontrinoStatoRequestClient, IRequestClient<TipoClienteListRequest> tipoClienteRequestClient,
        IRequestClient<TipoPagamentoListRequest> tipoPagamentoRequestClient, IRequestClient<TipoScontrinoListRequest> tipoScontrinoRequestClient,
        ILoggerService logger)
    {
        this.scontrinoPagatoRequestClient = scontrinoPagatoRequestClient;
        this.scontrinoStatoRequestClient = scontrinoStatoRequestClient;
        this.tipoClienteRequestClient = tipoClienteRequestClient;
        this.tipoPagamentoRequestClient = tipoPagamentoRequestClient;
        this.tipoScontrinoRequestClient = tipoScontrinoRequestClient;
        this.logger = logger;
    }

    [HttpGet("ScontrinoPagato")]
    [ProducesResponseType(typeof(List<ScontrinoPagato>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScontrinoPagatoAsync()
    {
        var result = new List<ScontrinoPagato>();

        using (var request = scontrinoPagatoRequestClient.Create(new ScontrinoPagatoListRequest { }))
        {
            var response = await request.GetResponse<ScontrinoPagatoListResponse>();
            result = response.Message.ScontriniPagati;

            switch (result.Count)
            {
                case > 0:
                    logger.SaveLogInformation($"Trovati {result.Count} record: ScontrinoPagato");
                    break;
                default:
                    logger.SaveLogWarning("Nessun record trovato: ScontrinoPagato");
                    break;
            }
        }

        return result.Count > 0 ? Ok(new DefaultResponse(true, result))
            : throw new ExceptionResponse(HttpStatusCode.NotFound, 0, "NotFound", $"Nessun record trovato: ScontrinoPagato");
    }

    [HttpGet("ScontrinoStato")]
    [ProducesResponseType(typeof(List<ScontrinoStato>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScontrinoStatoAsync()
    {
        var result = new List<ScontrinoStato>();

        using (var request = scontrinoStatoRequestClient.Create(new ScontrinoStatoListRequest { }))
        {
            var response = await request.GetResponse<ScontrinoStatoListResponse>();
            result = response.Message.ScontriniStati;

            switch (result.Count)
            {
                case > 0:
                    logger.SaveLogInformation($"Trovati {result.Count} record: ScontrinoStato");
                    break;
                default:
                    logger.SaveLogWarning("Nessun record trovato: ScontrinoStato");
                    break;
            }
        }

        return result.Count > 0 ? Ok(new DefaultResponse(true, result))
            : throw new ExceptionResponse(HttpStatusCode.NotFound, 0, "NotFound", $"Nessun record trovato: ScontrinoStato");
    }

    [HttpGet("TipoCliente")]
    [ProducesResponseType(typeof(List<TipoCliente>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTipoClienteAsync()
    {
        var result = new List<TipoCliente>();

        using (var request = tipoClienteRequestClient.Create(new TipoClienteListRequest { }))
        {
            var response = await request.GetResponse<TipoClienteListResponse>();
            result = response.Message.TipoClienti;

            switch (result.Count)
            {
                case > 0:
                    logger.SaveLogInformation($"Trovati {result.Count} record: TipoCliente");
                    break;
                default:
                    logger.SaveLogWarning("Nessun record trovato: TipoCliente");
                    break;
            }
        }

        return result.Count > 0 ? Ok(new DefaultResponse(true, result))
            : throw new ExceptionResponse(HttpStatusCode.NotFound, 0, "NotFound", $"Nessun record trovato: TipoCliente");
    }

    [HttpGet("TipoPagamento")]
    [ProducesResponseType(typeof(List<TipoPagamento>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTipoPagamentoAsync()
    {
        var result = new List<TipoPagamento>();

        using (var request = tipoPagamentoRequestClient.Create(new TipoPagamentoListRequest { }))
        {
            var response = await request.GetResponse<TipoPagamentoListResponse>();
            result = response.Message.TipoPagamenti;

            switch (result.Count)
            {
                case > 0:
                    logger.SaveLogInformation($"Trovati {result.Count} record: TipoPagamento");
                    break;
                default:
                    logger.SaveLogWarning("Nessun record trovato: TipoPagamento");
                    break;
            }
        }

        return result.Count > 0 ? Ok(new DefaultResponse(true, result))
            : throw new ExceptionResponse(HttpStatusCode.NotFound, 0, "NotFound", $"Nessun record trovato: TipoPagamento");
    }

    [HttpGet("TipoScontrino")]
    [ProducesResponseType(typeof(List<TipoScontrino>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTipoScontrinoAsync()
    {
        var result = new List<TipoScontrino>();

        using (var request = tipoScontrinoRequestClient.Create(new TipoScontrinoListRequest { }))
        {
            var response = await request.GetResponse<TipoScontrinoListResponse>();
            result = response.Message.TipoScontrini;

            switch (result.Count)
            {
                case > 0:
                    logger.SaveLogInformation($"Trovati {result.Count} record: TipoScontrino");
                    break;
                default:
                    logger.SaveLogWarning("Nessun record trovato: TipoScontrino");
                    break;
            }
        }

        return result.Count > 0 ? Ok(new DefaultResponse(true, result))
            : throw new ExceptionResponse(HttpStatusCode.NotFound, 0, "NotFound", $"Nessun record trovato: TipoScontrino");
    }
}