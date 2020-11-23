using AutoMapper;
using TesteT2S.WebApi.Features.ShipHandling.Enums;
using TesteT2S.WebApi.Features.ShipHandling.Models;
using TesteT2S.WebApi.Features.ShipHandling.ViewModels;
using TesteT2S.WebApi.ViewModels;

namespace TesteT2S.WebApi.Features.ShipHandling.Mappers
{
    public class HandlingProfile : Profile
    {
        public HandlingProfile()
        {
            CreateMap<CreateHandlingViewModel, Handling>()
                .ForMember(destination => destination.Ship, options => options.MapFrom(source => source.Navio))
                .ForMember(destination => destination.HandlingType,
                    options => options.MapFrom(source => source.TipoMovimentacao))
                .ForMember(destination => destination.Start, options => options.MapFrom(source => source.DataInicio))
                .ForMember(destination => destination.End, options => options.MapFrom(source => source.DataFim));
            CreateMap<Handling, HandlingViewModel>()
                .ForMember(destination => destination.Navio, options => options.MapFrom(source => source.Ship))
                .ForMember(destination => destination.TipoMovimentacao,
                    options => options.MapFrom(source => source.HandlingType))
                .ForMember(destination => destination.DataInicio, options => options.MapFrom(source => source.Start))
                .ForMember(destination => destination.DataFim, options => options.MapFrom(source => source.End));
            CreateMap<HandlingType, EnumViewModel>()
                .ConvertUsing<HandlingTypeConverter>();
        }
    }
}
