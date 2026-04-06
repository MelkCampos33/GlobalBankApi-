using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobalBankApi.Data;
using GlobalBankApi.Models;

namespace GlobalBankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly AppDB _context;

        public TransacoesController(AppDB context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> RegistrarTransacao(Transacao transacao)
        {
            var conta = await _context.Contas.FindAsync(transacao.ContaId);
            if (conta == null) return NotFound("A conta não foi encontrada.");

            if (transacao.Tipo.ToLower() == "saque")
            {
                if (conta.Saldo < transacao.Valor)
                    return Conflict("Saldo Insuficiente.");
                
                conta.Saldo -= transacao.Valor;
            }
            else if (transacao.Tipo.ToLower() == "deposito")
            {
                conta.Saldo += transacao.Valor;
            }

            // alerta de segurança do pc
            if (transacao.Valor > 10000)
            {
                Console.WriteLine($"Transação de alto valor detectada para a conta {conta.NumeroConta}!");
            }

            transacao.DataTransacao = DateTime.Now;
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = "Transação realizada com sucesso", NovoSaldo = conta.Saldo });
        }

        [HttpGet("extrato/{contaId}")]
        public async Task<ActionResult<IEnumerable<Transacao>>> GetExtrato(int contaId)
        {
            return await _context.Transacoes
                .Where(t => t.ContaId == contaId)
                .ToListAsync();
        }
    }
}