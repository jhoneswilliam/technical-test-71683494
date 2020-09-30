using System;

namespace Domain.Entities
{
    public class ContaAPagar : Entity
    {
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorCorrigido { get; set; }
        public int QuantidadeDiasEmAtraso { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }

        public Guid? MultaAplicadaId { get; set; }
        public Multa MultaAplicada { get; set; }
    }
}
