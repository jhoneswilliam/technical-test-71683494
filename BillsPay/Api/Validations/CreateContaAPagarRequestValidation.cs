using Domain.DTO.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validations
{
    public class CreateContaAPagarRequestValidation : AbstractValidator<CreateContaAPagarRequest>
    {
        public CreateContaAPagarRequestValidation()
        {
            RuleFor(e => e.Nome).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");
            RuleFor(e => e.ValorOriginal).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");
            RuleFor(e => e.DataPagamento).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");
            RuleFor(e => e.DataVencimento).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");
        }
    }
}
