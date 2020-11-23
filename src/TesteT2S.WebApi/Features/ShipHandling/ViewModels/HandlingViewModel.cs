using System;
using TesteT2S.WebApi.ViewModels;

namespace TesteT2S.WebApi.Features.ShipHandling.ViewModels
{
    public class HandlingViewModel
    {

        public int Id { get; set; }
        public string Navio { get; set; }
        public EnumViewModel TipoMovimentacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
