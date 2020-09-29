using System;

namespace Domain.DTO.Responses
{
    public class ContaAPagarReponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorCorrigido { get; set; }
        public int QuantidadeDiasEmAtraso { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
