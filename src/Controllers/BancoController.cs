using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobalBankApi.Data; // Certifique-se que o namespace do seu AppDB é este

[ApiController]
[Route("api/banco")]
public class BancoController : ControllerBase
{
    private readonly AppDB _context;
    public BancoController(AppDB context) => _context = context;

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var patrimonioTotal = await _context.Contas.SumAsync(c => c.Saldo);
        var totalTransacoes = await _context.Transacoes.CountAsync();

        return Ok(new { PatrimonioTotal = patrimonioTotal, QuantidadeTotalTransacoes = totalTransacoes });
    }
}