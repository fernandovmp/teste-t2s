using System;
using TesteT2S.WebApi.Features.ShipHandling.Enums;

namespace TesteT2S.WebApi.Features.ShipHandling.ViewModels
{
    public class CreateHandlingViewModel
    {
        public string Navio { get; set; }
        public HandlingType TipoMovimentacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
