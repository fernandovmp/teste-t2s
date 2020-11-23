using System.Collections.Generic;

namespace TesteT2S.WebApi.Features.Report.ViewModels
{
    public class HandlingReportCollectionViewModel
    {
        public IEnumerable<HandlingReportViewModel> Dados { get; set; }
        public int TotalExportacoes { get; set; }
        public int TotalImportacoes { get; set; }
    }
}
