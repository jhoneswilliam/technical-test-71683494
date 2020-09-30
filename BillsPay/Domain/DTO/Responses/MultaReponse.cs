using System;

namespace Domain.DTO.Responses
{
    public class MultaReponse
    {
        public Guid Id { get; set; }
        public int? PeriodoMaximoDeDiasEmAtraso { get; set; }
        public double PorcentagemAplicadaDiaAtraso { get; set; }
        public double PorcentagemAplicadaMulta { get; set; }

    }
}
