using Microsoft.EntityFrameworkCore;
using GlobalBankApi.Models;

namespace GlobalBankApi.Data
{
    // Corrigido: Herdando de DbContext em vez de AppDB
    public class AppDB : DbContext
    {
        public AppDB(DbContextOptions<AppDB> options) : base(options) { }

        public DbSet<ContaBancaria> Contas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
    }
}