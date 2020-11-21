using System.Collections;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using TesteT2S.WebApi.Features.Containers.Enums;
using TesteT2S.WebApi.Features.Containers.Validations;
using TesteT2S.WebApi.Features.Containers.ViewModels;
using Xunit;

namespace TesteT2S.WebApi.UnitTests.Features.Containers.Validations
{
    public class CreateContainerValidatorTests
    {
        private readonly CreateContainerValidator _createContainerValidator;

        public CreateContainerValidatorTests()
        {
            _createContainerValidator = new CreateContainerValidator();
        }

        [Theory]
        [MemberData(nameof(GetValidModels))]
        public void Validate_ValidModel_ShouldNotHaveValidationErrors(CreateContainerViewModel model)
        {
            TestValidationResult<CreateContainerViewModel> result = _createContainerValidator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        public static IEnumerable<object[]> GetValidModels()
        {
            yield return new[] {
                new CreateContainerViewModel{
                    Categoria = ContainerCategory.Importation,
                    Cliente = "Fernando",
                    Numero = "abcd1234567",
                    Status = ContainerStatus.Empty,
                    Tipo = 20
                }
            };
            yield return new[] {
                new CreateContainerViewModel{
                    Categoria = ContainerCategory.Exportation,
                    Cliente = "Fernando",
                    Numero = "abcd1234567",
                    Status = ContainerStatus.Empty,
                    Tipo = 20
                }
            };
            yield return new[] {
                new CreateContainerViewModel{
                    Categoria = ContainerCategory.Exportation,
                    Cliente = "Fernando",
                    Numero = "abcd1234567",
                    Status = ContainerStatus.Full,
                    Tipo = 20
                }
            };
            yield return new[] {
                new CreateContainerViewModel{
                    Categoria = ContainerCategory.Exportation,
                    Cliente = "Fernando",
                    Numero = "abcd1234567",
                    Status = ContainerStatus.Full,
                    Tipo = 40
                }
            };
            yield return new[] {
                new CreateContainerViewModel{
                    Categoria = ContainerCategory.Exportation,
                    Cliente = "Fernando",
                    Numero = "aaaa0000000",
                    Status = ContainerStatus.Full,
                    Tipo = 40
                }
            };
            yield return new[] {
                new CreateContainerViewModel{
                    Categoria = ContainerCategory.Exportation,
                    Cliente = "F",
                    Numero = "aaaa0000000",
                    Status = ContainerStatus.Full,
                    Tipo = 40
                }
            };
            yield return new[] {
                new CreateContainerViewModel{
                    Categoria = ContainerCategory.Exportation,
                    Cliente = "IjCQagrhMsxQwIZqfCINnYlYtDuNbKRnYZjteFlDSyNrXvRqLD",
                    Numero = "aaaa0000000",
                    Status = ContainerStatus.Full,
                    Tipo = 40
                }
            };
        }

        [Fact]
        public void Validate_CustomerNameGreaterThan50_HaveCustomerValidationError()
        {
            var model = new CreateContainerViewModel
            {
                Categoria = ContainerCategory.Importation,
                Cliente = "IjCQagrhMsxQwIZqfCINnYlYtDuNbKRnYZjteFlDSyNrXvRqLDCmTDTeZdpd",
                Numero = "abcd1234567",
                Status = ContainerStatus.Empty,
                Tipo = 20
            };
            TestValidationResult<CreateContainerViewModel> result = _createContainerValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(container => container.Cliente);
        }

        [Fact]
        public void Validate_CustomerNameIsEmpty_HaveCustomerValidationError()
        {
            var model = new CreateContainerViewModel
            {
                Categoria = ContainerCategory.Importation,
                Cliente = "",
                Numero = "abcd1234567",
                Status = ContainerStatus.Empty,
                Tipo = 20
            };
            TestValidationResult<CreateContainerViewModel> result = _createContainerValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(container => container.Cliente);
        }

        [Fact]
        public void Validate_InvalidType_HaveTypeValidationError()
        {
            var model = new CreateContainerViewModel
            {
                Categoria = ContainerCategory.Importation,
                Cliente = "Fernando",
                Numero = "abcd1234567",
                Status = ContainerStatus.Empty,
                Tipo = 15
            };
            TestValidationResult<CreateContainerViewModel> result = _createContainerValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(container => container.Tipo);
        }

        [Fact]
        public void Validate_StatusOutOfEnum_HaveStatusValidationError()
        {
            var model = new CreateContainerViewModel
            {
                Categoria = ContainerCategory.Importation,
                Cliente = "Fernando",
                Numero = "abcd1234567",
                Status = (ContainerStatus)2,
                Tipo = 20
            };
            TestValidationResult<CreateContainerViewModel> result = _createContainerValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(container => container.Status);
        }

        [Fact]
        public void Validate_CategoryOutOfEnum_HaveCategoryValidationError()
        {
            var model = new CreateContainerViewModel
            {
                Categoria = (ContainerCategory)2,
                Cliente = "Fernando",
                Numero = "abcd1234567",
                Status = ContainerStatus.Full,
                Tipo = 20
            };
            TestValidationResult<CreateContainerViewModel> result = _createContainerValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(container => container.Categoria);
        }

        [Theory]
        [InlineData("")]
        [InlineData("1234abcdefgh")]
        [InlineData("1234abcdef7")]
        [InlineData("123aabcdefg")]
        [InlineData("abcd12345a6")]
        [InlineData("0bcd1234567")]
        [InlineData("abc01234567")]
        [InlineData("abcde234567")]
        [InlineData("123a1bcdefg")]
        [InlineData("1234abc4efg")]
        [InlineData("12345678901")]
        [InlineData("aaaabcdefgh")]
        public void Validate_InvalidNumber_HaveNumberValidationError(string number)
        {
            var model = new CreateContainerViewModel
            {
                Categoria = ContainerCategory.Exportation,
                Cliente = "Fernando",
                Numero = number,
                Status = ContainerStatus.Full,
                Tipo = 20
            };
            TestValidationResult<CreateContainerViewModel> result = _createContainerValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(container => container.Numero);
        }
    }
}
