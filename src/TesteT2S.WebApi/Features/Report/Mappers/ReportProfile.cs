using AutoMapper;
using TesteT2S.WebApi.Features.Report.Models;
using TesteT2S.WebApi.Features.Report.ViewModels;

namespace TesteT2S.WebApi.Features.Report.Mappers
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<HandlingReportCollection, HandlingReportCollectionViewModel>()
                .ForMember(destination => destination.Dados, options => options.MapFrom(source => source.Data))
                .ForMember(destination => destination.TotalExportacoes,
                    options => options.MapFrom(source => source.TotalExportation))
                .ForMember(destination => destination.TotalImportacoes,
                    options => options.MapFrom(source => source.TotalImportation));
            CreateMap<HandlingReport, HandlingReportViewModel>()
                .ForMember(destination => destination.Cliente, options => options.MapFrom(source => source.Customer))
                .ForMember(destination => destination.Movimentacoes,
                    options => options.MapFrom(source => source.ReportEntries));
            CreateMap<HandlingReportEntry, HandlingReportEntryViewModel>()
                .ForMember(destination => destination.QuantidadeMovimentacoes,
                    options => options.MapFrom(source => source.HandlingAmount))
                .ForMember(destination => destination.TipoMovimentacao,
                    options => options.MapFrom(source => source.HandlingType));

        }

    }
}
