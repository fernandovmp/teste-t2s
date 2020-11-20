using System;
using AutoMapper;
using TesteT2S.WebApi.Features.Containers.Enums;
using TesteT2S.WebApi.Features.Containers.ViewModels;

namespace TesteT2S.WebApi.Features.Containers.Mappers
{
    public class ContainerStatusConverter : ITypeConverter<ContainerStatus, ContainerEnumViewModel>
    {
        public ContainerEnumViewModel Convert(ContainerStatus source,
            ContainerEnumViewModel destination,
            ResolutionContext context)
        {
            string description = source switch
            {
                ContainerStatus.Empty => "Vazio",
                ContainerStatus.Full => "Cheio",
                _ => throw new Exception(
                    $"There is no supported conversion to value {source} of {nameof(ContainerStatus)}")
            };
            return new ContainerEnumViewModel
            {
                Id = (int)source,
                Descricao = description
            };
        }
    }
}
