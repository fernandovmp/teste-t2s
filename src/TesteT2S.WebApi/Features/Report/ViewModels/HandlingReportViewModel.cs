using System.Collections.Generic;

namespace TesteT2S.WebApi.Features.Report.ViewModels
{
    public class HandlingReportViewModel
    {
        public string Cliente { get; set; }
        public IEnumerable<HandlingReportEntryViewModel> Movimentacoes { get; set; }
    }
}
