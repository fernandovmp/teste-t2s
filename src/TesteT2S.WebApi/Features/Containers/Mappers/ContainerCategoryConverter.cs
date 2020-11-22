using System;
using AutoMapper;
using TesteT2S.WebApi.Features.Containers.Enums;
using TesteT2S.WebApi.ViewModels;

namespace TesteT2S.WebApi.Features.Containers.Mappers
{
    public class ContainerCategoryConverter : ITypeConverter<ContainerCategory, EnumViewModel>
    {
        public EnumViewModel Convert(ContainerCategory source,
            EnumViewModel destination,
            ResolutionContext context)
        {
            string description = source switch
            {
                ContainerCategory.Exportation => "Exportação",
                ContainerCategory.Importation => "Importação",
                _ => throw new Exception(
                    $"There is no supported conversion to value {source} of {nameof(ContainerCategory)}")
            };
            return new EnumViewModel
            {
                Id = (int)source,
                Descricao = description
            };
        }
    }
}
