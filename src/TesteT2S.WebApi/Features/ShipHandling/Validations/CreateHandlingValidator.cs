using FluentValidation;
using TesteT2S.WebApi.Features.ShipHandling.ViewModels;

namespace TesteT2S.WebApi.Features.ShipHandling.Validations
{
    public class CreateHandlingValidator : AbstractValidator<CreateHandlingViewModel>
    {
        public CreateHandlingValidator()
        {
            RuleFor(handling => handling.Navio)
                .NotEmpty().WithMessage("Navio é obrigatório")
                .MaximumLength(50).WithMessage("Navio não deve ultrapassar 50 caractéres");
            RuleFor(handling => handling.TipoMovimentacao)
                .IsInEnum().WithMessage("Valor do tipo de movimentação inválida");
            RuleFor(handling => handling.DataInicio)
                .LessThan(handling => handling.DataFim)
                .WithMessage("Data de inicio deve ser inferior a data de fim");
        }
    }
}
