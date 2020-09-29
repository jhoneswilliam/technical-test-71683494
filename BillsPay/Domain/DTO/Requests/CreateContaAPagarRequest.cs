using System;

namespace Domain.DTO.Requests
{
    public class CreateContaAPagarRequest
    {
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
