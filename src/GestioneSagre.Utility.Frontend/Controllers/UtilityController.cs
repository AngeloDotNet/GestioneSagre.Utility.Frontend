namespace GestioneSagre.Utility.Frontend.Controllers;

public class UtilityController : BaseController
{
    IRequestClient<ScontrinoPagatoListRequest> scontrinoPagatoRequestClient;
    IRequestClient<ScontrinoStatoListRequest> scontrinoStatoRequestClient;
    IRequestClient<TipoClienteListRequest> tipoClienteRequestClient;
    IRequestClient<TipoPagamentoListRequest> tipoPagamentoRequestClient;
    IRequestClient<TipoScontrinoListRequest> tipoScontrinoRequestClient;

    public UtilityController(IRequestClient<ScontrinoPagatoListRequest> scontrinoPagatoRequestClient,
        IRequestClient<ScontrinoStatoListRequest> scontrinoStatoRequestClient,
        IRequestClient<TipoClienteListRequest> tipoClienteRequestClient,
        IRequestClient<TipoPagamentoListRequest> tipoPagamentoRequestClient,
        IRequestClient<TipoScontrinoListRequest> tipoScontrinoRequestClient)
    {
        this.scontrinoPagatoRequestClient = scontrinoPagatoRequestClient;
        this.scontrinoStatoRequestClient = scontrinoStatoRequestClient;
        this.tipoClienteRequestClient = tipoClienteRequestClient;
        this.tipoPagamentoRequestClient = tipoPagamentoRequestClient;
        this.tipoScontrinoRequestClient = tipoScontrinoRequestClient;
    }

    [HttpGet("ScontrinoPagato")]
    public async Task<IActionResult> GetScontrinoPagatoAsync()
    {
        var result = new List<ScontrinoPagato>();

        using (var request = scontrinoPagatoRequestClient.Create(new ScontrinoPagatoListRequest { }))
        {
            var response = await request.GetResponse<ScontrinoPagatoListResponse>();
            result = response.Message.ScontriniPagati;
        }

        return Ok(result);
    }

    [HttpGet("ScontrinoStato")]
    public async Task<IActionResult> GetScontrinoStatoAsync()
    {
        var result = new List<ScontrinoStato>();

        using (var request = scontrinoStatoRequestClient.Create(new ScontrinoStatoListRequest { }))
        {
            var response = await request.GetResponse<ScontrinoStatoListResponse>();
            result = response.Message.ScontriniStati;
        }

        return Ok(result);
    }

    [HttpGet("TipoCliente")]
    public async Task<IActionResult> GetTipoClienteAsync()
    {
        var result = new List<TipoCliente>();

        using (var request = tipoClienteRequestClient.Create(new TipoClienteListRequest { }))
        {
            var response = await request.GetResponse<TipoClienteListResponse>();
            result = response.Message.TipoClienti;
        }

        return Ok(result);
    }

    [HttpGet("TipoPagamento")]
    public async Task<IActionResult> GetTipoPagamentoAsync()
    {
        var result = new List<TipoPagamento>();

        using (var request = tipoPagamentoRequestClient.Create(new TipoPagamentoListRequest { }))
        {
            var response = await request.GetResponse<TipoPagamentoListResponse>();
            result = response.Message.TipoPagamenti;
        }

        return Ok(result);
    }

    [HttpGet("TipoScontrino")]
    public async Task<IActionResult> GetTipoScontrinoAsync()
    {
        var result = new List<TipoScontrino>();

        using (var request = tipoScontrinoRequestClient.Create(new TipoScontrinoListRequest { }))
        {
            var response = await request.GetResponse<TipoScontrinoListResponse>();
            result = response.Message.TipoScontrini;
        }

        return Ok(result);
    }
}