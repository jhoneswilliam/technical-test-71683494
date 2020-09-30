namespace Domain.Entities
{
    public class Multa : Entity
    {
        /// <summary>
        /// O valor null é usado para determinar como entidade padrão caso os demais estejam fora do range de dias
        /// </summary>
        public int? PeriodoMaximoDeDiasEmAtraso { get; set; }
        public double PorcentagemAplicadaDiaAtraso { get; set; }
        public double PorcentagemAplicadaMulta { get; set; }

    }
}
