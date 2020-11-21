using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using TesteT2S.WebApi.Features.Containers.ViewModels;

namespace TesteT2S.WebApi.Features.Containers.Validations
{
    public class CreateContainerValidator : AbstractValidator<CreateContainerViewModel>
    {
        public CreateContainerValidator()
        {
            RuleFor(container => container.Numero)
                .NotEmpty().WithMessage("Número do container é obrigatório")
                .Custom(ContainerNumberValidator);
            RuleFor(container => container.Tipo)
                .Custom(ContainerTypeValidator);
            RuleFor(container => container.Status)
                .IsInEnum().WithMessage("Valor de status inválido");
            RuleFor(container => container.Categoria)
                .IsInEnum().WithMessage("Valor da categoria inválida");
            RuleFor(container => container.Cliente)
                .NotEmpty().WithMessage("Nome do cliente é obrigatório")
                .MaximumLength(Constants.CustomerNameMaxLenght)
                .WithMessage("Nome do cliente deve ter menos de 50 caractéres");
        }

        private void ContainerNumberValidator(string value, CustomContext context)
        {
            const string ErrorMessage = "Número de container inválido";
            if (value.Length != Constants.ContainerNumberMaxLenght)
            {
                context.AddFailure(ErrorMessage);
                return;
            }
            if (int.TryParse(value.Substring(0, 4), out int _))
            {
                context.AddFailure(ErrorMessage);
                return;
            }
            if (!value.Substring(4).All(character => char.IsLetter(character)))
            {
                context.AddFailure(ErrorMessage);
            }

        }

        private void ContainerTypeValidator(int value, CustomContext context)
        {
            const string ErrorMessage = "Tipo deve ser 20 ou 40";
            if (value != 20 && value != 40)
            {
                context.AddFailure(ErrorMessage);
            }

        }
    }
}
