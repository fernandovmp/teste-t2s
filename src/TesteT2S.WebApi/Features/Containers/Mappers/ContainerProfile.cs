using AutoMapper;
using TesteT2S.WebApi.Features.Containers.Enums;
using TesteT2S.WebApi.Features.Containers.Models;
using TesteT2S.WebApi.Features.Containers.ViewModels;
using TesteT2S.WebApi.ViewModels;

namespace TesteT2S.WebApi.Features.Containers.Mappers
{
    public class ContainerProfile : Profile
    {
        public ContainerProfile()
        {
            CreateMap<CreateContainerViewModel, Container>()
                .ForMember(destination => destination.Number, options => options.MapFrom(source => source.Numero))
                .ForMember(destination => destination.Type, options => options.MapFrom(source => source.Tipo))
                .ForMember(destination => destination.Customer, options => options.MapFrom(source => source.Cliente))
                .ForMember(destination => destination.Category, options => options.MapFrom(source => source.Categoria));
            CreateMap<Container, ContainerViewModel>()
                .ForMember(destination => destination.Numero, options => options.MapFrom(source => source.Number))
                .ForMember(destination => destination.Tipo, options => options.MapFrom(source => source.Type))
                .ForMember(destination => destination.Cliente, options => options.MapFrom(source => source.Customer))
                .ForMember(destination => destination.Categoria, options =>
                {
                    options.MapFrom(source => source.Category);
                });
            CreateMap<ContainerStatus, EnumViewModel>()
                .ConvertUsing<ContainerStatusConverter>();
            CreateMap<ContainerCategory, EnumViewModel>()
                .ConvertUsing<ContainerCategoryConverter>();
        }
    }
}
