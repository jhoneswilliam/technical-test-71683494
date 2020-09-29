using System.Collections.Generic;

namespace Domain.Entities
{
    public class Multa : Entity
    {
        public int DiasEmAtraso { get; set; }
        public double PorcentagemAplicada { get; set; }

        public virtual ICollection<ContaAPagar> ContaAPagarMultadas { get; }

        public Multa()
        {
            ContaAPagarMultadas = new HashSet<ContaAPagar>();
        }
    }
}
