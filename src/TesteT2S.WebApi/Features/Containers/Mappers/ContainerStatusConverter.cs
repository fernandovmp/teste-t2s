using System;
using AutoMapper;
using TesteT2S.WebApi.Features.Containers.Enums;
using TesteT2S.WebApi.ViewModels;

namespace TesteT2S.WebApi.Features.Containers.Mappers
{
    public class ContainerStatusConverter : ITypeConverter<ContainerStatus, EnumViewModel>
    {
        public EnumViewModel Convert(ContainerStatus source,
            EnumViewModel destination,
            ResolutionContext context)
        {
            string description = source switch
            {
                ContainerStatus.Empty => "Vazio",
                ContainerStatus.Full => "Cheio",
                _ => throw new Exception(
                    $"There is no supported conversion to value {source} of {nameof(ContainerStatus)}")
            };
            return new EnumViewModel
            {
                Id = (int)source,
                Descricao = description
            };
        }
    }
}
