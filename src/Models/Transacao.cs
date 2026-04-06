using System.Text.Json.Serialization;

namespace GlobalBankApi.Models
{
    public class Transacao
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int ContaId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; }
    }
}