using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobalBankApi.Data;
using GlobalBankApi.Models;

namespace GlobalBankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly AppDB _context;

        public ContasController(AppDB context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ContaBancaria>> PostConta(ContaBancaria conta)
        {
            // Rsaldo não pode ser negativo na abertura
            if (conta.Saldo < 0)
            {
                return BadRequest("O saldo inicial não pode ser negativo para contas internacionais.");
            }

            _context.Contas.Add(conta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContas), new { id = conta.Id }, conta);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContaBancaria>>> GetContas()
        {
            return await _context.Contas.ToListAsync();
        }
    }
}